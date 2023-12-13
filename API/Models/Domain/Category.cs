namespace API.Models.Domain
{
  public class Category
  {
    public Guid Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? Urlhandle { get; set; } = string.Empty;

  }
}