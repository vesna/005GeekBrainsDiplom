using AutoMapper;
using Azure.Core;
using Contracts.Requests;
using Contracts.Responses;
using Microsoft.Extensions.Caching.Memory;
using StoreMarket004.BLL.Abstractions;
using StoreMarket004.DAL.Contexts;
using StoreMarket004.DAL.Models;

namespace StoreMarket004.BLL
{
    public class StoreService : IStoreService
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;
        public readonly IMemoryCache _cache;
        private readonly ITokenService _tokenService;

        public StoreService(StoreContext context, IMapper mapper, IMemoryCache cache, ITokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
            _tokenService = tokenService;
        }

        public int AddStore(StoreCreateRequest request, string token)
        {
            var roleName = _tokenService.GetRoleNameFromToken(token);
            if (roleName == RoleType.Administrator.ToString())
            {
                var entity = _mapper.Map<Store>(request);
                _context.Stores.Add(entity);
                _context.SaveChanges();
                _cache.Remove("stores");
                return entity.Id;
            }
            return -1;
        }

        public bool DeleteStore(int id, string token)
        {
            var roleName = _tokenService.GetRoleNameFromToken(token);
            if (roleName == RoleType.Administrator.ToString())
            {
                var result = _context.Stores.FirstOrDefault(x => x.Id == id);

                if (result != null)
                {
                    _context.Stores.Remove(result);
                    _context.SaveChanges();
                    return true;
                }
                else { return false; }
            }
            return false;
        }

        public StoreResponse? GetStoreById(int id)
        {
            var result = _context.Stores.FirstOrDefault(x => x.Id == id);
            if (result == null) { return null; }
            return _mapper.Map<StoreResponse>(result);
        }

        public IEnumerable<StoreResponse> GetStores()
        {
            if (_cache.TryGetValue("stores", out IEnumerable<StoreResponse>? result))
            {
                return result!;
            }
            IEnumerable<StoreResponse> stories = _context.Stores.Select(_mapper.Map<StoreResponse>).ToList();
            _cache.Set("stores", stories, TimeSpan.FromMinutes(30));
            return stories;
        }

        public bool UpdateStoreName(int id, string name, string token)
        {
            var roleName = _tokenService.GetRoleNameFromToken(token);
            if (roleName == RoleType.Administrator.ToString())
            {
                var entity = _context.Stores.FirstOrDefault(x => x.Id == id);

                if (entity != null)
                {
                    entity.Name = name;
                    _context.Stores.Update(entity);
                    _context.SaveChanges();
                    return true;
                }
                else { return false; }
            }
            return false;
        }

        public bool AddProductToStore(ProductStoreCreateRequest request, string token)
        {
            var roleName = _tokenService.GetRoleNameFromToken(token);
            if (roleName == RoleType.Administrator.ToString())
            {
                var entity = _mapper.Map<ProductStore>(request);
                var product = _context.Products.FirstOrDefault(x => x.Id == request.ProductId);
                var store = _context.Stores.FirstOrDefault(x => x.Id == request.StoreId);

                if (product == null || store == null) return false;
                _context.ProductStores.Add(entity);
                store.Count++;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteProductFromStore(int storeId, int productId, string token)
        {
            var roleName = _tokenService.GetRoleNameFromToken(token);
            if (roleName == RoleType.Administrator.ToString())
            {
                var result = _context.ProductStores.FirstOrDefault(x => x.ProductId == productId && x.StoreId == storeId);
                var store = _context.Stores.FirstOrDefault(x => x.Id == storeId);

                if (result != null && store != null)
                {
                    _context.ProductStores.Remove(result);
                    store.Count--;
                    _context.SaveChanges();
                    return true;
                }
                else { return false; }
            }
            return false;
        }
    }
}
