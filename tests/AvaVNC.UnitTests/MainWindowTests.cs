using Avalonia.Controls;

namespace AvaVNC.Tests;

public class MainWindowTests
{
    [AvaloniaFact]
    public void Test1()
    {
        var window = new MainWindow{ DataContext = new MainWindowViewModel()};
        window.Show();
        var AIInput = window.GetVisualDescendants().OfType<TextBox>().FirstOrDefault();
        AIInput?.Focus();
        window.KeyTextInput("A");
        var pic = window.CaptureRenderedFrame();
        pic?.Save("window.png");
       Assert.Equal("A", AIInput.Text);
    }
}
