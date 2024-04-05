using LinqToDB.Mapping;
using System.ComponentModel.DataAnnotations.Schema;
using ColumnAttribute = LinqToDB.Mapping.ColumnAttribute;
using TableAttribute = LinqToDB.Mapping.TableAttribute;

namespace DataAccess.LinqToDb.Models
{
    [Table("Relative")]
    public class RelationModel
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }
        [Column]
        public string Relationship { get; set; }
        [Column]
        public string FullName { get; set; }
        [Column]
        [ForeignKey("FK_RelationUser")]
        public int UserId { get; set; }
    }

}
