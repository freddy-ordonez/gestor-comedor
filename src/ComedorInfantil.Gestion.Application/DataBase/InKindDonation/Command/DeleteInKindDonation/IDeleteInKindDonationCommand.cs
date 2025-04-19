namespace ComedorInfantil.Gestion.Application.DataBase.InKindDonation.Command.DeleteInKindDonation
{
    public interface IDeleteInKindDonationCommand
    {
        /// <summary>
        /// Este metodo eliminada un registro de una donacion en especie si se encuentra en el sistema.
        /// </summary>
        /// <param name="inKindDonationId">El id de la donacion que se va a eliminar</param>
        /// <param name="userId">El id de el usuario que esta elimando el registro</param>
        /// <returns>Retorna un <c>true</c> si se elimino correctamente la donacion. Retorna un <c>false</c> si la donacion no se encuentra en el sistema</returns>
        Task<bool> Execute(int inKindDonationId, int userId);
    }
}
