using DataAccess.DataAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class UserData : IUserData
    {
        private readonly ISqlDataAccess _db;
        public UserData(ISqlDataAccess db)
        {
            _db = db;
        }

        //public Task<IEnumerable<UserModel>> GetUsers() =>
        //    _db.LoadData<UserModel, dynamic>("dbo.spUser_GetAll", new { });

        public Task<IEnumerable<UserModel>> GetUsers() =>
            _db.LoadWithQuery<UserModel, dynamic>("SELECT Id, FirstName, LastName FROM [dbo].[User]", new { });

        public Task<IEnumerable<UserModel>> GetUsersByFilter(string filter) =>
            _db.LoadData<UserModel, dynamic>("dbo.spUser_GetByFilter", new { Filter = filter });

        public async Task<UserModel?> GetUser(int id)
        {
             //var results = await _db.LoadData<UserModel, dynamic>("dbo.spUser_Get", new { Id = id });
             //return results.FirstOrDefault();
            return await GetUserWithRelations(id);
        }

        public async Task<UserModel?> GetUserWithRelations(int id)
        {
            var lookup = new Dictionary<int, UserModel>();
            var results = await _db.LoadWithJoin<UserModel, RelationModel, dynamic>(
                "SELECT u.Id, FirstName, LastName, RelationShip, r.Id, FullName FROM [User] u LEFT OUTER JOIN Relative r ON r.UserId = u.Id WHERE u.Id = @Id",
                new { Id = id },
                (user, relation) => {
                    UserModel userModel;

                    if (!lookup.TryGetValue(user.Id, out userModel))
                    {
                        userModel = user;
                        userModel.Relations = new List<RelationModel>();
                        lookup.Add(user.Id, userModel);
                    }

                    if (relation != null)
                    {
                        userModel.Relations.Add(relation);
                    }

                    return userModel;
                }, "RelationShip");
            return results.FirstOrDefault();
        }

        public async Task InsertUser(UserModel user) =>
            await _db.SaveData("dbo.spUser_Insert", new { user.FirstName, user.LastName });

        public async Task UpdateUser(UserModel user) =>
            await _db.SaveData("dbo.spUser_Update", user);

        public async Task DeleteUser(int id) =>
            await _db.SaveData("dbo.spUser_Delete", new { Id = id });

    }
}
