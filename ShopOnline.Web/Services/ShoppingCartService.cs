using Newtonsoft.Json;
using ShopOnline.Models.DTOs;
using ShopOnline.Web.Services.Contracts;
using System.Net.Http.Json;
using System.Text;

namespace ShopOnline.Web.Services
{
    //This class has to be registered for dependency injection in the program.cs
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly HttpClient httpClient;

        public ShoppingCartService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<CartItemDTO> AddItem(CartItemToAddDTO cartItemToAddDTO)
        {
            //This code calls the action method that we implemented in shoppingCartController class
            //
            try
            {
                var response = await httpClient.PostAsJsonAsync<CartItemToAddDTO>("api/ShoppingCart", cartItemToAddDTO);

                if (response.IsSuccessStatusCode)
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(CartItemDTO);
                    }
                    return await response.Content.ReadFromJsonAsync<CartItemDTO>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status:{response.StatusCode} - Message:{message}");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CartItemDTO> DeleteItem(int id)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/ShoppingCart/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CartItemDTO>();
                }
                return default(CartItemDTO);
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<List<CartItemDTO>> GetItems(int userId)
        {
            var response = await httpClient.GetAsync($"api/ShoppingCart/{userId}/GetItems");
            //This code places a http GET request to the GetItems action method and if content is returned,
            //returns the relevant content to the relevant Razor component
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<CartItemDTO>().ToList();
                }
                return await response.Content.ReadFromJsonAsync<List<CartItemDTO>>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status:{response.StatusCode} - Message:{message}");
            }
        }

        public async Task<CartItemDTO> UpdateItem(CartItemQuantityUpdateDTO cartItemQuantityUpdateDTO)
        {
            try
            {
                //First we have to serialize the DTO we want to pass to the server into json format
                var jsonRequest = JsonConvert.SerializeObject(cartItemQuantityUpdateDTO);
                
                //Then we create a object of type StringContent to that we can pass the reference data
                //in the appropraite format to the server
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

                //Here we all the PatchAsync method on our httpClient object and pass in the appropriate URI
                var response = await httpClient.PatchAsync($"api/ShoppingCart/{cartItemQuantityUpdateDTO.CartItemId}", content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CartItemDTO>();
                }

                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task UpdateQuantity(CartItemQuantityUpdateDTO updateItemDTO)
        {
            throw new NotImplementedException();
        }
    }
}
