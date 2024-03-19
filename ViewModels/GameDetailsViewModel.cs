using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using EFRelationships.Messages;
using EFRelationships.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EFRelationships.ViewModels;

[INotifyPropertyChanged]
public partial class GameDetailsViewModel : BaseViewModel
{
    [ObservableProperty]
    private string? _gameTitle;

    [ObservableProperty]
    private string? _gameDescription;
    
    [ObservableProperty]
    private string? _gameDeveloper;
    
    [ObservableProperty]
    private string? _gamePublisher;
    
    [ObservableProperty]
    private string? _gameReleaseDate;
    
    [ObservableProperty]
    private string? _gameRating;
    
    [ObservableProperty]
    private string? _gameCategory;

    private MyDbContext _dbContext;

    private int _window;
    public List<Comment> Comments { get; private set; }

    public GameDetailsViewModel(Game game, int window)
    {
        _window = window;
        _dbContext = App.ServiceProvider.GetRequiredService<MyDbContext>();

        _dbContext.Games.Include(g => g.Comments).First(g => g.Id == game.Id);

        Comments = _dbContext.Games.First(x => x.Id == game.Id).Comments.ToList();

        GameTitle = game.Title;
        GameDescription = game.Description;
        GameDeveloper = game.Developer;
        GamePublisher = game.Publisher;
        GameReleaseDate = game.ReleaseDate.ToString();

        if(Comments.Count == 0)
        {
            GameRating = "0/5";
        }
        else
        {
            GameRating = Math.Round((float)game.Rating / Comments.Count, 1).ToString() + "/5";
        }
        
        GameCategory = game.Category;
    }

    [RelayCommand]
    public void Back()
    {
        if(_window == 1)
        {
            var message = new ChangeViewModelMessage(new UserLibraryViewModel());

            WeakReferenceMessenger.Default.Send(message);
        }
        else if(_window == 2)
        {
            var message = new ChangeViewModelMessage(new AddGameToUserViewModel());

            WeakReferenceMessenger.Default.Send(message);
        }
        
    }

}
