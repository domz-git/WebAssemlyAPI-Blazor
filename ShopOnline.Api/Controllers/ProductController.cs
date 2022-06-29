//This controller returns data to the calling client 

//Object of type ProductRepository is automatically injected into out controller class's constructor via dependency
//injection


using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Api.Extensions;
using ShopOnline.Api.Repositories.Contracts;
using ShopOnline.Models.DTOs;

namespace ShopOnline.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController:ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        //This method is used to GET products data and returning it to the client
        //Thats we are using HttpGet attribute
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetItems()
        {
            try
            {
                //Here we call productRepositorie's GetItems method to return IEnumerable colletion of type product
                //to this action method
                var products = await this.productRepository.GetItems();
                //Same for type productCategory
                var productCategories = await this.productRepository.GetCategories();

                if (products == null || productCategories == null)
                {
                    return NotFound();
                }
                else
                {
                    //Here we join the collection of product categories with the collection of products so that we
                    //can return a collection of productDTO's to the client which will include the category name

                    //Here we implement an extension method ConvertToDTO (located in Extensions folder) to return a collection
                    //type of productDTO's to this action method

                    var productDTOs = products.ConvertToDTO(productCategories);
                    return Ok(productDTOs);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Error retrieving data from database");
            }
        }
        //This method is used to GET one single product data and returning it to the client
        //Thats we are using HttpGet attribute with included parameter called id

        //This id representes the id value of the item we want to retrieve 

        //Within the [HttpGet] attribute we have to include appripriate root template information
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDTO>> GetItem(int id)
        {
            try
            {
                //Here we call productRepositorie's GetItem method to return of type product
                //to this action method
                var product = await this.productRepository.GetItem(id);

                if (product == null)
                {
                    return BadRequest();
                }
                else
                {
                    //Here we join the product to the product category with the collection of products so that we
                    //can return a collection of productDTO's to the client which will include the category name

                    //Here we implement an extension method ConvertToDTO (located in Extensions folder) to return a collection
                    //type of productDTO's to this action method
                    var productCategory = await this.productRepository.GetCategory(product.CategoryId);

                    var productDTO = product.ConvertToDTO(productCategory);

                    return Ok(productDTO);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Error retrieving data from database");
            }
        }
    }
}
