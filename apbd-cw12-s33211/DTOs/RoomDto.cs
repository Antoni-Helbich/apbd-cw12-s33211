using System.ComponentModel.DataAnnotations;

namespace apbd_cw12_s33211.DTOs;

public class RoomDto
{
    [StringLength(4, MinimumLength = 4)]
    public string Id { get; set; } = string.Empty;
    public bool HasTv { get; set; }
    public WardDto Ward { get; set; } = null!;

}