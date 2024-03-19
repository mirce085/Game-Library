using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using EFRelationships.ViewModels;
using EFRelationships.Views;
using Microsoft.Extensions.Configuration;
using EFRelationships.Models;


namespace EFRelationships;


public partial class App : Application
{
    public static ServiceCollection ServiceCollection { get; private set; } = null!;
    public static ServiceProvider ServiceProvider { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        ServiceCollection = new ServiceCollection();

        var configurationBuilder = new ConfigurationBuilder();

        configurationBuilder.AddJsonFile("config.json");

        IConfiguration configuration = configurationBuilder.Build();

        ServiceCollection.AddSingleton<IConfiguration>(provider =>
        {
            return configuration;
        });

        ServiceCollection.AddSingleton<MainViewModel>();
        ServiceCollection.AddSingleton<MainView>();
        ServiceCollection.AddSingleton<MyDbContext>();

        ServiceProvider = ServiceCollection.BuildServiceProvider();

        var view = ServiceProvider.GetService<MainView>()!;

        view.Show();
    }
}
