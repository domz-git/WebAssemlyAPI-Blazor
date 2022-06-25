//These classes represent entities in a database, this is done at the begining of the development
//Each class defines paramaterers for each object and table in a database

//After this we need to install two data packeges in dependecies node in ShopOnline.Api using nuget packages manager
//EntityFrameWorkCore.SQLServer and EntityFrameworkCore.Tools
namespace ShopOnline.Api.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
    }
}
