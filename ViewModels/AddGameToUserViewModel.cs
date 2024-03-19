using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using CommunityToolkit.Mvvm.Input;
using EFRelationships.Messages;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using EFRelationships.Models;
using System.IO;
using System.Text.Json;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using EFRelationships.Services;


namespace EFRelationships.ViewModels;

[INotifyPropertyChanged]
public partial class AddGameToUserViewModel : BaseViewModel
{
    [ObservableProperty]
    private string? _gameTitle;

    [ObservableProperty]
    private string? _gameDescription;

    [ObservableProperty]
    private DateTime _gameReleaseDate;

    [ObservableProperty]
    private string? _gameDeveloper;

    [ObservableProperty]
    private string? _gamePublisher;

    [ObservableProperty]
    private int _gameRating;

    [ObservableProperty]
    private string? _gameCategory;

    private MyDbContext _dbContext;

    private User _user;

    public ObservableCollection<Game> Games { get; private set; }

    public AddGameToUserViewModel()
    {
        _dbContext = App.ServiceProvider.GetRequiredService<MyDbContext>();

        var tempList = _dbContext.Games.ToList();

        Games = new ObservableCollection<Game>();

        if(tempList != null)
        {
            Games = [.. tempList];
        }

        _user = GetUserService.GetUser();
    }



    [RelayCommand]
    public void Back()
    {
        var message = new ChangeViewModelMessage(new UserLibraryViewModel());

        WeakReferenceMessenger.Default.Send(message);
    }

    [RelayCommand]
    public void Details(Game? game)
    {
        if(game == null)
        {
            MessageBox.Show("Select game!");
            return;
        }

        var message = new ChangeViewModelMessage(new GameDetailsViewModel(game, 2));
        WeakReferenceMessenger.Default.Send(message);


    }


    [RelayCommand]
    public void Add(Game? game)
    {
        if (game == null)
        {
            MessageBox.Show("Select Game!");
            return;
        }

        var userWithGames = _dbContext.Users
                              .Include(u => u.Games)
                              .FirstOrDefault(u => u.Id == _user.Id);

        var obj = userWithGames!.Games.FirstOrDefault(x => x.Id == game.Id);

        if (obj == null)
        {
            userWithGames.Games.Add(game);

            _dbContext.SaveChanges();

            Back();
            return;
        }

        MessageBox.Show("You already have this game!");
    }

    [RelayCommand]
    public void AddGame()
    {
        var message = new ChangeViewModelMessage(new UpdateAddGameViewModel());
        WeakReferenceMessenger.Default.Send(message);
    }

    [RelayCommand]
    public void UpdateGame(Game? game)
    {
        if (game == null)
        {
            MessageBox.Show("Select Game!");
            return;
        }

        var message = new ChangeViewModelMessage(new UpdateAddGameViewModel(game));
        WeakReferenceMessenger.Default.Send(message);

    }
}
