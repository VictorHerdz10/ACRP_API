using System.Security.Claims;
using ACRP_API.Dtos.Users;
using ACRP_API.Dtos.ResponseDto;
using ACRP_API.Models;
using ACRP_API.Services;
using ACRP_API.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ACRP_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(
    IUserService usersService,
    GenerateJwt generateJwt,
    ILogger<UserController> logger) : ControllerBase
{
    private readonly IUserService _usersService = usersService;
    private readonly GenerateJwt _generateJwt = generateJwt;
    private readonly ILogger<UserController> _logger = logger;

    [HttpPost("register")]
    [SwaggerOperation(
        Summary = "Registrar un nuevo usuario",
        Description = "Registra un nuevo usuario con los detalles proporcionados.")]
    [SwaggerResponse(200, "Usuario registrado correctamente", typeof(SuccessResponse))]
    [SwaggerResponse(400, "El email ya está registrado", typeof(ErrorResponse))]
    [SwaggerResponse(500, "Error al registrar usuario", typeof(ErrorResponse))]
    public async Task<ActionResult<string>> Register(RegisterDto request)
    {
        try
        {
            // Verificar si el email ya existe
            var existingUser = await _usersService.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return BadRequest(new ErrorResponse { Message = "El email ya está registrado" });
            }

            // Crear nuevo usuario
            var user = new User
            {
                Email = request.Email,
                Username = request.Username,
                Password = request.Password
            };

            await _usersService.CreateAsync(user);

            return Ok(new SuccessResponse { Message = "Usuario registrado correctamente" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error en el registro de usuario");
            return StatusCode(500, new ErrorResponse { Message = "Error al registrar usuario" });
        }
    }

    [HttpPost("login")]
    [SwaggerOperation(
        Summary = "Iniciar sesión",
        Description = "Inicia sesión de un usuario con los detalles proporcionados.")]
    [SwaggerResponse(200, "Inicio de sesión exitoso", typeof(AuthResponse))]
    [SwaggerResponse(400, "El usuario no existe o la contraseña es incorrecta", typeof(ErrorResponse))]
    [SwaggerResponse(500, "Error al iniciar sesión", typeof(ErrorResponse))]
    public async Task<ActionResult<AuthResponse>> Login(LoginDto request)
    {
        try
        {
            var user = await _usersService.GetByEmailAsync(request.Email);
            if (user == null)
            {
                return BadRequest(new ErrorResponse { Message = "El usuario no existe" });
            }

            var isValid = _usersService.ValidatePassword(user, request.Password);
            if (!isValid)
            {
                return BadRequest(new ErrorResponse { Message = "Contraseña incorrecta" });
            }

            var token = _generateJwt.GenerateJwtToken(user);

            return Ok(new AuthResponse
            {
                Token = token
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error en el login");
            return StatusCode(500, new ErrorResponse { Message = "Error al iniciar sesión" });
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    [SwaggerOperation(
        Summary = "Eliminar un usuario",
        Description = "Elimina un usuario por su ID.")]
    [SwaggerResponse(200, "Usuario eliminado correctamente", typeof(SuccessResponse))]
    [SwaggerResponse(401, "Usuario no autenticado", typeof(ErrorResponse))]
    [SwaggerResponse(403, "No tiene permisos para realizar esta acción", typeof(ErrorResponse))]
    [SwaggerResponse(404, "Usuario no encontrado", typeof(ErrorResponse))]
    [SwaggerResponse(500, "Error al eliminar usuario", typeof(ErrorResponse))]
    public async Task<IActionResult> DeleteUser(string id)
    {
        try
        {
            var validationResult = AuthUtils.ValidateUserAndAdmin(User);
            if (validationResult is not OkResult)
            {
                return validationResult;
            }

            var user = await _usersService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound(new ErrorResponse { Message = "Usuario no encontrado" });
            }

            await _usersService.RemoveAsync(id);

            return Ok(new SuccessResponse { Message = "Usuario eliminado correctamente" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar usuario");
            return StatusCode(500, new ErrorResponse { Message = "Error al eliminar usuario" });
        }
    }

    [HttpPut("role/{id}")]
    [Authorize]
    [SwaggerOperation(
        Summary = "Actualizar el rol de un usuario",
        Description = "Actualiza el rol de un usuario por su ID.")]
    [SwaggerResponse(200, "Rol del usuario actualizado correctamente", typeof(SuccessResponse))]
    [SwaggerResponse(400, "Rol ya asignado", typeof(ErrorResponse))]
    [SwaggerResponse(401, "Usuario no autenticado o rol ya asignado", typeof(ErrorResponse))]
    [SwaggerResponse(403, "No tiene permisos para realizar esta acción", typeof(ErrorResponse))]
    [SwaggerResponse(404, "Usuario no encontrado", typeof(ErrorResponse))]
    [SwaggerResponse(500, "Error al actualizar el rol del usuario", typeof(ErrorResponse))]
    public async Task<IActionResult> UpdateUserRole(string id, [FromBody] UpdateRoleDto request)
    {
        try
        {
            var validationResult = AuthUtils.ValidateUserAndAdmin(User);
            if (validationResult is not OkResult)
            {
                return validationResult;
            }

            var user = await _usersService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound(new ErrorResponse { Message = "Usuario no encontrado" });
            }

            string role = await _usersService.GetRoleAsync(id);
            if (role == request.Role)
            {
                return BadRequest(new ErrorResponse { Message = "Ya este rol se encuentra asignado al usuario" });
            }

            await _usersService.UpdateRoleAsync(id, request.Role);

            return Ok(new SuccessResponse { Message = "Rol del usuario actualizado correctamente" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar el rol del usuario");
            return StatusCode(500, new ErrorResponse { Message = "Error al actualizar el rol del usuario" });
        }
    }

    [HttpGet("all")]
    [Authorize]
    [SwaggerOperation(
        Summary = "Obtener todos los usuarios",
        Description = "Obtiene una lista de todos los usuarios.")]
    [SwaggerResponse(200, "Lista de usuarios obtenida correctamente", typeof(List<UserResponseDto>))]
    [SwaggerResponse(401, "Usuario no autenticado", typeof(ErrorResponse))]
    [SwaggerResponse(403, "No tiene permisos para realizar esta acción", typeof(ErrorResponse))]
    [SwaggerResponse(500, "Error al obtener todos los usuarios", typeof(ErrorResponse))]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var validationResult = AuthUtils.ValidateUserAndAdmin(User);
            if (validationResult is not OkResult)
            {
                return validationResult;
            }

            var users = await _usersService.GetAllUsersAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener todos los usuarios");
            return StatusCode(500, new ErrorResponse { Message = "Error al obtener todos los usuarios" });
        }
    }
    [HttpGet("perfil")]
    [Authorize]
    [SwaggerOperation(
        Summary = "Obtener el perfil del usuario",
        Description = "Obtiene tlos datos del perfil del usuario autenticado.")]
    [SwaggerResponse(200, "Lista la informacion del perfil de usuario correctamente", typeof(PerfilResponseDto))]
    [SwaggerResponse(400, "No pude encontrar la información del perfil", typeof(ErrorResponse))]
    [SwaggerResponse(401, "Usuario no autenticado", typeof(ErrorResponse))]
    [SwaggerResponse(403, "No tiene permisos  para realizar esta acción", typeof(ErrorResponse))]
    [SwaggerResponse(500, "Error al obtener todos los usuarios", typeof(ErrorResponse))]
    public async Task<IActionResult> GetPerfil()
    {
        try
        {
            var validationResult = AuthUtils.ValidateUserAndAdmin(User);
            if (validationResult is not OkResult)
            {
                return validationResult;
            }
            var Email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (Email == null)
            {

                return BadRequest(new ErrorResponse { Message = "No pude encontrar la información del usuario" });

            }
            var perfil = await _usersService.GetPerfilAsync(Email);
            return Ok(perfil);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener todos los usuarios");
            return StatusCode(500, new ErrorResponse { Message = "Error al obtener todos los usuarios" });
        }
    }
}