//This class must be static

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

        //We want to return an IEnumerable collection of objects of type productDTO to the calling code
        //we do this with ConvertToDTO method

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
        //Convert an object of type product into object of type productDTO
        public static ProductDTO ConvertToDTO(this Product product, ProductCategory productCategory)
        {
            return new ProductDTO
            {
                Id = product.Id,
                Name = productCategory.Name,
                Description = product.Description,
                ImageURL = product.ImageURL,
                Price = product.Price,
                Quantity = product.Quantity,
                CategoryId = product.CategoryId,
                CategoryName = productCategory.Name
            };
        }
        //LINQ querry to join an IEnumerable collection of objects of type CartItem with an IEnumerable
        //collection of objects of type Product into an IEnumerabe collection of objects of type CartItemDTO
        public static IEnumerable<CartItemDTO> ConvertToDTO (this IEnumerable<CartItem> cartItems,
                                                             IEnumerable<Product> products)
        {
            return (from cartItem in cartItems
                    join product in products
                    on cartItem.ProductId equals product.Id
                    select new CartItemDTO
                    {
                        Id = cartItem.Id,
                        ProductId = cartItem.ProductId,
                        ProductName = product.Name,
                        ProductDescription = product.Description,
                        ProductImageURL = product.ImageURL,
                        Price = product.Price,
                        CartId = cartItem.Id,
                        Quantity = cartItem.Quantity,
                        TotalPrice = product.Price * cartItem.Quantity
                    }).ToList();
        }

        //Convert an object of type cartItem into object of type cartItemDTO
        public static CartItemDTO ConvertToDTO(this CartItem cartItem, Product product)
        {
            return new CartItemDTO
            {
                Id = cartItem.Id,
                ProductId = cartItem.ProductId,
                ProductName = product.Name,
                ProductDescription = product.Description,
                ProductImageURL = product.ImageURL,
                Price = product.Price,
                CartId = cartItem.Id,
                Quantity = cartItem.Quantity,
                TotalPrice = product.Price * cartItem.Quantity
            };
        }
    }
}
