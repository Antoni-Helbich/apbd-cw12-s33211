namespace apbd_cw12_s33211.DTOs;

public class GetPatientDto
{
    public string Pesel { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
    public bool Sex { get; set; }
    public List<AdmissionDto> Admissions { get; set; } = [];
    public List<BedAssignmentDto> BedAssignments { get; set; } = [];
}