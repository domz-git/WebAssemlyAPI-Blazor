using Microsoft.AspNetCore.Components;
using ShopOnline.Models.DTOs;


namespace ShopOnline.Web.Pages
{
    public class DisplayProductsBase : ComponentBase
    {
        //We want a parent Razor compononent to be able to pass an IEnumerable collection of objectsof type ProductDTO
        //to our DisplayProducts.razor child component

        //We achive this by creating a parameter property
        [Parameter]
        public IEnumerable<ProductDTO>? Products { get; set; }
    }
}
