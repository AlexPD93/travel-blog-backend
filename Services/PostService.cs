using travel_blog_backend.Models;

namespace travel_blog_backend.Services;

public class PostService
{
    private readonly Supabase.Client _supabase;

    public PostService(Supabase.Client supabase)
    {
        _supabase = supabase;
    }

    public async Task<List<Post>> GetPosts()
    {
        var response = await _supabase
            .From<Post>()
            .Get();

        return response.Models;
    }

    public async Task<Post?> GetPostById(int id)
    {
        var response = await _supabase
            .From<Post>()
            .Filter("id", Postgrest.Constants.Operator.Equals, id)
            .Get();
        return response.Models.FirstOrDefault();
    }

    public async Task<Post> CreatePost(Post post)
    {
        var response = await _supabase
            .From<Post>()
            .Insert(post);

        return response.Models.First();
    }
}