using Microsoft.AspNetCore.Mvc;
using travel_blog_backend.Models;
using travel_blog_backend.Services;
using travel_blog_backend.DTOs;

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
        var postDtos = posts.Select(p => new PostDto
        {
            Id = p.Id,
            Title = p.Title,
            Content = p.Content,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt
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
            CreatedAt = post.CreatedAt,
            UpdatedAt = post.UpdatedAt
        };

        return Ok(postDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] Post post)
    {
        var createdPost = await _postService.CreatePost(post);
        return CreatedAtAction(nameof(GetPosts), new { id = createdPost.Id }, createdPost);
    }
}