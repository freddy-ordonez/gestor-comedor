namespace ComedorInfantil.Gestion.Application.DataBase.ModuleByUser.Command.DeleteModuleByUser
{
    public interface IDeleteModuleByUserCommand
    {
        Task<bool> Execute(int moduleByUserId, int userId);

    }
}
