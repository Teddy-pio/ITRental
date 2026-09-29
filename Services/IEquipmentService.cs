using ITRental.DTOs;

namespace ITRental.Services;

public interface IEquipmentService
{
    IEnumerable<EquipmentDto> GetAll(int pageNumber, int pageSize); 
    EquipmentDto GetById(int id);
    int Create(CreateEquipmentDto dto);
    void Update(int id, UpdateEquipmentDto dto);
    void Delete(int id);
}


