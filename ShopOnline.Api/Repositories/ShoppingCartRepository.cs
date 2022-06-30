using Microsoft.EntityFrameworkCore;
using ShopOnline.Api.Data;
using ShopOnline.Api.Entities;
using ShopOnline.Api.Repositories.Contracts;
using ShopOnline.Models.DTOs;

namespace ShopOnline.Api.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ShopOnlineDbContext shopOnlineDbContext;

        public ShoppingCartRepository(ShopOnlineDbContext shopOnlineDbContext)
        {
            this.shopOnlineDbContext = shopOnlineDbContext;
        }

        //We implement this method to check whether an item has been accidentaly added twice
        private async Task<bool> CartItemExists(int cartId, int productId)
        {
            return await this.shopOnlineDbContext.CartItems.AnyAsync(c => c.CartId == cartId &&
                                                                        c.ProductId == productId);
        }
        public async Task<CartItem> AddItem(CartItemToAddDTO cartItemToAddDTO)
        {
            //Checking whether an item has already been added using the above method
            if(await CartItemExists(cartItemToAddDTO.CartId, cartItemToAddDTO.ProductId) == false)
            {

                //Next we check whether the product that the user is trying to add to the cart actually
                //exists using LINQ querry

                var item = await (from product in this.shopOnlineDbContext.Products
                                  where product.Id == cartItemToAddDTO.ProductId
                                  select new CartItem
                                  {
                                      CartId = cartItemToAddDTO.CartId,
                                      ProductId = product.Id,
                                      Quantity = cartItemToAddDTO.Quantity
                                  }).SingleOrDefaultAsync();

                //If it isn't null, thats means that it exists, and we want the code to add the relevant product
                //to the CartItem database table
                if (item != null)
                {
                    var result = await this.shopOnlineDbContext.CartItems.AddAsync(item);
                    await this.shopOnlineDbContext.SaveChangesAsync();
                    return result.Entity;
                }

            }

                return null;
        }

        public Task DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CartItem> GetItem(int id)
        {
            //LINQ querry that returns the results which is data containing to the perticular product
            //currently within the relevant usets shopping cart 

            return await (from cart in this.shopOnlineDbContext.Carts
                          join cartItem in this.shopOnlineDbContext.CartItems
                          on cart.Id equals cartItem.CartId
                          where cartItem.Id == id
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              ProductId = cartItem.ProductId,
                              Quantity = cartItem.Quantity,
                              CartId = cartItem.CartId
                          }).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CartItem>> GetItems(int userId)
        {
            //Using LINQ querry to check products currenty stored in the users shopping cart

            return await (from cart in this.shopOnlineDbContext.Carts
                          join cartItem in this.shopOnlineDbContext.CartItems
                          on cart.Id equals cartItem.CartId
                          where cart.UserId == userId
                          select new CartItem
                          {
                              Id = cart.Id,
                              ProductId = cartItem.ProductId,
                              Quantity = cartItem.Quantity,
                              CartId = cartItem.Id
                          }).ToListAsync();

        }

        public Task<CartItem> UpdateQuantity(int id, CartItemQuantityUpdateDTO cartItemQuantityUpdateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
