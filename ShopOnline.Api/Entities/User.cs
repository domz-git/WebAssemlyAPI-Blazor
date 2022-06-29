//These classes represent entities in a database
//Each class defines properties for each object and table in a database
namespace ShopOnline.Api.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }

    }
}
