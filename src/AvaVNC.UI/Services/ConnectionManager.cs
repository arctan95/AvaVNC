using System;
using System.Threading;
using System.Threading.Tasks;
using AvaVNC.Adapters.Logging;
using MarcusW.VncClient;
using Microsoft.Extensions.Logging;
using Splat;

namespace AvaVNC.Services;

public class ConnectionManager
{
    private readonly InteractiveAuthenticationHandler _interactiveAuthenticationHandler;

    private readonly VncClient _vncClient;

    public ConnectionManager(InteractiveAuthenticationHandler? interactiveAuthenticationHandler = null)
    {
        _interactiveAuthenticationHandler = interactiveAuthenticationHandler ?? Locator.Current.GetService<InteractiveAuthenticationHandler>()
            ?? throw new ArgumentNullException(nameof(interactiveAuthenticationHandler));

        // Create and populate default logger factory for logging to Avalonia logging sinks
        var loggerFactory = new LoggerFactory();
        loggerFactory.AddProvider(new AvaloniaLoggerProvider());

        _vncClient = new VncClient(loggerFactory);
    }

    public Task<RfbConnection> ConnectAsync(ConnectParameters parameters, CancellationToken cancellationToken = default)
    {
        parameters.AuthenticationHandler = _interactiveAuthenticationHandler;

        // Uncomment for debugging/visualization purposes
        //parameters.RenderFlags |= RenderFlags.VisualizeRectangles;

        return _vncClient.ConnectAsync(parameters, cancellationToken);
    }
}