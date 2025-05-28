using System.ComponentModel.DataAnnotations;

namespace EFcodefirst.DTOs;

public class DoctorDTO
{
    [Required]
    public int IdDoctor { get; set; }
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
}