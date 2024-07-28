using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RESTful.API.Data;
using RESTful.API.DTOs;
using RESTful.API.Infrastructures.Request;
using RESTful.API.Infrastructures.Response;
using RESTful.API.Models.Entity;
using RESTful.API.Repositories.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RESTful.API.Repositories.Implementation
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context) { }

        #region Search Request
        public async Task<SearchResponse<ProductDTO>> SearchAsync(List<Filter> filters, SortByInfo sortBy, int pageNumber, int pageSize)
        {
            var productQuery = GetAll();

            productQuery = await FilterAsync(productQuery, filters);
            productQuery = await SortByAsync(new List<SortByInfo> { sortBy }, productQuery);

            var totalRows = await productQuery.CountAsync();

            var pagedQuery = productQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var products = await pagedQuery.ToListAsync();

            var result = products.Select(product => new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Category = product.Category,
                Quantity = product.Quantity
            }).ToList();

            var totalPages = (long)Math.Ceiling((double)totalRows / pageSize);

            var response = new SearchResponse<ProductDTO>
            {
                Data = result,
                CurrentPage = pageNumber,
                TotalPages = totalPages,
                RowsPerPage = pageSize,
                ToltalRows = totalRows
            };

            return response;
        }
        #endregion

        #region Filter
        public async Task<IQueryable<Product>> FilterAsync(IQueryable<Product> query, List<Filter> filters)
        {
            if (filters != null && filters.Any())
            {
                foreach (var filter in filters)
                {
                    if (string.IsNullOrEmpty(filter.FieldName) || string.IsNullOrEmpty(filter.Value) || string.IsNullOrEmpty(filter.Operation))
                        continue;

                    switch (filter.FieldName.ToLower())
                    {
                        case "Name":
                            switch (filter.Operation.ToLower())
                            {
                                case "equals":
                                    query = query.Where(x => x.Name!.Equals(filter.Value, StringComparison.OrdinalIgnoreCase));
                                    break;
                                case "contains":
                                    query = query.Where(x => x.Name!.Contains(filter.Value));
                                    break;
                            }
                            break;

                        case "Price":
                            if (double.TryParse(filter.Value, out double priceValue))
                            {
                                switch (filter.Operation.ToLower())
                                {
                                    case "equals":
                                        query = query.Where(x => x.Price == priceValue);
                                        break;
                                    case "greaterthan":
                                        query = query.Where(x => x.Price > priceValue);
                                        break;
                                    case "lessthan":
                                        query = query.Where(x => x.Price < priceValue);
                                        break;
                                }
                            }
                            break;
                    }
                }
            }
            return query;
        }
        #endregion

        #region Sort
        public async Task<IQueryable<Product>> SortByAsync(List<SortByInfo> sorts, IQueryable<Product> query)
        {
            if (sorts != null && sorts.Any())
            {
                foreach (var item in sorts)
                {
                    if (string.IsNullOrEmpty(item.FieldName))
                        continue;

                    if (item.Accending.HasValue)
                    {
                        if (item.Accending.Value)
                        {
                            query = query.OrderBy(x => EF.Property<object>(x, item.FieldName));
                        }
                        else
                        {
                            query = query.OrderByDescending(x => EF.Property<object>(x, item.FieldName));
                        }
                    }
                }
            }
            return query;
        }
        #endregion

        #region Total Record
        public async Task<int> TotalRecordAsync(List<Filter> filters)
        {
            var query = GetAll();

            query = await FilterAsync(query, filters);
            return await query.CountAsync();
        }
        #endregion
    }
}
