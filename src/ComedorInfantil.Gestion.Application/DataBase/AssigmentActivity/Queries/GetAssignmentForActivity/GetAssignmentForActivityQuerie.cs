using ComedorInfantil.Gestion.Application.DTOs.AssigmentAcivity;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.AssigmentActivity.Queries.GetAssignmentForActivity
{
    public class GetAssignmentForActivityQuerie : IGetAssignmentForActivityQuerie
    {
        private readonly IDataBaseService _dataBaseService;

        public GetAssignmentForActivityQuerie(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }

        public async Task<List<AssigmentForActivityDTO>> Execute(int activityId)
        {
            var listAssignmentForActivities = await (from assigment in _dataBaseService.AssignmentActitvities
                                                     join activity in _dataBaseService.Activities
                                                     on assigment.ActivityId equals activity.ActivityId
                                                     join volunteer in _dataBaseService.Volunteers
                                                     on assigment.VolunteerId equals volunteer.VolunteerId
                                                     where assigment.ActivityId == activityId
                                                     select new AssigmentForActivityDTO
                                                     {
                                                         AssignmentId = assigment.AssignmentId,
                                                         NameVolunteer = volunteer.FirstName
                                                                         + " "
                                                                         + volunteer.LastName,
                                                         PhoneVolunteer = volunteer.Phone,
                                                         StartDate = activity.StartDate,
                                                     }).ToListAsync();

            return listAssignmentForActivities;

        }
    }
}
