using Microsoft.AspNetCore.Mvc;
using ITRental.DTOs;
using ITRental.Services;

namespace ITRental.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipmentController : ControllerBase
{
    private readonly IEquipmentService _equipmentService;

    public EquipmentController(IEquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
    }

    /// <summary>
    /// Pobiera stronicowaną listę dostępnego sprzętu IT.
    /// </summary>
    /// <param name="pageNumber">Numer strony (domyślnie 1)</param>
    /// <param name="pageSize">Ilość elementów na stronie (domyślnie 5)</param>
    /// <returns>Zwraca listę obiektów EquipmentDto</returns>
    [HttpGet]
    public ActionResult<IEnumerable<EquipmentDto>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
    {
        return Ok(_equipmentService.GetAll(pageNumber, pageSize));
    }

    /// <summary>
    /// Pobiera szczegóły konkretnego sprzętu po jego identyfikatorze ID.
    /// </summary>
    [HttpGet("{id}")]
    public ActionResult<EquipmentDto> GetById(int id)
    {
        return Ok(_equipmentService.GetById(id));
    }

    /// <summary>
    /// Tworzy i dodaje nowy sprzęt do systemu.
    /// </summary>
    /// <response code="201">Zwraca nowo utworzony obiekt</response>
    /// <response code="400">Jeśli walidacja się nie powiedzie lub sprzęt już istnieje</response>
    [HttpPost]
    public ActionResult Create(CreateEquipmentDto dto)
    {
        var newId = _equipmentService.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = newId }, dto);
    }

    /// <summary>
    /// Aktualizuje stawkę i dostępność wybranego sprzętu.
    /// </summary>
    [HttpPut("{id}")]
    public ActionResult Update(int id, UpdateEquipmentDto dto)
    {
        _equipmentService.Update(id, dto);
        return NoContent();
    }

    /// <summary>
    /// Usuwa sprzęt z systemu (Soft Delete).
    /// </summary>
    /// <response code="400">Jeśli sprzęt jest aktualnie wypożyczony</response>
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        _equipmentService.Delete(id);
        return NoContent();
    }
}

