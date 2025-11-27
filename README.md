# 🎮 Game Library

A modern desktop application for managing your game collection, inspired by Steam.  Built with WPF and Entity Framework Core, following the MVVM (Model-View-ViewModel) architectural pattern for clean separation of concerns and maintainability. 

![C#](https://img.shields.io/badge/C%23-100%25-239120?logo=c-sharp)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core-512BD4)
![MVVM](https://img.shields.io/badge/Pattern-MVVM-blue)

## ✨ Features

### User Management
- 👤 **User Registration**: Create new accounts with secure credentials
- 🔐 **User Login**: Authenticate and access your personal game library
- 📱 **Profile Management**: Manage your user information

### Game Library
- 📚 **Personal Library**: View and manage your collection of games
- ➕ **Add Games**: Add new games to your library
- 📝 **Game Details**: View comprehensive information about each game
- ✏️ **Update Games**: Edit game information and details
- 🗑️ **Remove Games**: Delete games from your library

### Technical Features
- 💾 **Database Persistence**: All data stored using Entity Framework Core
- 🔄 **Real-time Updates**: Changes reflected immediately in the UI
- 🎨 **Modern UI**: Clean and intuitive WPF interface
- 📱 **Responsive Design**: Adaptive layouts for different window sizes

## 🏗️ Architecture

### MVVM Pattern Implementation

The application strictly follows the **Model-View-ViewModel (MVVM)** pattern:

```
┌─────────────────────────────────────────┐
│              View (XAML)                │
│    User Interface & Data Binding        │
└──────────────┬──────────────────────────┘
               │ Data Binding
┌──────────────▼──────────────────────────┐
│           ViewModel                     │
│   Business Logic & Commands             │
└──────────────┬──────────────────────────┘
               │
┌──────────────▼──────────────────────────┐
│             Model                       │
│   Data Entities & Services              │
└─────────────────────────────────────────┘
```

### Project Structure

```
Game-Library/
├── Models/                    # Data entities
│   ├── Game. cs               # Game entity
│   ├── User.cs               # User entity
│   └── Comment.cs            # Comment entity
├── ViewModels/               # ViewModels (business logic)
│   ├── BaseViewModel.cs      # Base ViewModel with INotifyPropertyChanged
│   ├── MainViewModel.cs      # Main window ViewModel
│   ├── LoginViewModel.cs     # Login functionality
│   ├── RegistrationViewModel. cs        # User registration
│   ├── UserLibraryViewModel.cs         # User's game library
│   ├── GameDetailsViewModel.cs         # Game details display
│   ├── AddGameToUserViewModel.cs       # Add games to library
│   ├── UpdateAddGameViewModel.cs       # Update game information
│   └── AddCommentViewModel.cs          # Add comments to games
├── Views/                    # XAML Views
│   ├── MainView.xaml         # Main application window
│   ├── LoginView.xaml        # Login screen
│   ├── RegistrationView.xaml           # Registration screen
│   ├── UserLibraryView.xaml            # Library display
│   ├── GameDetailsView.xaml            # Game details page
│   ├── AddGameToUserView. xaml          # Add game dialog
│   ├── UpdateAddGameView.xaml          # Update game dialog
│   └── AddCommentView.xaml             # Comment dialog
├── Services/                 # Business services
├── Messages/                 # Messaging infrastructure
├── MyDbContext.cs           # Entity Framework DbContext
├── App.xaml                 # Application configuration
├── config.json              # Application settings
├── EFRelationships.sln      # Visual Studio solution
└── EFRelationships.csproj   # Project file
```

## 🛠️ Tech Stack

### Core Technologies
- **Framework**: . NET WPF (Windows Presentation Foundation)
- **Language**: C# 100%
- **Database ORM**: Entity Framework Core
- **Pattern**: MVVM (Model-View-ViewModel)

### Key Libraries & Packages
- **Entity Framework Core**: Database operations and migrations
- **XAML**: Declarative UI markup
- **INotifyPropertyChanged**: Data binding support
- **Dependency Injection**: Service management

## 📋 Prerequisites

- Windows 10/11
- .NET 6.0 SDK or later
- Visual Studio 2022 (recommended) or Visual Studio Code
- SQL Server or SQL Server Express (for database)

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/mirce085/Game-Library.git
cd Game-Library
```

### 2.  Configure Database Connection

Update the `config.json` file with your database connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=GameLibraryDb;Trusted_Connection=true;"
  }
}
```

### 3. Open in Visual Studio

1. Double-click `EFRelationships.sln`
2. Wait for Visual Studio to restore NuGet packages

### 4. Apply Database Migrations

Open the **Package Manager Console** in Visual Studio:

```powershell
Update-Database
```

Or using . NET CLI:

```bash
dotnet ef database update
```

### 5. Run the Application

- Press `F5` in Visual Studio to run with debugging
- Or press `Ctrl+F5` to run without debugging

## 💾 Database Schema

### Entities

**User**
- Id (Primary Key)
- Username
- Password (hashed)
- Email
- Games (Collection)
- Comments (Collection)

**Game**
- Id (Primary Key)
- Title
- Description
- Developer
- Publisher
- ReleaseDate
- Genre
- Users (Many-to-Many)
- Comments (Collection)

**Comment**
- Id (Primary Key)
- Text
- CreatedDate
- UserId (Foreign Key)
- GameId (Foreign Key)

### Relationships
- **User ↔ Game**: Many-to-Many (Users can own multiple games)
- **User → Comment**: One-to-Many (Users can write multiple comments)
- **Game → Comment**: One-to-Many (Games can have multiple comments)

## 🎯 Key Features Explained

### MVVM Implementation

#### ViewModels
All ViewModels inherit from `BaseViewModel` which implements `INotifyPropertyChanged`:

```csharp
public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
```

#### Data Binding
Views bind to ViewModel properties using XAML data binding:

```xml
<TextBlock Text="{Binding GameTitle}" />
<Button Command="{Binding AddGameCommand}" />
```

### Entity Framework Integration

The `MyDbContext` class manages database operations:

```csharp
public class MyDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Comment> Comments { get; set; }
}
```

## 🎨 User Interface

### Main Features

1.  **Login Screen**: Authenticate users
2. **Registration**: Create new accounts
3. **Library View**: Browse your game collection
4. **Game Details**: View comprehensive game information
5. **Add/Edit Games**: Manage your library

## 🚧 Future Enhancements

- [ ] Comment and review system
- [ ] Community interaction features
- [ ] Game search and filtering functionality
- [ ] Advanced user profiles with avatars
- [ ] Friend system and social features
- [ ] Game rating system (5-star ratings)
- [ ] Game categories and tags
- [ ] Play time tracking
- [ ] Achievement system
- [ ] Cloud save synchronization
- [ ] Game cover image support
- [ ] Dark/Light theme toggle
- [ ] Export library to CSV/JSON
- [ ] Steam API integration
- [ ] Wishlist feature
- [ ] Price tracking for games

## 🎓 Learning Outcomes

This project demonstrates:

- **MVVM Pattern**: Proper separation of concerns
- **Entity Framework Core**: ORM usage and migrations
- **WPF Development**: Modern desktop application design
- **Data Binding**: Two-way binding in XAML
- **Command Pattern**: ICommand implementation
- **Dependency Injection**: Service-based architecture
- **Database Relationships**: One-to-Many, Many-to-Many
- **CRUD Operations**: Create, Read, Update, Delete
- **Navigation**: Multi-window WPF application

## 🤝 Contributing

Contributions are welcome! To contribute:

1. Fork the repository
2.  Create a feature branch (`git checkout -b feature/AmazingFeature`)
3.  Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 Code Standards

- Follow C# naming conventions (PascalCase for public members)
- Use async/await for database operations
- Keep ViewModels testable and free of UI code
- Document public APIs with XML comments
- Follow MVVM pattern strictly

## 📄 License

See `LICENSE. txt` for license information.

## 📧 Contact

**Developer**: [mirce085](https://github.com/mirce085)

For questions, feature requests, or bug reports, please open an issue on GitHub. 

## 🙏 Acknowledgments

- Inspired by Steam and other digital game distribution platforms
- Built with modern . NET technologies
- Community-driven development

---

🎮 **Organize your gaming life with style! ** 📚

Built with ❤️ using C#, WPF, and Entity Framework Core
