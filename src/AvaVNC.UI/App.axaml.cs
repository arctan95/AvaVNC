using System.Windows.Input;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaVNC.Services;
using AvaVNC.Views;
using ReactiveUI;
using Splat;

namespace AvaVNC;

public partial class App : Application
{
    
    private IClassicDesktopStyleApplicationLifetime? _lifetime;
    public ICommand AboutCommand { get; }
    public ICommand QuitCommand { get; }
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        
        // Register dependencies
        Locator.CurrentMutable.RegisterLazySingleton(() => new ConnectionManager());
        Locator.CurrentMutable.RegisterLazySingleton(() => new InteractiveAuthenticationHandler());
    }

    public App()
    {
        AboutCommand = ReactiveCommand.Create(ShowAboutWindow);
        QuitCommand = ReactiveCommand.Create(QuitApplication);
        DataContext = this;
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            _lifetime = desktop;
            // desktop.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            desktop.MainWindow = new MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
    
    void ShowAboutWindow()
    {
        var window = new AboutWindow();

        window.Show();
    }

    void QuitApplication()
    {
        _lifetime?.Shutdown();
    }

}