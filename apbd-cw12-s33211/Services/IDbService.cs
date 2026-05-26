using apbd_cw12_s33211.DTOs;
using apbd_cw12_s33211.Models;

namespace apbd_cw12_s33211.Services;

public interface IDbService
{
    Task<List<GetPatientDto>> GetPatientsAsync(string? search);
    Task<BedAssignmentDto> PostBedAsync(string pesel, PostDto dto);
}