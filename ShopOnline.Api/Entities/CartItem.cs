//These classes represent entities in a database
//Each class defines paramaterers for each object and table in a database
namespace ShopOnline.Api.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

    }
}
