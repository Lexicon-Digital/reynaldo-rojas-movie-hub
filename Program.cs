using Microsoft.EntityFrameworkCore;
using MoviesAPI;
using MoviesAPI.DbContexts;
using MoviesAPI.Services;


// Instantiating builder
var builder = WebApplication.CreateBuilder(args);

// Registering services

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(options =>
{
  options.ReturnHttpNotAcceptable = true;

}).AddXmlDataContractSerializerFormatters();

builder.Services.AddDbContext<MovieAPIContext>(
  dbContextOptions => dbContextOptions.UseSqlite(
    builder.Configuration["ConnectionStrings:CityInfoDBConnectionString"]));

builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Generating app
var app = builder.Build();

// Apply midddlewares

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

