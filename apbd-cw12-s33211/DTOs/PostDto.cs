namespace apbd_cw12_s33211.DTOs;

public class PostDto
{
    public DateTime From { get; set; }
    public DateTime? To { get; set; } = null;
    public string BedType { get; set; } = string.Empty;
    public string Ward { get; set; } = string.Empty;
}