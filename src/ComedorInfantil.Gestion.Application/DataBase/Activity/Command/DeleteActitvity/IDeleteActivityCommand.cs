namespace ComedorInfantil.Gestion.Application.DataBase.Activity.Command.DeleteActitvity
{
    public interface IDeleteActivityCommand
    {
        /// <summary>
        /// Este metodo elimina un registro de una actividad si esta se encuentra en el sistema y no esta relacionada con otra tabla.
        /// </summary>
        /// <param name="activityId">Id de la actividad a eliminar</param>
        /// <param name="userId">Id del usuario que esta eliminando el registro</param>
        /// <returns>
        /// Retorna un <see cref="int"/>.
        /// -1 si la actividad se encuentra relacionada en el sistema.
        /// 1 si la actividad no se encuentra en el sistema.
        /// 0 si se realizo con exito la eliminacion del registro.
        /// </returns>
        Task<int> Execute(int activityId, int userId);
    }
}
