using Microsoft.AspNetCore.Components;
using ShopOnline.Models.DTOs;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    public class ShoppingCartBase : ComponentBase
    {
        //Here we write code for injecting an object of type IShoppingCartService into out class
        [Inject]
        public IShoppingCartService ShoppingCartService {get;set;}
        
        //Here we write code for referencing a collection of objects of type CartItemDTO,
        //This property will be used within the ShoppingCart.razor component for code that displays the 
        //relevant data to the user
        public List<CartItemDTO> ShoppingCartItems { get; set; }
        public string ErrorMessage { get; set; }

        //Here we write the code to override the OnInicialized async method where we implemented the code
        //for asigning the relevant collection of items returned from the server to the ShoppingCartItems
        //property
        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
            }
        }

        //Here we write the method that will calll the DeleteItem method we defined in ShoppingCartService
        //class

        //We need the result of this method to be reflected on the user interface which means we have to 
        // rerender ShoppingCart.razor component 

        //We do this by deleting the relevant cardItem object from the client side collection of cartItems
        //referenced by our shoppingCartItems property, thisis the RemoveCartItem() method comes in
        protected async Task DeleteCartItem_Click(int id)
        {
            var cartItemDTO = await ShoppingCartService.DeleteItem(id);

            RemoveCartItem(id);


        }
        private CartItemDTO GetCartItem(int id)
        {
            return ShoppingCartItems.FirstOrDefault(i => i.Id == id);
        }
        private void RemoveCartItem(int id)
        {
            var cartItemDTO = GetCartItem(id);

            ShoppingCartItems.Remove(cartItemDTO);
        }

    }
}
