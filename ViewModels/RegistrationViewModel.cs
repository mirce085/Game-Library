using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFRelationships.Models;
using EFRelationships.Messages;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Forms;

namespace EFRelationships.ViewModels;

[INotifyPropertyChanged]
public partial class RegistrationViewModel : BaseViewModel
{
    [ObservableProperty]
    private string? _login;

    [ObservableProperty]
    private string? _password;

    [ObservableProperty]
    private string? _passwordRepeat;

    private MyDbContext _dbContext = App.ServiceProvider.GetRequiredService<MyDbContext>();


    [RelayCommand]
    public void Register()
    {
        if (Login == string.Empty || Password == string.Empty || PasswordRepeat == string.Empty)
        {
            MessageBox.Show("Fill all fields!");
            return;
        }

        var user = _dbContext.Users.FirstOrDefault(u => u.Login == Login);

        if(user != null)
        {
            MessageBox.Show("That login has already choosen!");
            return;
        }

        if(Password!.Length != PasswordRepeat!.Length || Password != PasswordRepeat)
        {
            MessageBox.Show("Wrong input!");
            return;
        }


        var newUser = new User
        {
            Login = Login!,
            Password = Password!,
        };

        _dbContext.Users.Add(newUser);

        _dbContext.SaveChanges();

        MessageBox.Show("You have been successfully registrated!");

        LoginView();
    }


    [RelayCommand]
    public void LoginView()
    {
        var message = new ChangeViewModelMessage(new LoginViewModel());

        WeakReferenceMessenger.Default.Send(message);
    }

}
