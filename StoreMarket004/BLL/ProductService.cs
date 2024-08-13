using AutoMapper;
using Contracts.Requests;
using Contracts.Responses;
using Microsoft.Extensions.Caching.Memory;
using StoreMarket004.BLL.Abstractions;
using StoreMarket004.DAL.Contexts;
using StoreMarket004.DAL.Models;

namespace StoreMarket004.BLL
{
    public class ProductService : IProductService
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;
        public readonly IMemoryCache _cache;
        private readonly ITokenService _tokenService;

        public ProductService(StoreContext context, IMapper mapper, IMemoryCache cache, ITokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
            _tokenService = tokenService;
        }
        public int AddProduct(ProductCreateRequest request, string token)
        {
            var entity = _mapper.Map<Product>(request);
            var roleName = _tokenService.GetRoleNameFromToken(token);
            if (roleName == RoleType.Administrator.ToString())
            {
                _context.Products.Add(entity);
                _context.SaveChanges();
                _cache.Remove("products");
                return entity.Id;
            }
            return -1;
        }

        public ProductResponse? GetProductById(int id)
        {
            var result = _context.Products.FirstOrDefault(x => x.Id == id);
            if (result == null) { return null; }
            return _mapper.Map<ProductResponse>(result);
        }

        public IEnumerable<ProductResponse> GetProducts()
        {
            if (_cache.TryGetValue("products", out IEnumerable<ProductResponse>? result))
            {
                return result!;
            }
            IEnumerable<ProductResponse> products = _context.Products.Select(_mapper.Map<ProductResponse>).ToList();
            _cache.Set("products", products, TimeSpan.FromMinutes(30));

            return products;

        }

        public bool DeleteProduct(int id, string token)
        {
            var roleName = _tokenService.GetRoleNameFromToken(token);
            if (roleName == RoleType.Administrator.ToString())
            {
                var result = _context.Products.FirstOrDefault(x => x.Id == id);

                if (result != null)
                {
                    _context.Products.Remove(result);
                    _context.SaveChanges();
                    return true;
                }
                else { return false; }
            }
            return false;
        }

        public bool UpdateProductPrice(int id, decimal price, string token)
        {
            var roleName = _tokenService.GetRoleNameFromToken(token);
            if (roleName == RoleType.Administrator.ToString())
            {
                var entity = _context.Products.FirstOrDefault(x => x.Id == id);

                if (entity != null)
                {
                    entity.Price = price;
                    _context.Products.Update(entity);
                    _context.SaveChanges();
                    return true;
                }
                else { return false; }
            }
            return false;
        }

        /* public ProductResponse? GetProductByName(string name)
         {
             var result = _context.Products.FirstOrDefault(x => x.Name == name);
             if (result == null) { return null; }
             return _mapper.Map<ProductResponse>(result);
         }*/
    }
}
