using System.ComponentModel.DataAnnotations;
namespace apbd_cw12_s33211.DTOs;

public class PostDto
{
    public DateTime From { get; set; }
    public DateTime? To { get; set; } = null;
    [MaxLength(300)]
    public string BedType { get; set; } = string.Empty;
    [MaxLength(300)]
    public string Ward { get; set; } = string.Empty;
}