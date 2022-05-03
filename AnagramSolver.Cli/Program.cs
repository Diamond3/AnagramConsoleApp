﻿using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.DataAccess;
using AnagramSolver.BusinessLogic.Logic;
using AnagramSolver.BusinessLogic.Repositories;
using AnagramSolver.Cli;
using AnagramSolver.Contracts.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();


services
    .AddScoped<IDataAccess<HashSet<string>>, DataAccessHashSet>()
    .AddScoped<IWordRepository, WordRepository>()
    .AddScoped<IWordsService, WordsService>()
    .AddScoped<IAnagramSolverLogic, AnagramSolverLogic>()
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

var view = new AnagramSolverView(services.BuildServiceProvider().GetRequiredService<IWordsService>(),
    services.BuildServiceProvider().GetRequiredService<IAnagramSolverLogic>());

view.LoadView(environment, message);
view.FindAnagrams(settings, filePath);