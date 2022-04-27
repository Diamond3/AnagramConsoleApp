using System.Diagnostics;
using System.Text;
using AnagramSolver.BusinessLogic.DataAccess;
using AnagramSolver.BusinessLogic.Repositories;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

var services = new ServiceCollection();

Console.OutputEncoding = Encoding.Unicode;
Console.InputEncoding = Encoding.Unicode;

services
    .AddScoped<IDataAccess<HashSet<string>>, DataAccessHashSet>()
    .AddScoped<IAnagramSolver, AnagramSolver.BusinessLogic.Logic.AnagramSolver>()
    .BuildServiceProvider();

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false).Build();

var settings = config.GetSection("UserSettings").Get<UserSettings>();

IWordRepository repo = new WordRepository(services.BuildServiceProvider().GetRequiredService<IAnagramSolver>(),
    services.BuildServiceProvider().GetRequiredService<IDataAccess<HashSet<string>>>());

while (true)
{
    var input = Console.ReadLine();
    while (input.Length < settings.MinLength)
    {
        Console.WriteLine("Word is too short!");
        input = Console.ReadLine();
    }

    var list = repo.GetAnagrams(input.ToLower());
    
    if (list.Count > 0)
    {
        list.Take(settings.AnagramCount).ToList().ForEach(Console.WriteLine);
        Console.WriteLine();
    }
    else
        Console.WriteLine("No anagrams were found!");
    Console.WriteLine("------------");
}