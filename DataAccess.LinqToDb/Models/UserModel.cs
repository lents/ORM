using LinqToDB.Mapping;
using ColumnAttribute = LinqToDB.Mapping.ColumnAttribute;
using TableAttribute = LinqToDB.Mapping.TableAttribute;

namespace DataAccess.LinqToDb.Models
{
    [Table("User")]
    public class UserModel
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }
        [Column]
        public string FirstName { get; set; }
        [Column]
        public string LastName { get; set; }

        [Association(ThisKey = nameof(UserModel.Id), OtherKey = nameof(RelationModel.UserId))]
        public IEnumerable<RelationModel> Relations { get; set; } = new List<RelationModel>();

        public static UserModel Build(UserModel user, IEnumerable<RelationModel> relations)
        {
            if (user != null) {
                user.Relations = relations;
            }
            return user;
        }
    }

}
