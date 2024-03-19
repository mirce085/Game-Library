using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EFRelationships.Models;
using Microsoft.Extensions.DependencyInjection;
using EFRelationships.Messages;
using CommunityToolkit.Mvvm.Messaging;

namespace EFRelationships.ViewModels;

[INotifyPropertyChanged]
public partial class UpdateAddGameViewModel : BaseViewModel
{
    [ObservableProperty]
    private string? _title;

    [ObservableProperty]
    private string? _description;

    [ObservableProperty]
    private string? _developer;

    [ObservableProperty]
    private DateTime? _date;

    [ObservableProperty]
    private string? _publisher;

    [ObservableProperty]
    private string? _category;

    [ObservableProperty]
    private string? _buttonText;

    private Game? _game;

    private MyDbContext _dbContext = App.ServiceProvider.GetRequiredService<MyDbContext>();

    public UpdateAddGameViewModel()
    {
        ButtonText = "Add";
    }

    public UpdateAddGameViewModel(Game game)
    {
        Title = game.Title;
        Description = game.Description;
        Developer = game.Developer;
        Publisher = game.Publisher;
        Category = game.Category;
        _game = game;
        ButtonText = "Update";
    }

    [RelayCommand]
    public void UpdateAdd()
    {
        if (Title == string.Empty || Description == string.Empty || Date == null || Developer == string.Empty || Publisher == string.Empty || Category == string.Empty)
        {
            MessageBox.Show("Fill all fields!");
            return;
        }

        if (_game != null)
        {
            _game.Title = Title!;
            _game.Description = Description!;
            _game.Developer = Developer!;
            _game.Publisher = Publisher!;
            _game.Category = Category!;
            _game.ReleaseDate = Date.Value;

            _dbContext.Games.Update(_game);

            _dbContext.SaveChanges();

            MessageBox.Show("You have been successfully updated the game!");

            Back();

            return;
        }

        var game = new Game
        {
            Title = Title!,
            Description = Description!,
            Developer = Developer!,
            Publisher = Publisher!,
            Category = Category!,
            ReleaseDate = Date.Value,
        };

        _dbContext.Add(game);

        _dbContext.SaveChanges();

        MessageBox.Show("You have been successfully added the game!");

        Back();
    }

    [RelayCommand]
    public void Back()
    {
        var message = new ChangeViewModelMessage(new AddGameToUserViewModel());

        WeakReferenceMessenger.Default.Send(message);
    }
}
