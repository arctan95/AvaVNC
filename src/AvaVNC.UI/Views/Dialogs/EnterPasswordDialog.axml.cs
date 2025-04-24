using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace AvaVNC.Views.Dialogs;

public partial class EnterPasswordDialog : Window
{

    public EnterPasswordDialog()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public void OnCancelClick(object sender, RoutedEventArgs e)
    {
        Close(null);
    }

    public void OnOkClick(object sender, RoutedEventArgs e)
    {
        Close(PasswordTextBox.Text);
    }
}