using Microsoft.Data.SqlClient;

namespace Consoleappprogram
{
    internal class DataConnect
    {
        public static void Main(string[] args)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BijayDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

                try
                {
                    conn.Open();
                    Console.WriteLine("Bijay Shrestha");
                    Console.WriteLine("Connection Opened");
                    Console.WriteLine("Choose an operation:");
                    Console.WriteLine("1. Create user");
                    Console.WriteLine("2. Read user");
                    Console.WriteLine("3. Update user");
                    Console.WriteLine("4. Delete user");

                    int choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            CreateUser(conn);
                            break;
                        case 2:
                            ReadUser(conn);
                            break;
                        case 3:
                            UpdateUser(conn);
                            break;
                        case 4:
                            DeleteUser(conn);
                            break;
                        default:
                            Console.WriteLine("Invalid choice");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private static void CreateUser(SqlConnection conn)
        {
            Console.WriteLine("Enter user details:");
            Console.Write("Id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Role: ");
            string role = Console.ReadLine();
            Console.Write("Status: ");
            string status = Console.ReadLine();

            string insertSql = "INSERT INTO Students(Id, username, password, email, role, status) VALUES(@id, @username, @password, @email, @role, @status)";

            using (SqlCommand insertCommand = new SqlCommand(insertSql, conn))
            {
                insertCommand.Parameters.AddWithValue("@id", id);
                insertCommand.Parameters.AddWithValue("@username", username);
                insertCommand.Parameters.AddWithValue("@password", password);
                insertCommand.Parameters.AddWithValue("@email", email);
                insertCommand.Parameters.AddWithValue("@role", role);
                insertCommand.Parameters.AddWithValue("@status", status);

                int rowAffected = insertCommand.ExecuteNonQuery();
                Console.WriteLine($"{rowAffected} row(s) inserted.");
            }
        }

        private static void ReadUser(SqlConnection conn)
        {
            string selectSql = "SELECT * FROM Students";

            using (SqlCommand selectCommand = new SqlCommand(selectSql, conn))
            using (SqlDataReader reader = selectCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Id: {reader["Id"]}, Username: {reader["username"]}, Email: {reader["email"]}, Role: {reader["role"]}, Status: {reader["status"]}");
                }
            }
        }

        private static void UpdateUser(SqlConnection conn)
        {
            Console.Write("Enter the Id of the user to update: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter new username: ");
            string newUsername = Console.ReadLine();

            string updateSql = "UPDATE Students SET username = @username WHERE Id = @id";

            using (SqlCommand updateCommand = new SqlCommand(updateSql, conn))
            {
                updateCommand.Parameters.AddWithValue("@id", id);
                updateCommand.Parameters.AddWithValue("@username", newUsername);

                int rowAffected = updateCommand.ExecuteNonQuery();
                Console.WriteLine($"{rowAffected} row(s) updated.");
            }
        }

        private static void DeleteUser(SqlConnection conn)
        {
            Console.Write("Enter the Id of the user to delete: ");
            int id = Convert.ToInt32(Console.ReadLine());

            string deleteSql = "DELETE FROM Students WHERE Id = @id";

            using (SqlCommand deleteCommand = new SqlCommand(deleteSql, conn))
            {
                deleteCommand.Parameters.AddWithValue("@id", id);

                int rowAffected = deleteCommand.ExecuteNonQuery();
                Console.WriteLine($"{rowAffected} row(s) deleted.");
            }
        }
    }
}
