using DataAccess.LinqToDb.DataAccess;
using DataAccess.LinqToDb.Models;
using LinqToDB;

namespace DataAccess.LinqToDb.Data
{
    public class UserData : IUserData
    {
        private readonly ApiDataContext _db;
        public UserData(ApiDataContext db)
        {
            _db = db;
        }

        public Task DeleteUser(int id)
        {
            return _db.Users.DeleteAsync(s => s.Id == id);
        }

        public Task<UserModel?> GetUser(int id)
        {
            return _db.Users.FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<List<UserModel>> GetUsers()
        {
            return _db.Users.ToListAsync();
        }

        public Task<List<UserModel>> GetUsersByFilter(string filter)
        {
            return _db.Users.Where(s => s.FirstName.Contains(filter) || s.LastName.Contains(filter)).ToListAsync();
        }

        public Task InsertUser(UserModel user)
        {
            return _db.InsertWithIdentityAsync(user);
        }

        public Task UpdateUser(UserModel user)
        {
            return _db.UpdateAsync(user);
        }
    }
}
