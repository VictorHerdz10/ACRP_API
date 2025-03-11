using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ACRP_API.Dtos.ResponseDto;

namespace ACRP_API.Utils
{
    public static class AuthUtils
    {
        public static ActionResult ValidateUserAndAdmin(ClaimsPrincipal user)
        { 

            var isAdmin = user.FindFirst(ClaimTypes.Role)?.Value;
            if (string.IsNullOrEmpty(isAdmin) || isAdmin != "admin")
            {
                return new ObjectResult(new ErrorResponse { Message = "Usted no puede realizar esta acción" }) { StatusCode = 403 };
            }

            return new OkResult(); 
        }
    }
}