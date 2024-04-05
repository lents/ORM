namespace DataAccess.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<RelationModel> Relations { get; set; } = new List<RelationModel>();
        
    }
}
