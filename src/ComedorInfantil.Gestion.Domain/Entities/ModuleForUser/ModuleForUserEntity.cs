using ComedorInfantil.Gestion.Domain.Entities.Module;
using ComedorInfantil.Gestion.Domain.Entities.User;

namespace ComedorInfantil.Gestion.Domain.Entities.ModuleForUser
{
    //Reglas de negocio:
    //Vamos a poder traer todos los modulos de un usuario desde(GET): /users/{userId}/modules-for-user
    //Vamos a poder asignar modulos de un usuario desde(POST): /users/modules-for-user
    //Vamos a poder eliminar modulos de un usuario desde(DELETE): /users/{userId}/modules-for-user/{moduleId}
    public class ModuleForUserEntity
    {
        public int ModuleForUserId { get; set; }
        public int UserId { get; set; }
        public int ModuleId { get; set; }

        public UserEntity User { get; set; }
        public ModuleEntity Module { get; set; }
    }
}
