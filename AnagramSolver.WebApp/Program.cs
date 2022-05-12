using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.DataAccess;
using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst;
using Word = AnagramSolver.Contracts.Models.Word;
using WordRepository = AnagramSolver.EF.CodeFirst.Repositories.WordRepository;
using AnagramDBContext = AnagramSolver.EF.CodeFirst.Models.AnagramDBContext;
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