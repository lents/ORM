using DataAccess.LinqToDb.Models;
using LinqToDB;
using LinqToDB.Data;

namespace DataAccess.LinqToDb.DataAccess
{
    public class ApiDataContext : DataConnection
    {
        public ApiDataContext(DataOptions<ApiDataContext> options)
        : base(options.Options) { }

        public ITable<UserModel> Users => this.GetTable<UserModel>();
        public ITable<RelationModel> Relatives => this.GetTable<RelationModel>();
        public ITable<StudentModel> Students => this.GetTable<StudentModel>();

    }

}
