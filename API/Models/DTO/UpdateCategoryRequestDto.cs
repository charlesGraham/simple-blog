namespace API.Models.DTO
{
  public class UpdateCategoryRequestDto
  {
    public string? Name { get; set; } = string.Empty;
    public string? UrlHandle { get; set; } = string.Empty;
  }
}