using API.Models.Domain;
using API.Models.DTO;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class BlogPostsController : ControllerBase
  {
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly ICategoryRepository _categoryRepository;

    public BlogPostsController(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
    {
      this._blogPostRepository = blogPostRepository;
      this._categoryRepository = categoryRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBlogPost(CreateBlogPostRequestDto request)
    {
      // DTO to domain model
      var blogPost = new BlogPost
      {
        Title = request.Title,
        ShortDescription = request.ShortDescription,
        Content = request.Content,
        FeaturedImageUrl = request.FeaturedImageUrl,
        UrlHandle = request.UrlHandle,
        PublishedDate = request.PublishedDate,
        Author = request.Author,
        IsVisible = request.IsVisible,
        Categories = new List<Category>()
      };

      foreach (var categoryGuid in request.Categories)
      {
        var existingCategory = await _categoryRepository.GetById(categoryGuid);
        if (existingCategory is not null)
        {
          blogPost.Categories.Add(existingCategory);
        }
      }

      blogPost = await _blogPostRepository.CreateAsync(blogPost);

      // domain model to DTO
      var response = new BlogPostDto
      {
        Id = blogPost.Id,
        Title = blogPost.Title,
        ShortDescription = blogPost.ShortDescription,
        Content = blogPost.Content,
        FeaturedImageUrl = blogPost.FeaturedImageUrl,
        UrlHandle = blogPost.UrlHandle,
        PublishedDate = blogPost.PublishedDate,
        Author = blogPost.Author,
        IsVisible = blogPost.IsVisible,
        Categories = blogPost.Categories.Select(dto => new CategoryDto
        {
          Id = dto.Id,
          Name = dto.Name,
          UrlHandle = dto.UrlHandle
        }).ToList()
      };

      return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBlogPosts()
    {

      // API call
      var blogPosts = await _blogPostRepository.GetAllAsync();

      // domain model to DTO
      var response = new List<BlogPostDto>();

      foreach (var blogPost in blogPosts)
      {
        response.Add(new BlogPostDto
        {
          Id = blogPost.Id,
          Title = blogPost.Title,
          ShortDescription = blogPost.ShortDescription,
          Content = blogPost.Content,
          FeaturedImageUrl = blogPost.FeaturedImageUrl,
          UrlHandle = blogPost.UrlHandle,
          PublishedDate = blogPost.PublishedDate,
          Author = blogPost.Author,
          IsVisible = blogPost.IsVisible,
          Categories = blogPost.Categories.Select(dto => new CategoryDto
          {
            Id = dto.Id,
            Name = dto.Name,
            UrlHandle = dto.UrlHandle
          }).ToList()
        });
      }

      return Ok(response);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetBlogPostById(Guid id)
    {

      var existingBlogPost = await _blogPostRepository.GetByIdAsync(id);

      if (existingBlogPost is null)
      {
        return NotFound();
      }

      // domain model to DTO
      var response = new BlogPostDto
      {
        Id = existingBlogPost.Id,
        Title = existingBlogPost.Title,
        ShortDescription = existingBlogPost.ShortDescription,
        Content = existingBlogPost.Content,
        FeaturedImageUrl = existingBlogPost.FeaturedImageUrl,
        UrlHandle = existingBlogPost.UrlHandle,
        PublishedDate = existingBlogPost.PublishedDate,
        Author = existingBlogPost.Author,
        IsVisible = existingBlogPost.IsVisible,
        Categories = existingBlogPost.Categories.Select(dto => new CategoryDto
        {
          Id = dto.Id,
          Name = dto.Name,
          UrlHandle = dto.UrlHandle
        }).ToList()
      };

      return Ok(response);
    }
  }
}