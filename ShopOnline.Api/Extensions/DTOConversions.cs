//This class must be static

//We want to return an IEnumerable collection of objects of type productDTO to the calling code
//we do this with ConvertToDTO method


using ShopOnline.Api.Entities;
using ShopOnline.Models.DTOs;

namespace ShopOnline.Api.Extensions
{
    public static class DTOConversions
    {
        //The 1st parameter of an extension method must be the type of an object on which we want to be able to call
        //the ConvertToDTO extenstion method, has to have "this" keyword

        //The 2nd parameter is of a type that is an IEnumerable collection of type productCategory

        //This method, using the Linq querry, joins a collection of type product to a collection of type productCategory
        //and return a collection of type productDTO

        public static IEnumerable<ProductDTO> ConvertToDTO(this IEnumerable<Product> products, IEnumerable<ProductCategory> productCategories)
        {
            return (from product in products
                    join productCategory in productCategories
                    on product.CategoryId equals productCategory.Id
                    select new ProductDTO
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        Quantity = product.Quantity,
                        CategoryId = product.CategoryId,
                        CategoryName = productCategory.Name
                    }).ToList();
        }
    }
}
