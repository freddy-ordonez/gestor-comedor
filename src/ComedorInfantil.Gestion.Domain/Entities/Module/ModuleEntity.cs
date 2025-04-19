using ComedorInfantil.Gestion.Domain.Entities.ModuleForUser;

namespace ComedorInfantil.Gestion.Domain.Entities.Module
{
    //Reglas de negocio:
    //Vamos a poder tomar todos los modulos de un usuario desde: /users/{userId}/modules
    public class ModuleEntity
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string ClassCSS { get; set; }
        public string Link { get; set; }

        public ICollection<ModuleForUserEntity> ModuleForUserEntities { get; set; }
    }
}
