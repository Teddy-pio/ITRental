using ITRental.DTOs;
using ITRental.Models;
using ITRental.Data;

namespace ITRental.Services;

public class EquipmentService : IEquipmentService
{
    private readonly AppDbContext _context;

    public EquipmentService(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<EquipmentDto> GetAll(int pageNumber, int pageSize)
    {
        return _context.Equipments
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(e => new EquipmentDto
            {
                Id = e.Id,
                FullName = $"{e.Category} {e.Model}",
                DailyRate = e.DailyRate
            })
            .ToList();
    }

    public EquipmentDto GetById(int id)
    {
        var equipment = _context.Equipments.FirstOrDefault(e => e.Id == id);
        if (equipment == null) throw new KeyNotFoundException($"Sprzęt o ID {id} nie istnieje.");

        return new EquipmentDto
        {
            Id = equipment.Id,
            FullName = $"{equipment.Category} {equipment.Model}",
            DailyRate = equipment.DailyRate
        };
    }

    public int Create(CreateEquipmentDto dto)
    {
        var exists = _context.Equipments.Any(e => e.Category.ToLower() == dto.Category.ToLower() && 
                                                  e.Model.ToLower() == dto.Model.ToLower());
        if (exists) throw new ApplicationException("Taki sprzęt (kategoria i model) już istnieje w bazie!");

        var newEquipment = new Equipment
        {
            Category = dto.Category,
            Model = dto.Model,
            DailyRate = dto.DailyRate,
            IsAvailable = true,
            ClientId = 1
        };

        _context.Equipments.Add(newEquipment);
        _context.SaveChanges();

        return newEquipment.Id;
    }

    public void Update(int id, UpdateEquipmentDto dto)
    {
        var equipment = _context.Equipments.FirstOrDefault(e => e.Id == id);
        if (equipment == null) throw new KeyNotFoundException($"Sprzęt o ID {id} nie istnieje.");

        equipment.DailyRate = dto.DailyRate;
        equipment.IsAvailable = dto.IsAvailable;
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var equipment = _context.Equipments.FirstOrDefault(e => e.Id == id);
        if (equipment == null) throw new KeyNotFoundException($"Sprzęt o ID {id} nie istnieje.");

        if (!equipment.IsAvailable) throw new ApplicationException("Nie można usunąć sprzętu, który jest aktualnie wypożyczony klientowi!");

        equipment.IsDeleted = true; 
        _context.SaveChanges();
    }
}


