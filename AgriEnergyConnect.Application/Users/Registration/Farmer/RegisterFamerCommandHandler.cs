using AgriEnergyConnect.Application.Abstractions.Authentication;
using AgriEnergyConnect.Application.Abstractions.Messaging;
using AgriEnergyConnect.Domain.Abstractions;
using AgriEnergyConnect.Domain.Users;

namespace AgriEnergyConnect.Application.Users.Registration.Farmer;

public sealed class RegisterFamerCommandHandler(
    IAuthenticationService authenticationService,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork
    ) : ICommandHandler<FarmerRegistrationCommand, string>
{
    public async Task<Result<string>> Handle(FarmerRegistrationCommand request, CancellationToken cancellationToken)
    {
        var farmer = User.FarmerFactory(
            new FirstName(request.firstName),
            new LastName(request.lastName),
            new Email(request.email)
        );  
        
        var identityId = await authenticationService.RegisterAsync(
            farmer,
            request.password,
            cancellationToken);
        
        if (identityId == null)
        {
            return Result.Failure<string>(
                new Error("User.Registration","Failed to register employee please try again later"));
        }
         
        farmer.SetIdentityId(identityId);
         
        userRepository.Add(farmer);
         
        await unitOfWork.SaveChangesAsync(cancellationToken);
         
        return farmer.Id.ToString();
    }
} 