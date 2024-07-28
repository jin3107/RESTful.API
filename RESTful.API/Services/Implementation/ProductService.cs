using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RESTful.API.DTOs;
using RESTful.API.Models.Entity;
using RESTful.API.Repositories.Interface;
using RESTful.API.Services.Interface;

namespace RESTful.API.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task AddProductAsync(ProductDTO productDTO)
        {
            var newProduct = _mapper.Map<Product>(productDTO);
            newProduct.Id = Guid.NewGuid();
            await _repository.AddAsync(newProduct);
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var deleteProduct = await _repository.GetByIdAsync(id);
            if (deleteProduct == null)
            {
                throw new Exception($"Product Id does not found");
            }

            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var products = await _repository.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> GetProductByIdAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task UpdateProductAsync(ProductDTO productDTO)
        {
            var exisProduct = await _repository.GetByIdAsync(productDTO.Id);
            if (exisProduct == null)
            {
                throw new Exception($"Product Id does not found");
            }

            //var product = _mapper.Map<Product>(productDTO);
            //await _repository.UpdateAsync(product);
            _mapper.Map(productDTO, exisProduct);
            await _repository.UpdateAsync(exisProduct);
        }
    }
}
