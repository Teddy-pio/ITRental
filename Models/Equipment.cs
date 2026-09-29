namespace ITRental.Models;

public class Equipment
{
    public int Id { get; set; }
    public string Category { get; set; } = string.Empty; 
    public string Model { get; set; } = string.Empty;
    public decimal DailyRate { get; set; }
    public bool IsAvailable { get; set; }
    public int ClientId { get; set; } 
    public bool IsDeleted { get; set; } = false;
}