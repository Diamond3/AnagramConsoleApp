using AnagramSolver.BusinessLogic.DataAccess;
using AnagramSolver.BusinessLogic.Logic;
using AnagramSolver.BusinessLogic.Repositories;
using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Cli;
using AnagramSolver.Contracts.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();


services
    .AddScoped<IDataAccess<HashSet<string>>, DataAccess>()
    /*.AddScoped<IWordRepository, WordRepository>()*/
    .AddScoped<IWordsServiceOld, WordsServiceOld>()
    .AddScoped<IAnagramSolverLogic, AnagramSolverLogic>()
    .AddScoped<IWordRepositoryOld>(sp
        => new WordRepositoryOld(sp.GetService<IDataAccess<HashSet<string>>>(), "zodynas.txt"))
    .BuildServiceProvider();


var environment = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");

var builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.{environment}.json", false, true)
    .AddEnvironmentVariables();

var config = builder.Build();

var message = config.GetValue<string>("Message");
var filePath = config.GetValue<string>("FilePath");
var settings = config.GetSection("UserSettings").Get<UserSettings>();

/*var view = new AnagramSolverView(services.BuildServiceProvider().GetRequiredService<IWordsService>(),
    services.BuildServiceProvider().GetRequiredService<IAnagramSolverLogic>());*/

var view = new AnagramSolverHttpClientView();

view.LoadView(environment, message);
await view.FindAnagrams(settings, filePath);