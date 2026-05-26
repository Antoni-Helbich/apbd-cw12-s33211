using apbd_cw12_s33211.DTOs;
using apbd_cw12_s33211.Exceptions;
using apbd_cw12_s33211.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apbd_cw12_s33211.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public PatientsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPatients([FromQuery] string? search)
        {
            var res = await _dbService.GetPatientsAsync(search);
            return Ok(res);
        }

        [HttpPost]
        [Route("{pesel}/bedassignments")]
        public async Task<IActionResult> PostBed(string pesel, [FromBody] PostDto dto)
        {
            try
            {
                var res = await _dbService.PostBedAsync(pesel, dto);
                return Ok(res);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
