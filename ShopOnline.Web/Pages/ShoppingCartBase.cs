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
        public IEnumerable<CartItemDTO> ShoppingCartItems { get; set; }
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


    }
}
