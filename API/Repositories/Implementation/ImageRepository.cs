using API.Data;
using API.Models.Domain;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementation
{
  public class ImageRepository : IImageRepository
  {
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ApplicationDbContext _dbContext;

    public ImageRepository(
      IWebHostEnvironment webHostEnvironment,
      IHttpContextAccessor httpContextAccessor,
      ApplicationDbContext dbContext)
    {
      this._webHostEnvironment = webHostEnvironment;
      this._httpContextAccessor = httpContextAccessor;
      this._dbContext = dbContext;
    }

    public async Task<IEnumerable<BlogImage>> GetAll()
    {
      return await _dbContext.BlogImages.ToListAsync();
    }

    public async Task<BlogImage> Upload(IFormFile file, BlogImage blogImage)
    {
      // upload image
      var localPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", $"{blogImage.FileName}{blogImage.FileExtension}");
      using var stream = new FileStream(localPath, FileMode.Create);
      await file.CopyToAsync(stream);

      // update the database
      var httpRequest = _httpContextAccessor.HttpContext.Request;
      var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{blogImage.FileName}{blogImage.FileExtension}";
      blogImage.Url = urlPath;

      await _dbContext.BlogImages.AddAsync(blogImage);
      await _dbContext.SaveChangesAsync();
      return blogImage;
    }
  }
}