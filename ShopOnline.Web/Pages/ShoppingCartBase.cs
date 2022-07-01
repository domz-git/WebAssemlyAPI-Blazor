using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ShopOnline.Models.DTOs;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    public class ShoppingCartBase : ComponentBase
    {

        //We include IJSRuntime so that we can call functions for this class in out blazor component
        [Inject]
        public IJSRuntime Js { get; set; }

        //Here we write code for injecting an object of type IShoppingCartService into out class
        [Inject]
        public IShoppingCartService ShoppingCartService {get;set;}
        
        //Here we write code for referencing a collection of objects of type CartItemDTO,
        //This property will be used within the ShoppingCart.razor component for code that displays the 
        //relevant data to the user
        public List<CartItemDTO> ShoppingCartItems { get; set; }
        public string ErrorMessage { get; set; }
        protected string TotalPrice { get; set; }
        protected int TotalQuantity { get; set; }


        //Here we write the code to override the OnInicialized async method where we implemented the code
        //for asigning the relevant collection of items returned from the server to the ShoppingCartItems
        //property
        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
                CalculateCartSummaryTotals();
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
            CalculateCartSummaryTotals();


        }
        //The calling code will pass in the id of the shoppingCart item and the quantity value
        
        protected async Task UpdateQuantity_Click(int id, int quantity)
        {
            try
            {
                //If the quantity is greater than zeto, i.e. Valid, we call the UpdateItem on the 
                //shoppingCartService object to call the relevant server side code to update the database
                if (quantity > 0)
                {
                    var updateItemDTO = new CartItemQuantityUpdateDTO
                    {
                        CartItemId = id,
                        Quantity = quantity
                    };

                    var returnedUpdateItemDTO = await this.ShoppingCartService.UpdateItem(updateItemDTO);

                    UpdateItemTotalPrice(returnedUpdateItemDTO);
                    CalculateCartSummaryTotals();
                    await Js.InvokeVoidAsync("MakeUpdateQuantityButtonVisible", id, false);
                }
                //if the quantity that is passed is invalid we set the quantity field for the item to 1
                else
                {
                    var item = this.ShoppingCartItems.FirstOrDefault(i => i.Id == id);

                    if (item != null)
                    {
                        item.Quantity = 1;
                        item.TotalPrice = item.Price;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected async Task UpdateQuantity_Input(int id)
        {
            await Js.InvokeVoidAsync("MakeUpdateQuantityButtonVisible", id, true);
        }
        private void UpdateItemTotalPrice(CartItemDTO cartItemDTO)
        {
            var item = GetCartItem(cartItemDTO.Id);

            if (item != null)
            {
                item.TotalPrice = cartItemDTO.Price * cartItemDTO.Quantity;
            }
        }
        private void CalculateCartSummaryTotals()
        {
            SetTotalPrice();
            SetTotalQuantity();
        }

        private void SetTotalPrice()
        {
            TotalPrice = this.ShoppingCartItems.Sum(p => p.TotalPrice).ToString("C");
        }
        private void SetTotalQuantity()
        {
            TotalQuantity = this.ShoppingCartItems.Sum(q => q.Quantity);
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
