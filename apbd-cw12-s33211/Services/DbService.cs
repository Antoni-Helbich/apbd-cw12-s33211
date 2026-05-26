using apbd_cw12_s33211.DTOs;
using apbd_cw12_s33211.Exceptions;
using apbd_cw12_s33211.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd_cw12_s33211.Services;
using apbd_cw12_s33211.Data;

public class DbService : IDbService
{
    private readonly Apbd12Context _dbContext;

    public DbService(Apbd12Context dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    public async Task<List<GetPatientDto>> GetPatientsAsync(string? search)
    {
        var res = await _dbContext.Patients
            .Where(p => string.IsNullOrEmpty(search) || EF.Functions.Like(p.FirstName, $"%{search}%") || EF.Functions.Like(p.LastName, $"%{search}%"))
            .Select(p => new GetPatientDto
            {
                Pesel = p.Pesel,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Age = p.Age,
                Sex = p.Sex,
                Admissions = p.Admissions.Select(a => new AdmissionDto
                {
                    Id = a.Id,
                    AdmissionDate = a.AdmissionDate,
                    DischargeDate = a.DischargeDate,
                    Ward = new WardDto
                    {
                        Id = a.Ward.Id,
                        Name = a.Ward.Name,
                        Description = a.Ward.Description
                    }
                }).ToList(),
                BedAssignments = p.BedAssignments.Select(ba => new BedAssignmentDto
                {
                    Id = ba.Id,
                    From = ba.From,
                    To = ba.To,
                    Bed = new BedDto
                    {
                        Id = ba.Bed.Id,
                        BedType = new BedTypeDto
                        {
                            Id = ba.Bed.BedType.Id,
                            Name = ba.Bed.BedType.Name,
                            Description = ba.Bed.BedType.Description
                        },
                        Room = new RoomDto
                        {
                            Id = ba.Bed.Room.Id,
                            HasTv = ba.Bed.Room.HasTv,
                            Ward = new WardDto
                            {
                                Id = ba.Bed.Room.Ward.Id,
                                Name = ba.Bed.Room.Ward.Name,
                                Description = ba.Bed.Room.Ward.Description
                            }
                        }
                    }
                }).ToList()
            }).ToListAsync();

        return res;
    }

    public async Task<BedAssignmentDto> PostBedAsync(string pesel, PostDto dto)
    {
        var patient = await _dbContext.Patients
            .Where(p => p.Pesel == pesel)
            .FirstOrDefaultAsync();
        if (patient == null) throw new NotFoundException("Nie znaleziono pacjenta o podanym peselu");
        
        var maxDate = new DateTime(3000, 1, 1);
        
        var bed = await _dbContext.Beds
            .Include(b => b.BedType)
            .Include(b => b.Room)
                .ThenInclude(r => r.Ward)
            .Where(b => !_dbContext.BedAssignments.Any(ba =>
                ba.BedId == b.Id &&
                dto.From < (ba.To ?? maxDate) && 
                (dto.To ?? maxDate) > ba.From))
            .Where(b => b.Room.Ward.Name == dto.Ward)
            .Where(b => b.BedType.Name == dto.BedType)
            .FirstOrDefaultAsync();
        if (bed == null) throw new NotFoundException("Nie znaleziono łóżka o podanych kryteriach");

        var assignment = new BedAssignment
        {
            PatientPesel = pesel,
            BedId = bed.Id,
            From = dto.From,
            To = dto.To
        };
        _dbContext.BedAssignments.Add(assignment);
        await _dbContext.SaveChangesAsync();

        return new BedAssignmentDto
        {
            Id = assignment.Id,
            From = assignment.From,
            To = assignment.To,
            Bed = new BedDto
            {
                Id = bed.Id,
                BedType = new BedTypeDto
                {
                    Id = bed.BedTypeId,
                    Name = bed.BedType.Name,
                    Description = bed.BedType.Description
                },
                Room = new RoomDto
                {
                    Id = bed.Room.Id,
                    HasTv = bed.Room.HasTv,
                    Ward = new WardDto
                    {
                        Id = bed.Room.WardId,
                        Name = bed.Room.Ward.Name,
                        Description = bed.Room.Ward.Description
                    }
                }
            }
        };
    }
}