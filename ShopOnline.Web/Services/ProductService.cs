using ShopOnline.Models.DTOs;
using ShopOnline.Web.Services.Contracts;
using System.Net.Http.Json;

namespace ShopOnline.Web.Services
{
    //This class has to be registered for dependency injection in the program.cs

    public class ProductService : IProductService
    {
        private readonly HttpClient httpClinet;

        //Using the constructor we inject the Httpclient object into this class (ProductService.cs)

        public ProductService(HttpClient httpClinet)
        {
            this.httpClinet = httpClinet;
        }

        public async Task<ProductDTO> GetItem(int id)
        {
            try
            {
                var response = await httpClinet.GetAsync($"api/Product/{id}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode==System.Net.HttpStatusCode.NoContent)
                    {
                        return default (ProductDTO);
                    }
                    return await response.Content.ReadFromJsonAsync<ProductDTO>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
                
            }
            catch (Exception)
            {
                //Log excetion
                throw;
            }
        }

        public async Task<IEnumerable<ProductDTO>> GetItems()
        {
            try
            {
                //We are using generic GetFromJsonAsync method to call for the appropriate action method within
                //web API component

                //We are passing a type argument which is IEnumerable collection of type ProductDTO to the 
                //GetFromJsonAsync method because we want for our GetFromJsonAsync method to return an
                //IEnumerable collection of type ProductDTO

                //This method will translate the data which will be in json format, return from web API component
                //into an object of type IEnumerable which is strongly typed with the type ProductDTO

                //We are also passing a string which says where the collection of resources, that we want to
                //retrieve from out web API component, can be found

                //The web API will invoke the GetItem method within the ProductController class of web API
                //component based on that string information ("api/Product")
                var response = await this.httpClinet.GetAsync("api/Product");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductDTO>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductDTO>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
                
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
