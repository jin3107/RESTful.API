using RESTful.API.Data;
using RESTful.API.Models.Entity;
using RESTful.API.Repositories.Interface;

namespace RESTful.API.Repositories.Implementation
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context) { }
    }
}
