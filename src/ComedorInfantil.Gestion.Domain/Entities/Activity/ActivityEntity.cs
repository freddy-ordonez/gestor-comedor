using ComedorInfantil.Gestion.Domain.Entities.AssignmentActitvity;
using System.Collections.ObjectModel;

namespace ComedorInfantil.Gestion.Domain.Entities.Activity
{
    //Reglas de Negocio:
    //Vamos a poder crear una actividad desde(POST): /activities
    //Vamos a poder tomar todos los actividades desde(GET): /activities
    //Vamos a poder actualizar un actividad desde(PUT): /activities/{activityId}
    //Vamos a poder eliminar un actividad desde(DELETE): /activities/{activityId}
    //Vamos a poder tomar una actividad desde(GET): /activities/{activityId}
    public class ActivityEntity
    {
        public int ActivityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Collection<AssignmentActitvityEntity> Assignments { get; set; }
    }
}
