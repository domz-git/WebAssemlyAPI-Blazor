//This classes are based on the entity classes but are differently shaped based on the data that needs to be pased
//between server and client

//We need to add project reference on ShopOnline.Models to the dependencies on the ShopOnline.Api

//These DTO classes need to have a public access modifier
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Models.DTOs
{
    public  class ProductDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

    }
}
