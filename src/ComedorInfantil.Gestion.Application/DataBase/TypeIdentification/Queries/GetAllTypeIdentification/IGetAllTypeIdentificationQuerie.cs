using ComedorInfantil.Gestion.Application.DTOs.TypeIdentification;

namespace ComedorInfantil.Gestion.Application.DataBase.TypeIdentification.Queries.GetAllTypeIdentification
{
    public interface IGetAllTypeIdentificationQuerie
    {
        Task<List<TypeIdentificationDTO>> Execute();
    }
}
