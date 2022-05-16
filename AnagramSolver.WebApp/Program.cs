using AnagramSolver.BusinessLogic.DataAccess;
using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.CodeFirst.Models;
using Word = AnagramSolver.Contracts.Models.Word;
using WordRepository = AnagramSolver.EF.CodeFirst.Repositories.WordRepository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services
    .AddScoped<IDataAccess<HashSet<string>>, DataAccessHashSet>()
    .AddScoped<IWordService<Word>, WordService>()
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