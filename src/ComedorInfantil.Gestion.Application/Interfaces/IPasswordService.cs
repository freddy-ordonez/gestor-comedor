using ComedorInfantil.Gestion.Domain.Entities.User;

namespace ComedorInfantil.Gestion.Application.Interfaces
{
    public interface IPasswordService
    {
        string HashPassword(string plainPassword, UserEntity user);
        bool VerifyPassword(string hashedPassword, string plainPassword, UserEntity user);
    }
}
