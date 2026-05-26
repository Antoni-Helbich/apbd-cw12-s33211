using System.ComponentModel.DataAnnotations;

namespace apbd_cw12_s33211.DTOs;

public class GetPatientDto
{
    [StringLength(11, MinimumLength = 11)]
    public string Pesel { get; set; } = string.Empty;
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
    public bool Sex { get; set; }
    public List<AdmissionDto> Admissions { get; set; } = [];
    public List<BedAssignmentDto> BedAssignments { get; set; } = [];
}