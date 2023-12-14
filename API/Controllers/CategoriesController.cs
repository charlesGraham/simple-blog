using API.Data;
using API.Models.Domain;
using API.Models.DTO;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CategoriesController : ControllerBase
  {
    private readonly ApplicationDbContext _dbContext;

    public CategoriesController(ApplicationDbContext dbContext)
    {
      this._dbContext = dbContext;

    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
    {
      // map DTO to domain model
      var category = new Category
      {
        Name = request.Name,
        Urlhandle = request.Urlhandle
      };

      await _dbContext.Categories.AddAsync(category);
      await _dbContext.SaveChangesAsync();

      // domain model to DTO
      var response = new CategoryDto
      {
        Id = category.Id,
        Name = category.Name,
        UrlHandle = category.Urlhandle
      };

      return Ok(response);
    }
  }
}