using AnagramSolver.API.Services.Services;
using AnagramSolver.BusinessLogic.DataAccess;
using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.EF.CodeFirst.Models;
using AnagramSolver.EF.CodeFirst.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services
    .AddScoped<IDataAccess<HashSet<string>>, DataAccess>()
    .AddScoped<IWordService<Word>, AnagramSolverApiService>()
    .AddScoped<IWordRepository, WordRepository>()
    .AddScoped<IFileService, FileService>()
    .AddScoped<ICookieService, CookieService>()
    .AddSingleton<IUserService, UserService>()
    .AddDbContext<AnagramDbContext>();

var app = builder.Build();

app.UseStaticFiles();
app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Anagrams}/{word?}",
    new { controller = "Home", action = "Anagrams" });

app.Run();