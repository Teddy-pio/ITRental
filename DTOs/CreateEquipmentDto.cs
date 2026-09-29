using System.ComponentModel.DataAnnotations;

namespace ITRental.DTOs;

public class CreateEquipmentDto
{
    [Required(ErrorMessage = "Kategoria sprzętu jest wymagana.")]
    [StringLength(50, ErrorMessage = "Kategoria nie może przekraczać 50 znaków.")]
    public string Category { get; set; } = string.Empty;

    [Required(ErrorMessage = "Model sprzętu jest wymagany.")]
    public string Model { get; set; } = string.Empty;

    [Range(1, 10000, ErrorMessage = "Cena za dobę musi mieścić się w przedziale od 1 do 10 000 zł.")]
    public decimal DailyRate { get; set; }
}