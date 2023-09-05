using System.ComponentModel.DataAnnotations;

namespace Musicians.Domain.Entities;

public class Musician
{
    [Key]
    [Required]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public string PerformAs { get; set; } = string.Empty;
    public DateTime IntroDate { get; set; }
    public string? Instrument { get; set; }
}
