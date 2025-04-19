namespace ComedorInfantil.Gestion.Application.DataBase.AssigmentActivity.Command.DeleteAssignmentActivity
{
    public interface IDeleteAssignmentActivityCommand
    {
        /// <summary>
        /// Este metodo elimina un registro de una asignacion de una actividad si esta se encuentra en el sistema y no esta relacionada con otra tabla 
        /// </summary>
        /// <param name="assignmentActivityId">El id de asigancion que se va a eliminar</param>
        /// <param name="userId">El id de el usuario que esta eliminando esa asignacion</param>
        /// <returns>Retorna un <c>true</c> si la asigancion fuer elimnada con exito. Si no devuelve un <c>false</c></returns>
        Task<bool> Execute(int assignmentActivityId, int userId);
    }
}
