using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Spg.Search.DomainModel.Dtos;
using Spg.Search.Repository;
using Spg.Search.Search.Models;
using Spg.Search.Search.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Information);

builder.Services.Configure<SearchDatabaseSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<IMongoDBServices, MongoDBServices>();
builder.Services.AddSingleton<ISearchRepository, SearchRepository>();
builder.Services.AddSingleton<ISearchService, SearchService>();

// seeding
var services = builder.Services.BuildServiceProvider();
var searchService = services.GetRequiredService<ISearchService>();
var searchHistories = searchService.GetAllSearchHistory();
if (searchHistories.Count() == 0)
{
    searchService.CreateSearchHistory(new SearchHistoryDto
    {
        searchUsername = "admin",
        searchContent = "admin"
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