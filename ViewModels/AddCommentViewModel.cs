using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFRelationships.Models;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using EFRelationships.Messages;
using CommunityToolkit.Mvvm.Messaging;

namespace EFRelationships.ViewModels;

[INotifyPropertyChanged]
public partial class AddCommentViewModel : BaseViewModel
{
    [ObservableProperty]
    private int _rating = 0;

    [ObservableProperty]
    private string? _comment;

    private Game _game;

    private MyDbContext _dbContext;

    public AddCommentViewModel(Game game)
    {
        _dbContext = App.ServiceProvider.GetRequiredService<MyDbContext>();

        _game = _dbContext.Games.Include(g => g.Comments).First(g => g.Id == game.Id);
    }

    [RelayCommand]
    public void Add()
    {
        if(Comment == string.Empty)
        {
            MessageBox.Show("Fill all fields!");
            return;
        }
        _game.Rating += Rating;
        _game.Comments.Add(new Comment
        {
            GameId = _game.Id,
            Text = Comment!,
        });
        _dbContext.Games.Update(_game);

        _dbContext.SaveChanges();

        MessageBox.Show("You have been successfully added comment!");
        Back();
    }

    [RelayCommand]
    public void Back()
    {
        var message = new ChangeViewModelMessage(new UserLibraryViewModel());

        WeakReferenceMessenger.Default.Send(message);
    }
}
