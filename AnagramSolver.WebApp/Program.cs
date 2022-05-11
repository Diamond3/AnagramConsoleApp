using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.DataAccess;
using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst;
using AnagramSolver.EF.DatabaseFirst.Models;
using Word = AnagramSolver.Contracts.Models.Word;
//using AnagramSolver.EF.DatabaseFirst.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services
    .AddScoped<IDataAccess<HashSet<string>>, DataAccessHashSet>()
    .AddScoped<IWordService<Word>, WordService>()
    .AddScoped<IWordRepository, WordRepository>()
    .AddScoped<IFileService, FileService>()
    .AddScoped<ICookieService, CookieService>()
    .AddDbContext<AnagramDBContext>();

var app = builder.Build();

var configuration = app.Configuration;

app.UseStaticFiles();
app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Anagrams}/{word?}",
    new { controller = "Home", action = "Anagrams" });

app.Run();