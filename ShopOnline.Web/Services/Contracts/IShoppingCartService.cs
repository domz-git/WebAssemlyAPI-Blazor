//Here we implement code that interacts with the action methods we created within out shoppingCartController
//class in out Web API project

using ShopOnline.Models.DTOs;

namespace ShopOnline.Web.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task<List<CartItemDTO>> GetItems(int userId);
        Task<CartItemDTO> AddItem(CartItemToAddDTO cartItemToAddDTO);
        Task<CartItemDTO> DeleteItem(int id);
        Task<CartItemDTO> UpdateItem(CartItemQuantityUpdateDTO cartItemQuantityUpdateDTO);
    }
}
