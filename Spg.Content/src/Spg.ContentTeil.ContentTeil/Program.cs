using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Spg.ContentTeil.DomainModel.Dtos;
using Spg.ContentTeil.Repository;
using Spg.ContentTeil.ContentTeil.Models;
using Spg.ContentTeil.ContentTeil.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Information);

builder.Services.Configure<ContentDatabaseSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<IMongoDBServices, MongoDBServices>();
builder.Services.AddSingleton<IContentRepository, ContentRepository>();
builder.Services.AddSingleton<IContentService, ContentService>();

// seeding
var services = builder.Services.BuildServiceProvider();
var contentService = services.GetRequiredService<IContentService>();
var contents = contentService.GetAllContent();
if (contents.Count() == 0)
{
    contentService.CreateContent(new ContentDto
    {
        text = "admin",
    });
}

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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