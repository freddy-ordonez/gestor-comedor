using AutoMapper;
using ComedorInfantil.Gestion.Application.DTOs.TypeIdentification;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase.TypeIdentification.Queries.GetAllTypeIdentification
{
    public class GetAllTypeIdentificationQuerie : IGetAllTypeIdentificationQuerie
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;

        public GetAllTypeIdentificationQuerie(IDataBaseService dataBaseService, IMapper mapper)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
        }

        public async Task<List<TypeIdentificationDTO>> Execute()
        {
            var listTypeIdentifications = await _dataBaseService.TypeIdentifications
                .Where(x => x.Status == "Activo")
                .ToListAsync();

            return _mapper.Map<List<TypeIdentificationDTO>>(listTypeIdentifications);
        }
    }
}
