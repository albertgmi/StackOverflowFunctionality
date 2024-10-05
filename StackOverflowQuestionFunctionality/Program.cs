using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using StackOverflowQuestionFunctionality;
using StackOverflowQuestionFunctionality.Entities;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StackOverflowDbContext>(
    option=>option
    .UseSqlServer(builder.Configuration.GetConnectionString("EFStackOverflowCopyConnectionString")));

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddTransient<IDataSeeder, Seeder>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();

var dbContext = scope.ServiceProvider.GetRequiredService<StackOverflowDbContext>();

if (dbContext.Database.GetPendingMigrations().Any())
{
    dbContext.Database.Migrate();
    dbContext.SaveChanges();
}
var dataSeeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
dataSeeder.Seed(dbContext);

app.Run();