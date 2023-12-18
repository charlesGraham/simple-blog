using API.Models.Domain;
using API.Models.DTO;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ImagesController : ControllerBase
  {
    private readonly IImageRepository _imageRepository;

    public ImagesController(IImageRepository imageRepository)
    {
      this._imageRepository = imageRepository;
    }

    [HttpPost]
    public async Task<IActionResult> UploadImage(IFormFile file, string fileName, string title)
    {
      ValidateFileUpload(file);

      if (ModelState.IsValid)
      {
        var blogImage = new BlogImage
        {
          FileName = fileName,
          FileExtension = Path.GetExtension(file.FileName).ToLower(),
          Title = title,
          DateCreated = DateTime.Now
        };

        blogImage = await _imageRepository.Upload(file, blogImage);

        // domain model to DTO
        var response = new BlogImageDto
        {
          Id = blogImage.Id,
          Title = blogImage.Title,
          DateCreated = blogImage.DateCreated,
          FileExtension = blogImage.FileExtension,
          FileName = blogImage.FileName,
          Url = blogImage.Url
        };

        return Ok(response);

      }

      return BadRequest(ModelState);
    }

    private void ValidateFileUpload(IFormFile file)
    {
      var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

      if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
      {
        ModelState.AddModelError("file", "Unsupported file format");
      }

      if (file.Length > 10485760)
      {
        ModelState.AddModelError("file", "File size cannot be more than 10MB");
      }
    }
  }
}