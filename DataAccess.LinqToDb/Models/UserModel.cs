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
    }

}
