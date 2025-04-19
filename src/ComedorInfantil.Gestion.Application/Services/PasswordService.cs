using ComedorInfantil.Gestion.Application.Interfaces;
using ComedorInfantil.Gestion.Domain.Entities.User;
using Microsoft.AspNetCore.Identity;

namespace ComedorInfantil.Gestion.Application.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordHasher<UserEntity> _passwordHasher = new();

        public string HashPassword(string plainPassword, UserEntity user)
        {
            return _passwordHasher.HashPassword(user, plainPassword);
        }

        public bool VerifyPassword(string hashedPassword, string plainPassword, UserEntity user)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, hashedPassword, plainPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
