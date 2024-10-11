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

builder.Services.AddTransient<Seeder>();

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
var dataSeeder = scope.ServiceProvider.GetRequiredService<Seeder>();
var users = dataSeeder.CreateUsersInDatabase(dbContext);
var questions = dataSeeder.CreateQuestionsInDatabase(dbContext, users);
var answers = dataSeeder.CreateAnswersInDatabase(dbContext, questions, users);
var comments = dataSeeder.CreateCommentsInDatabase(dbContext, answers, questions, users);
dataSeeder.CreateTagsAndAssignThemToQuestions(dbContext, questions);

app.MapGet("users", (StackOverflowDbContext dbContext) =>
{
    var users = dbContext.Users.Where(u => u.Id == Guid.Parse("D80D8586-90EF-1AE4-A3D8-03B08255EB28"));
    return users;
});

app.Run();