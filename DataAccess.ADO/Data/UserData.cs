using DataAccess.ADO.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace DataAccess.ADO.Data
{
    public class UserData : IUserData
    {
        private readonly IConfiguration _config;
        string connectionId = "Default";
        public UserData(IConfiguration config)
        {
            _config = config;
        }
        public async Task<IEnumerable<UserModel>> GetUsers() {
            using var connection = new SqlConnection(_config.GetConnectionString(connectionId));
            connection.Open();

            var query = "SELECT Id, FirstName, LastName FROM [User]";
            var users = new List<UserModel>();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {                      
                        //int age = reader.GetInt32(2);
                        users.Add(new UserModel
                        {
                            Id = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2)
                        });
                    }
                }
            }
            return users;
        }

        public async Task<IEnumerable<UserModel>> GetUsersByFilter(string filter)
        {
            using var connection = new SqlConnection(_config.GetConnectionString(connectionId));
            connection.Open();

            var query = "SELECT Id, FirstName, LastName FROM [User] WHERE FirstName LIKE '%'+@Filter+'%' OR LastName LIKE '%'+@Filter+'%'";
            var users = new List<UserModel>();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Filter", filter);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        //int age = reader.GetInt32(2);
                        users.Add(new UserModel
                        {
                            Id = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2)
                        });
                    }
                }
            }
            return users;
        }
        public async Task<UserModel?> GetUser(int userId)
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString(connectionId)))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Name, Email FROM Users WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", userId);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new UserModel
                            {
                                Id = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2)
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public async Task<UserModel?> GetUserWithRelations(int userId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString(connectionId));
            connection.Open();

            var query = "SELECT u.Id, FirstName, LastName, RelationShip, r.Id, FullName FROM [User] u LEFT OUTER JOIN Relative r ON r.UserId = u.Id WHERE u.Id = @Id";
            var users = new List<UserModel>();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", userId);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new UserModel
                        {
                            Id = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2)
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public async Task InsertUser(UserModel user)
        {
            using var connection = new SqlConnection(_config.GetConnectionString(connectionId));
            connection.Open();

            string query = "INSERT INTO [User] (FirstName, LastName) VALUES (@FirstName, @LastName)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);

                int rowsAffected = command.ExecuteNonQuery();
            }
        }


        public async Task UpdateUser(UserModel user) {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString(connectionId)))
            {
                await connection.OpenAsync();

                string updateQuery = "UPDATE Users SET Name = @Name, Email = @Email WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", user.Id);
                    command.Parameters.AddWithValue("@Name", user.FirstName);
                    command.Parameters.AddWithValue("@Email", user.LastName);

                    int rowsAffected = await command.ExecuteNonQueryAsync();                    
                }
            }
        }

        public async Task DeleteUser(int userId) {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString(connectionId)))
            {
                await connection.OpenAsync();

                string deleteQuery = "DELETE FROM Users WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", userId);

                    int rowsAffected = await command.ExecuteNonQueryAsync();                    
                }
            }
        }

    }
}
