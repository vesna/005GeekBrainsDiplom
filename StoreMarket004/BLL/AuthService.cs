using AutoMapper;
using Contracts.Requests;
using Contracts.Responses;
using GreenDonut;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StoreMarket004.BLL.Abstractions;
using StoreMarket004.DAL.Contexts;
using StoreMarket004.DAL.Models;
using System.Data.Entity;

namespace StoreMarket004.BLL
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IEncryptService _encryptService;
        private readonly AuthContext _authContext;
        private readonly IMapper _mapper;

        public AuthService(ITokenService tokenService, AuthContext authContext, IEncryptService encryptService, IMapper mapper)
        {
            _tokenService = tokenService;
            _authContext = authContext;
            _encryptService = encryptService;
            _mapper = mapper;
        }

        public string? Login(string email, string password)
        {
            var user = _authContext.Users.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return null;
            }
            var pass= _encryptService.HashPassword(password, user.Salt);
            if(!user.Password.SequenceEqual(pass)) { 
                return null;
            }
            var role = _authContext.Roles.FirstOrDefault(x => x.Id == user.RoleId);
            var token = _tokenService.GenerateToken(user.Email, role.Name.ToString());
            return token;
        }

        public string? Register(UserAuthRequest request)
        {
            var user = _authContext.Users.FirstOrDefault(x => x.Email == request.Email);
            if (user is not null)
            {
                return null;
            }

            var role = _authContext.Roles.FirstOrDefault(x => x.Name == request.RoleName.ToString());
            var salt = _encryptService.GenerateSalt();
            user = new User
            {
                Email = request.Email,
                Salt = salt,
                Password = _encryptService.HashPassword(request.Password, salt),
                RoleId = role.Id
            };
            _authContext.Users.Add(user);
            _authContext.SaveChanges();
            var token = _tokenService.GenerateToken(user.Email, role.Name.ToString());
            return token;
        }
    }
}
