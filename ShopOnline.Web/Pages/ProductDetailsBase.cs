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

    }
}
