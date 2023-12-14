using API.Models.Domain;
using API.Models.DTO;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CategoriesController : ControllerBase
  {
    private readonly ICategoryRepository _categoryRepository;

    public CategoriesController(ICategoryRepository categoryRepository)
    {
      this._categoryRepository = categoryRepository;
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

      await _categoryRepository.CreateAsync(category);

      // domain model to DTO
      var response = new CategoryDto
      {
        Id = category.Id,
        Name = category.Name,
        UrlHandle = category.Urlhandle
      };

      return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
      // API call
      var categories = await _categoryRepository.GetAllAsync();

      // domain model to DTO
      var response = new List<CategoryDto>();

      foreach (var category in categories)
      {
        response.Add(new CategoryDto
        {
          Id = category.Id,
          Name = category.Name,
          UrlHandle = category.Urlhandle
        });
      }

      return Ok(response);

    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
      var existingCategory = await _categoryRepository.GetById(id);

      if (existingCategory is null)
      {
        return NotFound();
      }

      // domain model to DTO
      var response = new CategoryDto
      {
        Id = existingCategory.Id,
        Name = existingCategory.Name,
        UrlHandle = existingCategory.Urlhandle
      };

      return Ok(response);
    }
  }
}