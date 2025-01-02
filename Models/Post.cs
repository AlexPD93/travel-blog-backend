using Postgrest.Attributes;
using Postgrest.Models;
using System.Text.Json.Serialization;


namespace travel_blog_backend.Models;

[Table("posts")]
public class Post : BaseModel
{
    [PrimaryKey("id", false)]
    [JsonIgnore]
    public long Id { get; set; }
    [Column("title")]
    public string Title { get; set; } = string.Empty;
    [Column("content")]
    public string Content { get; set; } = string.Empty;
    [Column("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }
    [Column("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
    [Column("category")]
    public string Category { get; set; } = string.Empty;
    [Column("image_url")]
    public string ImageUrl { get; set; } = string.Empty;
}