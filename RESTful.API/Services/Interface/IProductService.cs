using RESTful.API.DTOs;
using RESTful.API.Infrastructures.Request;
using RESTful.API.Infrastructures.Response;

namespace RESTful.API.Services.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO> GetProductByIdAsync(Guid id);
        Task AddProductAsync(ProductDTO productDTO);
        Task UpdateProductAsync(ProductDTO productDTO);
        Task DeleteProductAsync(Guid id);
        Task<SearchResponse<ProductDTO>> SearchProductsAsync(List<Filter> filters, SortByInfo sortBy, int pageNumber, int pageSize);
    }
}
