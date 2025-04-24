using Avalonia;
using Avalonia.Headless;
using Avalonia.ReactiveUI;
using AvaVNC.Tests;

[assembly: AvaloniaTestApplication(typeof(TestAppBuilder))]

namespace AvaVNC.Tests;

public class TestAppBuilder
{
    public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<App>()
        .UseReactiveUI()
        .UseSkia()
        .UseHeadless(new AvaloniaHeadlessPlatformOptions{UseHeadlessDrawing = false});
}