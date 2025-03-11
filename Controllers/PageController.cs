using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ACRP_API.Services;
using ACRP_API.Dtos.Pages;
using ACRP_API.Dtos.ResponseDto;
using Swashbuckle.AspNetCore.Annotations;
using ACRP_API.Utils;

namespace ACRP_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PageController(IPageService pageService, ILogger<PageController> logger) : ControllerBase
{
    private readonly IPageService _pageService = pageService;
    private readonly ILogger<PageController> _logger = logger;

    [HttpGet]
    [Authorize]
    [SwaggerOperation(
        Summary = "Obtener todas las páginas",
        Description = "Obtiene una lista de todas las páginas disponibles.")]
    [SwaggerResponse(200, "Lista de páginas obtenida correctamente", typeof(List<PageResponseDto>))]
    [SwaggerResponse(401, "Usuario no autenticado", typeof(ErrorResponse))]
    [SwaggerResponse(403, "No tiene permisos para realizar esta acción", typeof(ErrorResponse))]
    [SwaggerResponse(500, "Error al obtener las páginas", typeof(ErrorResponse))]
    public async Task<ActionResult<List<PageResponseDto>>> GetAllPages()
    {
        try
        {
            var validationResult = AuthUtils.ValidateUserAndAdmin(User);
            if (validationResult is not OkResult)
            {
                return validationResult;
            }

            var pages = await _pageService.GetAllPagesAsync();
            return Ok(pages);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener las páginas");
            return StatusCode(500, new ErrorResponse { Message = "Error al obtener las páginas" });
        }
    }

    [HttpGet("{id}")]
    [Authorize]
    [SwaggerOperation(
        Summary = "Obtener página por ID",
        Description = "Obtiene una página específica por su ID.")]
    [SwaggerResponse(200, "Página encontrada", typeof(PageResponseDto))]
    [SwaggerResponse(401, "Usuario no autenticado", typeof(ErrorResponse))]
    [SwaggerResponse(403, "No tiene permisos para realizar esta acción", typeof(ErrorResponse))]
    [SwaggerResponse(404, "Página no encontrada", typeof(ErrorResponse))]
    [SwaggerResponse(500, "Error al obtener la página", typeof(ErrorResponse))]
    public async Task<ActionResult<PageResponseDto>> GetPage(string id)
    {
        try
        {
            var validationResult = AuthUtils.ValidateUserAndAdmin(User);
            if (validationResult is not OkResult)
            {
                return validationResult;
            }

            var page = await _pageService.GetPageByIdAsync(id);
            if (page == null)
                return NotFound(new ErrorResponse { Message = "Página no encontrada" });

            return Ok(page);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener la página");
            return StatusCode(500, new ErrorResponse { Message = "Error al obtener la página" });
        }
    }

    [HttpPost]
    [Authorize]
    [SwaggerOperation(
             Summary = "Crear nueva página",
             Description = "Crea una nueva página con los datos proporcionados.")]
    [SwaggerResponse(201, "Página creada correctamente", typeof(SuccessResponse))]
    [SwaggerResponse(400, "Ya existe una página con ese nombre", typeof(ErrorResponse))]
    [SwaggerResponse(401, "Usuario no autenticado", typeof(ErrorResponse))]
    [SwaggerResponse(403, "No tiene permisos para realizar esta acción", typeof(ErrorResponse))]
    [SwaggerResponse(500, "Error al crear la página", typeof(ErrorResponse))]
    public async Task<ActionResult> CreatePage(CreatePageDto pageDto)
    {
        try
        {
            var validationResult = AuthUtils.ValidateUserAndAdmin(User);
            if (validationResult is not OkResult)
            {
                return validationResult;
            }
            var IsExistName = await _pageService.PageNameExistsAsync(pageDto.Name);
            if (IsExistName)
            {
                return BadRequest(new ErrorResponse { Message = "Ya existe una página con ese nombre" });
            }

            var page = await _pageService.CreatePageAsync(pageDto);
            var successResponse = new SuccessResponse { Message = "Página creada correctamente" };
            return CreatedAtAction(nameof(GetPage), new { id = page.Id }, successResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear la página");
            return StatusCode(500, new ErrorResponse { Message = "Error al crear la página" });
        }
    }

    [HttpPut("{id}")]
    [Authorize]
    [SwaggerOperation(
        Summary = "Actualizar página",
        Description = "Actualiza una página existente con los datos proporcionados.")]
    [SwaggerResponse(200, "Página actualizada correctamente", typeof(SuccessResponse))]
    [SwaggerResponse(401, "Usuario no autenticado", typeof(ErrorResponse))]
    [SwaggerResponse(403, "No tiene permisos para realizar esta acción", typeof(ErrorResponse))]
    [SwaggerResponse(404, "Página no encontrada", typeof(ErrorResponse))]
    [SwaggerResponse(500, "Error al actualizar la página", typeof(ErrorResponse))]
    public async Task<IActionResult> UpdatePage(string id, UpdatePageDto pageDto)
    {
        try
        {
            var validationResult = AuthUtils.ValidateUserAndAdmin(User);
            if (validationResult is not OkResult)
            {
                return validationResult;
            }

            var page = await _pageService.GetPageByIdAsync(id);
            if (page == null)
                return NotFound(new ErrorResponse { Message = "Página no encontrada" });

            await _pageService.UpdatePageAsync(id, pageDto);
            return Ok(new SuccessResponse { Message = "Página actualizada correctamente" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar la página");
            return StatusCode(500, new ErrorResponse { Message = "Error al actualizar la página" });
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    [SwaggerOperation(
        Summary = "Eliminar página",
        Description = "Elimina una página existente por su ID.")]
    [SwaggerResponse(200, "Página eliminada correctamente", typeof(SuccessResponse))]
    [SwaggerResponse(401, "Usuario no autenticado", typeof(ErrorResponse))]
    [SwaggerResponse(403, "No tiene permisos para realizar esta acción", typeof(ErrorResponse))]
    [SwaggerResponse(404, "Página no encontrada", typeof(ErrorResponse))]
    [SwaggerResponse(500, "Error al eliminar la página", typeof(ErrorResponse))]
    public async Task<IActionResult> DeletePage(string id)
    {
        try
        {
            var validationResult = AuthUtils.ValidateUserAndAdmin(User);
            if (validationResult is not OkResult)
            {
                return validationResult;
            }

            var page = await _pageService.GetPageByIdAsync(id);
            if (page == null)
                return NotFound(new ErrorResponse { Message = "Página no encontrada" });

            await _pageService.DeletePageAsync(id);
            return Ok(new SuccessResponse { Message = "Página eliminada correctamente" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar la página");
            return StatusCode(500, new ErrorResponse { Message = "Error al eliminar la página" });
        }
    }
}