using AutoMapper;
using Contracts.Requests;
using Contracts.Responses;
using StoreMarket004.DAL.Models;

namespace StoreMarket004.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() { 
            CreateMap<Product, ProductResponse>(MemberList.Destination).ReverseMap();
            CreateMap<Product, ProductCreateRequest>(MemberList.Destination).ReverseMap();
            CreateMap<Product, ProductUpdateRequest>(MemberList.Destination).ReverseMap();

            CreateMap<Category, CategoryResponse>(MemberList.Destination).ReverseMap();
            CreateMap<Category, CategoryCreateRequest>(MemberList.Destination).ReverseMap();
            CreateMap<Category, CategoryUpdateRequest>(MemberList.Destination).ReverseMap();

            CreateMap<Store, StoreResponse>(MemberList.Destination).ReverseMap();
            CreateMap<Store, StoreCreateRequest>(MemberList.Destination).ReverseMap();
            CreateMap<Store, StoreUpdateRequest>(MemberList.Destination).ReverseMap();

            CreateMap<ProductStore, ProductStoreCreateRequest>(MemberList.Destination).ReverseMap();

            CreateMap<User, UserAuthRespose>(MemberList.Destination).ReverseMap();
            CreateMap<User, UserAuthRequest>(MemberList.Destination).ReverseMap();
        }
    }
}
