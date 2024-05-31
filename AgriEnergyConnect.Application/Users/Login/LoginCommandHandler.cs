using AgriEnergyConnect.Application.Abstractions.Authentication;
using AgriEnergyConnect.Application.Abstractions.Messaging;
using AgriEnergyConnect.Domain.Abstractions;

namespace AgriEnergyConnect.Application.Users.Login;

public sealed class LoginCommandHandler( IAuthenticationService authenticationService) : ICommandHandler<LoginCommand, bool>
{
    public async Task<Result<bool>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await authenticationService.LoginAsync(request.username, request.password, cancellationToken);
        
        if(result is null)
        {
            return false;
        }
        
        return true;
    }
}