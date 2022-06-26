//We handle calls to the web API component with services
//These are cs classes that handle interactions with the web API component

using ShopOnline.Models.DTOs;

namespace ShopOnline.Web.Services.Contracts
{
    public interface IProductService
    {

        Task<IEnumerable<ProductDTO>> GetItems();
    }
}
