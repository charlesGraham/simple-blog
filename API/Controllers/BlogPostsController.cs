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

    public BlogPostsController(IBlogPostRepository blogPostRepository)
    {
      this._blogPostRepository = blogPostRepository;
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
        IsVisble = request.IsVisble
      };

      await _blogPostRepository.CreateAsync(blogPost);

      // domain model to DTO
      var response = new BlogPostDto
      {
        Title = blogPost.Title,
        ShortDescription = blogPost.ShortDescription,
        Content = blogPost.Content,
        FeaturedImageUrl = blogPost.FeaturedImageUrl,
        UrlHandle = blogPost.UrlHandle,
        PublishedDate = blogPost.PublishedDate,
        Author = blogPost.Author,
        IsVisble = blogPost.IsVisble
      };

      return Ok(response);

    }
  }
}