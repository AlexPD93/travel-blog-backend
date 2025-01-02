using Microsoft.AspNetCore.Mvc;
using travel_blog_backend.Models;
using travel_blog_backend.Services;
using travel_blog_backend.DTOs;
using System.Text.Json;

namespace travel_blog_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly PostService _postService;

    public PostsController(PostService postService)
    {
        _postService = postService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPosts()
    {
        var posts = await _postService.GetPosts();
        var postDtos = posts.Select(post => new PostDto
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content,
            ImageUrl = post.ImageUrl,
            Category = post.Category,
            CreatedAt = post.CreatedAt,
            UpdatedAt = post.UpdatedAt
        }).ToList();

        return Ok(postDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostById(int id)
    {
        var post = await _postService.GetPostById(id);
        if (post == null)
        {
            return NotFound(new { Message = $"Post with id {id} not found" });
        }
        var postDto = new PostDto
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content,
            ImageUrl = post.ImageUrl,
            Category = post.Category,
            CreatedAt = post.CreatedAt,
            UpdatedAt = post.UpdatedAt
        };

        return Ok(postDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDto createPostDto)
    {
        if (createPostDto == null)
        {
            return BadRequest(new { Message = "Post data is required" });
        }

        var post = new Post
        {
            Title = createPostDto.Title,
            Content = createPostDto.Content,
            ImageUrl = createPostDto.ImageUrl,
            Category = createPostDto.Category,
            CreatedAt = DateTimeOffset.UtcNow,
        };

        var createdPost = await _postService.CreatePost(post);

        if (createdPost == null)
        {
            return StatusCode(500, new { Message = "There was an error creating the post" });
        }
        var postDto = new CreatePostDto
        {
            Title = createdPost.Title,
            Content = createdPost.Content,
            ImageUrl = createPostDto.ImageUrl,
            Category = createPostDto.Category,
            CreatedAt = DateTimeOffset.UtcNow,
        };
        return CreatedAtAction(nameof(GetPostById), new { id = createdPost.Id }, postDto);
    }
}