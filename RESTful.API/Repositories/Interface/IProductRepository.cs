using RESTful.API.DTOs;
using RESTful.API.Infrastructures.Request;
using RESTful.API.Infrastructures.Response;
using RESTful.API.Models.Entity;

namespace RESTful.API.Repositories.Interface
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IQueryable<Product>> SortByAsync(List<SortByInfo> sorts, IQueryable<Product> query);
        Task<IQueryable<Product>> FilterAsync(IQueryable<Product> query, List<Filter> filters);
        Task<SearchResponse<ProductDTO>> SearchAsync(List<Filter> filters, SortByInfo sortBy, int pageNumber, int pageSize);
        Task<int> TotalRecordAsync(List<Filter> filters);
    }
}
