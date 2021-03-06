//Here we are returning a collection of product data to the client side calling code (Blazor components)

//We need to use shopOnlineDbContext object to interact with the database

using Microsoft.EntityFrameworkCore;
using ShopOnline.Api.Data;
using ShopOnline.Api.Entities;
using ShopOnline.Api.Repositories.Contracts;

namespace ShopOnline.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {

        //This member variable is used for referencing shopOnlineDbContext object 
        private readonly ShopOnlineDbContext shopOnlineDbContext;
        
        //We need to inject object of type shopOnlineDbContext into this constructor, so we need to define a 
        //parameter with a contructor of type shopOnlineDbContext
        public ProductRepository(ShopOnlineDbContext shopOnlineDbContext)
        {
            this.shopOnlineDbContext = shopOnlineDbContext;
        }
        //This method returns all the categories from the categories table, IEnumerable collection of categories from db
        //Since we want the code to run async we have to use await
        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
            var categories = await this.shopOnlineDbContext.ProductCategories.ToListAsync();
            return categories;
        }
        
        public async Task<ProductCategory> GetCategory(int id)
        {
            var category = await this.shopOnlineDbContext.ProductCategories.SingleOrDefaultAsync(c => c.Id == id);
            return category;
        }
        //Here we retrive the data of a single item by using FindAsync method, by using id that mathes the id
        //of the item we are retrieving

        //We have to expose the functionallity to a calling client by implementing code that gets executed in action
        //to an appriopriate HttpGet request
        public async Task<Product> GetItem(int id)
        {
            var product = await this.shopOnlineDbContext.Products.FindAsync(id);
            return product;
        }
        //This method returns all the products from the products table, IEnumerable collection of products from db
        //Since we want the code to run async we have to use await
        public async Task<IEnumerable<Product>> GetItems()
        {
            var products = await this.shopOnlineDbContext.Products.ToListAsync();
            return products;
        }
    }
}
