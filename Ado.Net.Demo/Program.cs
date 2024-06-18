using System;
using System.Data;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connectionString = "Server=.;Database=UserDB;Integrated Security=True;";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "INSERT INTO [User] (FirstName, LastName) VALUES (@FirstName, @LastName)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@FirstName", "John");
                command.Parameters.AddWithValue("@LastName", "Doe");

                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine("Rows Affected: " + rowsAffected);
            }
            query = "SELECT Id, FirstName, LastName FROM [User]";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string lastName = reader.GetString(2);
                        //int age = reader.GetInt32(2);

                        Console.WriteLine($"ID: {id}, FirstName: {name}, LastName: {lastName}");
                    }
                }
            }
        }
        

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT Id, Name, Age FROM Students";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Students");

            foreach (DataRow row in dataSet.Tables["Students"].Rows)
            {
                int id = (int)row["Id"];
                string name = (string)row["Name"];
                int age = (int)row["Age"];

                Console.WriteLine($"ID: {id}, Name: {name}, Age: {age}");
            }
        }
    }

    public static async Task<int> GetUserCountAsync(string connectionString)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            string query = "SELECT COUNT(*) FROM Users";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // ExecuteScalar returns the first column of the first row in the result set
                object result = await command.ExecuteScalarAsync();
                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
                return 0;
            }
        }
    }
}
