namespace API.Models.DTO
{
  public class CreateCategoryRequestDto
  {
    public string? Name { get; set; } = string.Empty;
    public string? Urlhandle { get; set; } = string.Empty;
  }
}