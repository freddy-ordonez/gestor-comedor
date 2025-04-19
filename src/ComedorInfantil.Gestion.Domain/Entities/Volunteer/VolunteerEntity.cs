using ComedorInfantil.Gestion.Domain.Entities.AssignmentActitvity;
using ComedorInfantil.Gestion.Domain.Entities.TypeIdentification;

namespace ComedorInfantil.Gestion.Domain.Entities.Volunteer
{
    //Reglas de Negocio:
    //Vamos a poder crear un voluntatio desde(POST): /volunteers
    //Vamos a poder tomar todos los voluntatios desde(GET): /volunteers
    //Vamos a poder actualizar un voluntatio desde(PUT): /volunteers/{volunteerId}
    //Vamos a poder eliminar un voluntatio desde(DELETE): /volunteers/{volunteerId}
    public class VolunteerEntity
    {
        public int VolunteerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Identification { get; set; }
        public string Phone { get; set; }
        public string Availability { get; set; }
        public string Status { get; set; }
        public int TypeIdentification { get; set; }

        public ICollection<AssignmentActitvityEntity> Assigments { get; set; }
        public TypeIdentificationEntity TypeIdentificationEntity { get; set; }

    }
}
