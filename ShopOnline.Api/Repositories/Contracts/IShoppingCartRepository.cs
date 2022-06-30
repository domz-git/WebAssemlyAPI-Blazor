//We are using a repository design pattern to abstract the data handling

//Repositories are classes or components that encapsulate the logic required to access data sources

//This interface is implemented by ShoppingCartRepository class

using ShopOnline.Api.Entities;
using ShopOnline.Models.DTOs;

namespace ShopOnline.Api.Repositories.Contracts
{
    public interface IShoppingCartRepository
    {
        Task<CartItem> AddItem(CartItemToAddDTO cartItemToAddDTO);
        Task<CartItem> UpdateQuantity(int id, CartItemQuantityUpdateDTO cartItemQuantityUpdateDTO);
        Task<CartItem> DeleteItem(int id);
        Task<CartItem> GetItem(int id);
        Task<IEnumerable<CartItem>> GetItems(int userId); 
    }
}
