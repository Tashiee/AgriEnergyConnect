using AgriEnergyConnect.Application.Abstractions.Authentication;
using AgriEnergyConnect.Application.Abstractions.Messaging;
using AgriEnergyConnect.Domain.Abstractions;
using AgriEnergyConnect.Domain.Users;
 
namespace AgriEnergyConnect.Application.Users.Registration.Employee;

public sealed class EmployeeRegistrationCommandHandler (
    IAuthenticationService authenticationService,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork
    ): ICommandHandler<EmployeeRegistrationCommand, string>
{
    public async Task<Result<string>> Handle(EmployeeRegistrationCommand request, CancellationToken cancellationToken)
    {
        var employee = User.EmployeeFactory(
            new FirstName(request.firstName),
            new LastName(request.lastName),
            new Email(request.email)
        );
        
        var identityId = await authenticationService.RegisterAsync(
            employee,
            request.password,
            cancellationToken);
        
        if(identityId is null)
        {
            return Result.Failure<string>(
                new Error("User.Registration","Failed to register employee please try again later"));
        }
        
        employee.SetIdentityId(identityId);
          
        userRepository.Add(employee);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return employee.Id.ToString();
    }
}
 