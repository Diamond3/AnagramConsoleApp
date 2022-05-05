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
    .AddScoped<IWordsService, WordsService>()
    .AddScoped<IFileService, FileService>()
    .AddScoped<ICookieService, CookieService>()
    .AddScoped<IAnagramSolverLogic, AnagramSolverLogic>()
    .AddScoped<IWordRepository>(sp
        => new WordRepository(sp.GetService<IDataAccess<HashSet<string>>>(), "zodynas.txt"));

var app = builder.Build();

IConfiguration configuration = app.Configuration;
/*configuration.GetSection()*/

app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Anagrams}/{word?}",
    new { controller = "Home", action = "Anagrams"});

app.Run();