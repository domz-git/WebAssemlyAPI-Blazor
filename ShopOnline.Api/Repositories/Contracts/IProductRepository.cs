//We are using a repository design pattern to abstract the data handling

//Repositories are classes or components that encapsulate the logic required to access data sources

//This interface is implemented by ProductRepository class

using ShopOnline.Api.Entities;
namespace ShopOnline.Api.Repositories.Contracts
{
    public interface IProductRepository
    {
        //These methods return generic task objects

        //This method returns IEnumerable collection of type Product
        //A type IEnumerable collection is passed as a type argument to the Task object
        //This is so that method that implements this method definition can run asyncronously
        Task<IEnumerable<Product>> GetItems();
        //This method follows a similar pattern
        Task<IEnumerable<ProductCategory>> GetCategories();
        Task<Product> GetItem(int id);
        Task<ProductCategory> GetCategory(int id);
    }
}
