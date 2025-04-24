using System;
using System.Reactive;
using System.Threading.Tasks;
using MarcusW.VncClient;
using MarcusW.VncClient.Protocol.SecurityTypes;
using MarcusW.VncClient.Security;
using ReactiveUI;

namespace AvaVNC.Services;

public class InteractiveAuthenticationHandler: IAuthenticationHandler
{
    public Interaction<Unit, string?> EnterPasswordInteraction { get; } = new();

    /// <inhertitdoc />
    public async Task<TInput> ProvideAuthenticationInputAsync<TInput>(RfbConnection connection, ISecurityType securityType, IAuthenticationInputRequest<TInput> request)
        where TInput : class, IAuthenticationInput
    {
        if (typeof(TInput) == typeof(PasswordAuthenticationInput))
        {
            // string? password = await Dispatcher.UIThread.InvokeAsync(async () => await EnterPasswordInteraction.Handle(Unit.Default)).ConfigureAwait(false);
            string password = "123456";
            // TODO: Implement canceling of authentication input requests instead of passing an empty password!
            if (password == null)
                password = string.Empty;

            return (TInput)Convert.ChangeType(new PasswordAuthenticationInput(password), typeof(TInput));
        }

        throw new InvalidOperationException("The authentication input request is not supported by the interactive authentication handler.");
    }
}