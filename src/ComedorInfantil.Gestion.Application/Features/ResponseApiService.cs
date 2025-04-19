using ComedorInfantil.Gestion.Domain.Models;

namespace ComedorInfantil.Gestion.Application.Features
{
    public static class ResponseApiService
    {
        public static BaseResponseModel Response(int statusCode, object data = null, string message = null) 
        {
            bool success = statusCode >= 200 && statusCode < 300;
            return new BaseResponseModel
            {
                StatusCode = statusCode,
                Success = success,
                Data = data,
                Message = message
            };
        }
    }
}
