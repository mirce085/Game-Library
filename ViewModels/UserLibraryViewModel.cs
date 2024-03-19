using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using EFRelationships.Messages;
using EFRelationships.Models;
using EFRelationships.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EFRelationships.ViewModels;

public partial class UserLibraryViewModel : BaseViewModel
{
    private User _user = null!;
    public ObservableCollection<Game> Games { get; private set; }

    private MyDbContext _dbContext;
    public UserLibraryViewModel()
    {
        _user = GetUserService.GetUser();

        _dbContext = App.ServiceProvider.GetRequiredService<MyDbContext>();

        var userWithGames = _dbContext.Users.Include(x => x.Games).First(x => x.Id == _user.Id);

        var gameList = userWithGames.Games.ToList();

        Games = new ObservableCollection<Game>();

        if(gameList != null)
        {
            Games = [.. gameList];
        }
    }

    [RelayCommand]
    public void Remove(Game? game)
    {
        if(game == null)
        {
            MessageBox.Show("Select Game!");
            return;
        }
        try
        {
            _dbContext.Users.FirstOrDefault(x => x.Id == _user.Id)!.Games.Remove(game);

            _dbContext.SaveChanges();

            Games.Remove(game);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }
    }

    [RelayCommand]
    public void Details(Game? game)
    {
        if (game == null)
        {
            MessageBox.Show("Select Game!");
            return;
        }

        var message = new ChangeViewModelMessage(new GameDetailsViewModel(game, 1));

        WeakReferenceMessenger.Default.Send(message);
    }

    [RelayCommand]
    public void AddComment(Game? game)
    {
        if (game == null)
        {
            MessageBox.Show("Select Game!");
            return;
        }

        var message = new ChangeViewModelMessage(new AddCommentViewModel(game));

        WeakReferenceMessenger.Default.Send(message);
    }

    [RelayCommand]
    public void AddGame()
    {
        var message = new ChangeViewModelMessage(new AddGameToUserViewModel());

        WeakReferenceMessenger.Default.Send(message);
    }
}
