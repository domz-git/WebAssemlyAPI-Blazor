//This classes are based on the entity classes but are differently shaped based on the data that needs to be pased
//between server and client
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Models.DTOs
{
    public class CartItemQuantityUpdateDTO
    {
        public int CartItemId { get; set; }
        public int Quantity { get; set; }
    }
}
