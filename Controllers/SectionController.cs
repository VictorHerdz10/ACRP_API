using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ACRP_API.Services;
using ACRP_API.Dtos.Sections;
using ACRP_API.Dtos.ResponseDto;
using Swashbuckle.AspNetCore.Annotations;
using ACRP_API.Utils;

namespace ACRP_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SectionController(ISectionService sectionService, ILogger<SectionController> logger) : ControllerBase
{
    private readonly ISectionService _sectionService = sectionService;
    private readonly ILogger<SectionController> _logger = logger;

    [HttpGet]
    [Authorize]
    [SwaggerOperation(
        Summary = "Obtener todas las secciones",
        Description = "Obtiene una lista de todas las secciones disponibles.")]
    [SwaggerResponse(200, "Lista de secciones obtenida correctamente", typeof(List<SectionResponseDto>))]
    [SwaggerResponse(401, "Usuario no autenticado", typeof(ErrorResponse))]
    [SwaggerResponse(403, "No tiene permisos para realizar esta acción", typeof(ErrorResponse))]
    [SwaggerResponse(500, "Error al obtener las secciones", typeof(ErrorResponse))]
    public async Task<ActionResult<List<SectionResponseDto>>> GetAllSections()
    {
        try
        {
            var validationResult = AuthUtils.ValidateUserAndAdmin(User);
            if (validationResult is not OkResult)
            {
                return validationResult;
            }

            var sections = await _sectionService.GetAllSectionsAsync();
            return Ok(sections);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener las secciones");
            return StatusCode(500, new ErrorResponse { Message = "Error al obtener las secciones" });
        }
    }

    [HttpGet("{id}")]
    [Authorize]
    [SwaggerOperation(
        Summary = "Obtener sección por ID",
        Description = "Obtiene una sección específica por su ID.")]
    [SwaggerResponse(200, "Sección encontrada", typeof(SectionResponseDto))]
    [SwaggerResponse(401, "Usuario no autenticado", typeof(ErrorResponse))]
    [SwaggerResponse(403, "No tiene permisos para realizar esta acción", typeof(ErrorResponse))]
    [SwaggerResponse(404, "Sección no encontrada", typeof(ErrorResponse))]
    [SwaggerResponse(500, "Error al obtener la sección", typeof(ErrorResponse))]
    public async Task<ActionResult<SectionResponseDto>> GetSection(string id)
    {
        try
        {
            var validationResult = AuthUtils.ValidateUserAndAdmin(User);
            if (validationResult is not OkResult)
            {
                return validationResult;
            }

            var section = await _sectionService.GetSectionByIdAsync(id);
            if (section == null)
                return NotFound(new ErrorResponse { Message = "Sección no encontrada" });

            return Ok(section);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener la sección");
            return StatusCode(500, new ErrorResponse { Message = "Error al obtener la sección" });
        }
    }

    [HttpPost]
    [Authorize]
    [SwaggerOperation(
        Summary = "Crear nueva sección",
        Description = "Crea una nueva sección con los datos proporcionados.")]
    [SwaggerResponse(201, "Sección creada correctamente", typeof(SectionResponseDto))]
    [SwaggerResponse(400, "Ya existe una sección con ese Titulo", typeof(ErrorResponse))]
    [SwaggerResponse(401, "Usuario no autenticado", typeof(ErrorResponse))]
    [SwaggerResponse(403, "No tiene permisos para realizar esta acción", typeof(ErrorResponse))]
    [SwaggerResponse(400, "Datos inválidos", typeof(ErrorResponse))]
    [SwaggerResponse(500, "Error al crear la sección", typeof(ErrorResponse))]
    public async Task<ActionResult> CreateSection(CreateSectionDto sectionDto)
    {
        try
        {
            var validationResult = AuthUtils.ValidateUserAndAdmin(User);
            if (validationResult is not OkResult)
            {
                return validationResult;
            }
            var IsTitleExists = await _sectionService.SectionTitleExistsAsync(sectionDto.Title);
            if (IsTitleExists)
                return BadRequest(new ErrorResponse { Message = "Ya existe una sección con ese Titulo" });

            var section = await _sectionService.CreateSectionAsync(sectionDto);
            var successResponse = new SuccessResponse { Message = "Sección creada correctamente" };
            return CreatedAtAction(nameof(GetSection), new { id = section.Id }, successResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear la sección");
            return StatusCode(500, new ErrorResponse { Message = "Error al crear la sección" });
        }
    }

    [HttpPut("{id}")]
    [Authorize]
    [SwaggerOperation(
        Summary = "Actualizar sección",
        Description = "Actualiza una sección existente con los datos proporcionados.")]
    [SwaggerResponse(200, "Sección actualizada correctamente", typeof(SuccessResponse))]
    [SwaggerResponse(401, "Usuario no autenticado", typeof(ErrorResponse))]
    [SwaggerResponse(403, "No tiene permisos para realizar esta acción", typeof(ErrorResponse))]
    [SwaggerResponse(404, "Sección no encontrada", typeof(ErrorResponse))]
    [SwaggerResponse(500, "Error al actualizar la sección", typeof(ErrorResponse))]
    public async Task<IActionResult> UpdateSection(string id, UpdateSectionDto sectionDto)
    {
        try
        {
            var validationResult = AuthUtils.ValidateUserAndAdmin(User);
            if (validationResult is not OkResult)
            {
                return validationResult;
            }

            var section = await _sectionService.GetSectionByIdAsync(id);
            if (section == null)
                return NotFound(new ErrorResponse { Message = "Sección no encontrada" });

            await _sectionService.UpdateSectionAsync(id, sectionDto);
            return Ok(new SuccessResponse { Message = "Sección actualizada correctamente" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar la sección");
            return StatusCode(500, new ErrorResponse { Message = "Error al actualizar la sección" });
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    [SwaggerOperation(
        Summary = "Eliminar sección",
        Description = "Elimina una sección existente por su ID.")]
    [SwaggerResponse(200, "Sección eliminada correctamente", typeof(SuccessResponse))]
    [SwaggerResponse(401, "Usuario no autenticado", typeof(ErrorResponse))]
    [SwaggerResponse(403, "No tiene permisos para realizar esta acción", typeof(ErrorResponse))]
    [SwaggerResponse(404, "Sección no encontrada", typeof(ErrorResponse))]
    [SwaggerResponse(500, "Error al eliminar la sección", typeof(ErrorResponse))]
    public async Task<IActionResult> DeleteSection(string id)
    {
        try
        {
            var validationResult = AuthUtils.ValidateUserAndAdmin(User);
            if (validationResult is not OkResult)
            {
                return validationResult;
            }

            var section = await _sectionService.GetSectionByIdAsync(id);
            if (section == null)
                return NotFound(new ErrorResponse { Message = "Sección no encontrada" });

            await _sectionService.DeleteSectionAsync(id);
            return Ok(new SuccessResponse { Message = "Sección eliminada correctamente" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar la sección");
            return StatusCode(500, new ErrorResponse { Message = "Error al eliminar la sección" });
        }
    }
}