using ComedorInfantil.Gestion.Domain.Entities.Activity;
using ComedorInfantil.Gestion.Domain.Entities.Volunteer;

namespace ComedorInfantil.Gestion.Domain.Entities.AssignmentActitvity
{
    //Reglas del Negocio:
    //Vamos a poder asignar una actividad desde(POST): /activity/{activityId}/assigment-activity
    //Vamos a poder tomar las asiganaciones de una activadad desde(GET): /activity/{activityId}/assigment-activity
    //Vamos a poder tomar las asiganaciones de un voluntario desde(GET): /volunteers/{volunteerId}/assigment-activity
    //Vamos a poder eliminar la asiganacion de un voluntario desde(DELETE): /volunteers/{volunteerId}/assigment-activity/{assigmentId} 
    public class AssignmentActitvityEntity
    {
        public int AssignmentId { get; set; }
        public int VolunteerId { get; set; }
        public int ActivityId { get; set; }
        public DateTime AssignmentDate { get; set; }

        public VolunteerEntity Volunteer { get; set; }
        public ActivityEntity Activity { get; set; }
    }
}
