using Microsoft.AspNetCore.Components;
using ShopOnline.Models.DTOs;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    //For this class to be inherited by the Products razor component, it has to inherit from class ComponentBase

    public class ProductsBase:ComponentBase
    {
        //Here we create a property to enable dependency injection of an object of type IProductService into
        //Product.razor component

        [Inject]
        public IProductService ProductService { get; set; }

        //Next, we create a public property to expose an IEnumerable collection of objects of type ProductDTO
        public IEnumerable<ProductDTO> Products { get; set; }

        //Now we need our code that retrieves the product data from the server, web API component, to run when
        //Products.razor component is first invoked 

        //We do this by overriding a function named OnInicializedAsync

        protected override async Task OnInitializedAsync()
        {
            Products = await ProductService.GetItems();
        }

    }
}
