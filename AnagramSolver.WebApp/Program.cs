using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.DataAccess;
using AnagramSolver.BusinessLogic.Logic;
using AnagramSolver.BusinessLogic.Repositories;
using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services
    .AddScoped<IDataAccess<HashSet<string>>, DataAccessHashSet>()
    .AddScoped<IWordService<Word>, WordService>()
    .AddScoped<IWordRepository, WordRepository>()
    .AddScoped<IFileService, FileService>()
    .AddScoped<ICookieService, CookieService>()
    .AddScoped<IAnagramSolverLogic, AnagramSolverLogic>();

var app = builder.Build();

var configuration = app.Configuration;

app.UseStaticFiles();
app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Anagrams}/{word?}",
    new { controller = "Home", action = "Anagrams" });

app.Run();