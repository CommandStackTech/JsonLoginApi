using JsonLoginApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ CORS: allow any origin (open)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

// Register our UserService
builder.Services.AddSingleton<IUserService, UserService>();

var app = builder.Build();

// ✅ Enable CORS (must be before auth + endpoints)
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.

{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
