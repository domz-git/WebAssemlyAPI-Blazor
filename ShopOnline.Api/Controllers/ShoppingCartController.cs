//This controller returns data to the calling client

//Objects of type ShoppingCartRepsitory and ProductRepository are automatically injected into this class'es
//contructor via dependency injection

using Microsoft.AspNetCore.Mvc;
using ShopOnline.Api.Extensions;
using ShopOnline.Api.Repositories.Contracts;
using ShopOnline.Models.DTOs;

namespace ShopOnline.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController:ControllerBase
    {
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IProductRepository productRepository;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.productRepository = productRepository;
        }

        //This method is used to GET items currenty stored within the relevant shopping cart and return
        //it to the client

        //Thats why we are using HttpGet attribute
        [HttpGet]
        [Route("{userId}/GetItems")]
        public async Task<ActionResult<IEnumerable<CartItemDTO>>> GetItems(int userId)
        {
            try
            {
                var cartItems = await this.shoppingCartRepository.GetItems(userId);

                if(cartItems == null)
                {
                    return NoContent();
                }

                var products = await this.productRepository.GetItems();
                if(products == null)
                {
                    throw new Exception("No products exist in the system.");
                }

                //We dont want to return a collection of objects of cartItem, we want to return a collection 
                //of objects of type CartItemDTO

                //We use productrepository object to retrive a collection of objects of type product, we 
                //then join the collection of cartItem objects with the collection of objects of type product
                //using a LINQ querry and return a collection of objects of type CartItemDTO

                var cartItemsDTO = cartItems.ConvertToDTO(products);
                return Ok(cartItemsDTO);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //This method is used to GET one single item data and returning it to the client
        //Thats we are using HttpGet attribute with included parameter called id

        //This id representes the id value of the item we want to retrieve 

        //Within the [HttpGet] attribute we have to include appripriate root template information
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CartItemDTO>> GetItem(int id)
        {
            try
            {

                //Here we call shoppingCartRepositorie's GetItem method to return of type item
                //to this action method
                var cartItem = await this.shoppingCartRepository.GetItem(id);

                if (cartItem == null)
                {
                    return NotFound();
                }

                var product = await productRepository.GetItem(cartItem.ProductId);

                if (product == null)
                {
                    return NotFound();
                }

                //We have to convert the object of type cartItem to an object of cartItemDTO

                var cartItemDTO = cartItem.ConvertToDTO(product);

                return Ok(cartItemDTO);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CartItemDTO>> PostItem([FromBody] CartItemToAddDTO cartItemToAddDTO)
        {
            try
            {
                var newCartItem = await this.shoppingCartRepository.AddItem(cartItemToAddDTO);

                if (newCartItem == null)
                {
                    return NoContent();
                }

                var product = await productRepository.GetItem(newCartItem.ProductId);

                if (product == null)
                {
                    throw new Exception($"Something went wrong when attempting to retrieve product (productId: ({cartItemToAddDTO.ProductId}))");
                }
                //We want to return newly added item of type CartItemDTO so we can call the same code that
                //we implemented for GetItem() method

                var newcartItemDTO = newCartItem.ConvertToDTO(product);

                //It is standard practice for a POST method to return a location of the resource where the 
                //newly added item can be found 

                //This location will be returned in the head of HTTP response return from this method

                //That is why we use CreateAtAction method

                //The resource can be found at URI ateining to GetItem method and we are including id of the 
                //newly added resource and the newly added object of type newCartItemDTO 
                return CreatedAtAction(nameof(GetItem), new { id = newcartItemDTO.Id });

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        //Here we use HttpDelete, and declare root template information by passing in an appropriate string
        //argument to the HttpDelete atribute, id needs to be part of URI
        
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CartItemDTO>> DeleteItem(int id)
        {
            try
            {
                var cartItem = await this.shoppingCartRepository.DeleteItem(id);

                if (cartItem == null)
                {

                    return NotFound();
                }
                var product = await this.productRepository.GetItem(cartItem.ProductId);

                if (product == null)
                {
                    return NotFound();
                }
                var cartItemDTO = cartItem.ConvertToDTO(product);

                return Ok(cartItemDTO);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        //Here we expose the UpdateQuantity method functionality to the client through an action method
        //We use HttpPatch because we want to partially update the resource

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<CartItemDTO>> UpdateQuantity(int id, CartItemQuantityUpdateDTO cartItemQuantityUpdateDTO)
        {
            try
            {
                //Here we call the UpdateQuantity() method od the shoppingCartRepository object with the 
                //quantity value passed into out action method by the client 
                var cartItem = await this.shoppingCartRepository.UpdateQuantity(id, cartItemQuantityUpdateDTO);

                if (cartItem == null)
                {
                    return NotFound();
                }

                var product = await this.productRepository.GetItem(cartItem.ProductId);

                var cartItemDTO = cartItem.ConvertToDTO(product);

                return Ok(cartItemDTO);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



    }
}
