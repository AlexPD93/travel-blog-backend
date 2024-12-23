using DotNetEnv;
using Supabase;
using travel_blog_backend.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configure Supabase client

Env.Load();
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddScoped<PostService>();

// Access configuration
var supabaseUrl = builder.Configuration["SUPABASE_URL"]
    ?? throw new InvalidOperationException("Supabase URL is not configured");
var supabaseAnonKey = builder.Configuration["SUPABASE_ANON_KEY"]
    ?? throw new InvalidOperationException("Supabase Anon Key is not configured");

builder.Services.AddSingleton(sp =>
{
    return new Client(supabaseUrl, supabaseAnonKey, new SupabaseOptions
    {
        AutoRefreshToken = true,
    });
});

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();