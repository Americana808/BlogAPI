using BlogAPI.Models;
using BlogAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    // Phase 1: in-memory "database"
    private readonly BlogDbContext _db;

    public PostsController(BlogDbContext db)
    {
        _db = db;
    }

    // GET /api/posts
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var posts = await _db.Posts.OrderByDescending(p => p.CreatedAtUtc).ToListAsync();
        return Ok(posts);
    }

    // GET /api/posts/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var post = await _db.Posts.FindAsync(id);
        if (post is null) return NotFound();

        return Ok(post);
    }

    // POST /api/posts
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Post newPost)
    {
        newPost.Id = 0; // Ensure the ID is zero so EF Core will generate a new one
        newPost.CreatedAtUtc = DateTime.UtcNow;
        newPost.UpdatedAtUtc = DateTime.UtcNow;

        _db.Posts.Add(newPost);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = newPost.Id }, newPost);
    }

    // PUT /api/posts/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Post updatedPost)
    {
        var post = await _db.Posts.FindAsync(id);
        if (post is null) return NotFound();

        post.Title = updatedPost.Title;
        post.Content = updatedPost.Content;
        post.Slug = updatedPost.Slug;
        post.IsPublished = updatedPost.IsPublished;
        post.UpdatedAtUtc = DateTime.UtcNow;

        await _db.SaveChangesAsync();

        return Ok(post);
    }

    // DELETE /api/posts/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var post = await _db.Posts.FindAsync(id);
        if (post is null) return NotFound();
        
        _db.Posts.Remove(post);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
