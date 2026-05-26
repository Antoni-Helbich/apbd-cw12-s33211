using System.ComponentModel.DataAnnotations;

namespace apbd_cw12_s33211.DTOs;

public class WardDto
{
    public int Id { get; set; }
    [MaxLength(300)]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}