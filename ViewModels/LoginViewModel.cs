using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using EFRelationships.Messages;
using EFRelationships.Models;
using EFRelationships.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EFRelationships.ViewModels;

[INotifyPropertyChanged]
public partial class LoginViewModel : BaseViewModel
{
    [ObservableProperty]
    private string? _usernameText;

    [ObservableProperty]
    private string? _passwordText;

    private MyDbContext _dbContext;

    public LoginViewModel()
    {
        _dbContext = App.ServiceProvider.GetRequiredService<MyDbContext>();
    }


    [RelayCommand]
    public void Login()
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.Login == UsernameText && x.Password == PasswordText);
        if (user == null)
        {
            MessageBox.Show("There is no such user!");
            return;
        }

        GetUserService.SetUser(user);

        var message = new ChangeViewModelMessage(new UserLibraryViewModel());
        WeakReferenceMessenger.Default.Send(message);
    }

    [RelayCommand]
    public void Registration()
    {
        var message = new ChangeViewModelMessage(new RegistrationViewModel());

        WeakReferenceMessenger.Default.Send(message);
    }
}
