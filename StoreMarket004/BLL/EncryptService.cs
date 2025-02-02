﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using StoreMarket004.BLL.Abstractions;
using System.Text;

namespace StoreMarket004.BLL
{
    public class EncryptService : IEncryptService
    {
        public byte[] GenerateSalt() => Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());

        public byte[] HashPassword(string password, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 512 /8
                );
        }
    }
}
