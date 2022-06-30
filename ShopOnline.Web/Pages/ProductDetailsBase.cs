using Microsoft.AspNetCore.Components;
using ShopOnline.Models.DTOs;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    public class ProductDetailsBase : ComponentBase
    {
        //We want a parent Razor compononent to be able to pass an IEnumerable collection of objectsof type ProductDTO
        //to our DisplayProducts.razor child component

        //We achive this by creating a parameter property
        [Parameter]
        public int Id { get; set; }
        [Inject]
        public IProductService ProductService { get; set; }
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }
        //We use an object of navigation manager to navigate the user to the relevant cart screen
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public ProductDTO Product { get; set; }
        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Product = await ProductService.GetItem(Id);
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
            }
        }
        // This is a click event handler method that is implemented when the user click "Add to cart button"
        //located in ProductDetauls.razor component
        protected async Task AddToCard_Click(CartItemToAddDTO cartItemToAddDTO)
        {
            //Here we call the AddItem() method on the injected ShoppingCartService object to add a product
            //to the shopping cart
            try
            {
                var cartItemDTO = await ShoppingCartService.AddItem(cartItemToAddDTO);

                //Here we invoke the NavigateTo() method on the NavigationManager object to navigate the user
                //to the shopping cart screen
                NavigationManager.NavigateTo("/ShoppingCart");
            }
            catch (Exception)
            {

                //Log exception
            }
        }

    }
}
