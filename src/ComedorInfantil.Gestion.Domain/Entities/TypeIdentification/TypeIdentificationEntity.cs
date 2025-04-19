using ComedorInfantil.Gestion.Domain.Entities.Volunteer;

namespace ComedorInfantil.Gestion.Domain.Entities.TypeIdentification
{
    //Reglas de negocio:
    //Vamos a poder tomar todos los tipos de identificacion desde(GET): /type-identifications
    public class TypeIdentificationEntity
    {
        public int TypeIdentificationId { get; set; }
        public string TypeIdentification { get; set; }
        public string Status { get; set; }

        public VolunteerEntity Volunteer { get; set; }
    }
}
