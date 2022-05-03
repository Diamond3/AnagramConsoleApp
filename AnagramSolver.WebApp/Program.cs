using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.DataAccess;
using AnagramSolver.BusinessLogic.Logic;
using AnagramSolver.BusinessLogic.Repositories;
using AnagramSolver.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services
    .AddScoped<IDataAccess<HashSet<string>>, DataAccessHashSet>()
    .AddScoped<IWordRepository, WordRepository>()
    .AddScoped<IWordsService, WordsService>()
    .AddScoped<IAnagramSolverLogic, AnagramSolverLogic>();

var app = builder.Build();

IConfiguration configuration = app.Configuration;

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Anagrams}/{word?}",
    new { controller = "Home", action = "Anagrams"});

app.Run();