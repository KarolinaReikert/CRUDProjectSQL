using System.Data;
using System.Data.SQLite;
using System.Runtime.CompilerServices;

namespace CRUDProjectSQL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Program program = new Program();
            Users user1 = new Users();
            user1.Id = 33;
            user1.FirstName = "Toms";
            user1.LastName = "Berzins";

            // Console.WriteLine(program.AddUser(user1));
           // Console.WriteLine(program.DeleteUser(user1));
           
            DataTable dataTable = GetUserById (14);
            var id = dataTable.Rows[0]["id"];
            var firstName = dataTable.Rows[0]["firstName"];
            var lastName = dataTable.Rows[0]["lastName"];

            Console.WriteLine($" {id} {firstName} {lastName}");
        }

        private int ExecuteWrite(string query, Dictionary<string, object> args)
        {
            int numberOfRowsAffected;

            using (var connect = new SQLiteConnection("Data Source=your_path/bookstores.db"))
            {
                connect.Open();
                using (var command = new SQLiteCommand(query, connect))
                {
                    foreach (var dictionaryElements in args)
                    {
                        command.Parameters.AddWithValue(dictionaryElements.Key, dictionaryElements.Value);
                    }
                    numberOfRowsAffected = command.ExecuteNonQuery();
                }
                return numberOfRowsAffected;

            }
        }
        private int AddUser(Users user)
        {
            const string query = "INSERT INTO Users (id, firstName, lastName) VALUES (@id, @firstName, @lastName);";
            var arg = new Dictionary<string, object>
            {
                { "@id", user.Id},
                { "@firstName" , user.FirstName},
                { "@lastName", user.LastName}
            };
            return ExecuteWrite(query, arg);
        }
        private int DeleteUser(Users user)
        {
            const string query = "DELETE FROM Users where id = 8;";
            var arg = new Dictionary<string, object>
            {
            { "@id", user.Id},

            };
            return ExecuteWrite(query, arg);
        }

        private static DataTable GetUserById(int id)
        {
            using (var connect = new SQLiteConnection("Data Source=your_path/bookstores.db"))
            {
                connect.Open();
                var query = "SELECT * FROM Users WHERE id = @id";
                using (var command = new SQLiteCommand(query, connect))
                {
                    command.Parameters.AddWithValue("@id", id);

                    var dataAdapter = new SQLiteDataAdapter(command);

                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataAdapter.Dispose();

                    return dataTable;
                }
            }
        }
    }
}


