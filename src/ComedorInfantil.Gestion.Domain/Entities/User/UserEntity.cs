using ComedorInfantil.Gestion.Domain.Entities.Audit;
using ComedorInfantil.Gestion.Domain.Entities.ModuleForUser;

namespace ComedorInfantil.Gestion.Domain.Entities.User
{
    //Reglas de Negocio:
    //Vamos a poder crear un usuario desde(POST): /users
    //Vamos a poder tomar todos los usuarios desde(GET): /users
    //Vamos a poder actualizar un usuario desde(PUT): /users/{userId}
    //Vamos a poder eliminar un usuario desde(DELETE): /user/{userId}
    public class UserEntity
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }

        public ICollection<AuditEntity> Audits { get; set; }
        public ICollection<ModuleForUserEntity> ModuleForUserEntities { get; set; }
    }
}
