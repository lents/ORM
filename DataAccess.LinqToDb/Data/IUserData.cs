using DataAccess.LinqToDb.Models;

namespace DataAccess.LinqToDb.Data
{
    public interface IUserData
    {
        Task DeleteUser(int id);
        Task<UserModel?> GetUser(int id);
        Task<List<UserModel>> GetUsers();
        Task<List<UserModel>> GetUsersByFilter(string filter);
        Task InsertUser(UserModel user);
        Task UpdateUser(UserModel user);
    }
}