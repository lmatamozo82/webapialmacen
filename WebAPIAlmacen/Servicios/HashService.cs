﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using WebAPIAlmacen.DTOs;

namespace WebAPIAlmacen.Servicios
{
    public class HashService
    {
        public ResultadoHash Hash(string textoPlano)
        {
            var salt = new byte[16];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(salt);
            }

            return Hash(textoPlano, salt);
        }

        public ResultadoHash Hash(string textoPlano, byte[] salt)
        {
            var claveDerivada = KeyDerivation.Pbkdf2(password: textoPlano,
                salt: salt, prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 32);

            var hash = Convert.ToBase64String(claveDerivada);

            return new ResultadoHash()
            {
                Hash = hash,
                Salt = salt
            };
        }
    }

}
