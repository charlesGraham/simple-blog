namespace API.Models.Domain
{
  public class BlogImage
  {
    public Guid Id { get; set; }
    public string? FileName { get; set; } = string.Empty;
    public string? FileExtension { get; set; } = string.Empty;
    public string? Title { get; set; } = string.Empty;
    public string? Url { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; }
  }
}