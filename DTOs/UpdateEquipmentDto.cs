using System.ComponentModel.DataAnnotations;

namespace ITRental.DTOs;

public class UpdateEquipmentDto
{
    [Range(1, 10000, ErrorMessage = "Cena za dobę musi mieścić się w przedziale od 1 do 10 000 zł.")]
    public decimal DailyRate { get; set; }
    
    public bool IsAvailable { get; set; }
}


