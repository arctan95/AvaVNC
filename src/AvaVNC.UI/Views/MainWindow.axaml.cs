using Avalonia.Controls;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using AvaVNC.ViewModels;
using AvaVNC.Views.Dialogs;
using ReactiveUI;

namespace AvaVNC.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{

    public MainWindow()
    {
      
        InitializeComponent();
        if (Design.IsDesignMode) return;
        InitializeWindow();
#if DEBUG
        this.AttachDevTools();
#endif
    }
    
    public  void OnEnableFullscreenButtonClicked(object? sender, RoutedEventArgs e) => SetFullscreenMode(true);

    private void SetFullscreenMode(bool fullscreen)
    {
        WindowState = fullscreen ? WindowState.FullScreen : WindowState.Normal;
    
        TopDockPanel.IsVisible = !fullscreen;
        BottomDockPanel.IsVisible = !fullscreen;
        RightDockPanel.IsVisible = !fullscreen;
    }

    private async Task DoShowEnterPasswordDialogAsync(IInteractionContext<Unit, string?> interaction)
    {
        var dialog = new EnterPasswordDialog();
        dialog.DataContext = interaction.Input;
        string? password = await dialog.ShowDialog<string?>(this);
        interaction.SetOutput(password);
    }
    
    private void InitializeWindow()
    {
        DataContext = new MainWindowViewModel();
        this.WhenActivated(action =>
        {
            // Handle authentication requests
            action(ViewModel!.InteractiveAuthenticationHandler.EnterPasswordInteraction.RegisterHandler(DoShowEnterPasswordDialogAsync));
            // Bind connect button text to connect command execution
            action(ConnectButton.Bind(Button.ContentProperty, ViewModel.ConnectCommand.IsExecuting.Select(executing => executing ? "Connecting..." : "Connect")));
        });
        
        // Register keybinding for exiting fullscreen
        KeyBindings.Add(new KeyBinding {
            Gesture = new KeyGesture(Key.Escape, KeyModifiers.Control),
            Command = ReactiveCommand.Create(() => SetFullscreenMode(false))
        });

    }
    
}