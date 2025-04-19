using ComedorInfantil.Gestion.Application.DTOs.AssigmentAcivity;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.AssigmentActivity.Queries.GetAssignmentForVolunteer
{
    internal class GetAssignmentForVolunteerQuerie : IGetAssignmentForVolunteerQuerie
    {
        private readonly IDataBaseService _dataBaseService;

        public GetAssignmentForVolunteerQuerie(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }

        public async Task<List<AssigmentForVolunteerDTO>> Execute(int volunteerId)
        {
            var listAssigmentForVolunteer = await (from assignment in _dataBaseService.AssignmentActitvities
                                                   join activity in _dataBaseService.Activities
                                                   on assignment.ActivityId equals activity.ActivityId
                                                   join volunteer in _dataBaseService.Volunteers
                                                   on assignment.VolunteerId equals volunteer.VolunteerId
                                                   where assignment.VolunteerId == volunteerId
                                                   select new AssigmentForVolunteerDTO
                                                   {
                                                       AssignmentId = assignment.AssignmentId,
                                                       DescriptionActivity = activity.Description,
                                                       NameActivity = activity.Name,
                                                       StartDate = activity.StartDate,
                                                   }).ToListAsync();

            return listAssigmentForVolunteer;
        }
    }
}
