using AgriEnergyConnect.Application.Abstractions.Messaging;
using AgriEnergyConnect.Domain.Abstractions;
using AgriEnergyConnect.Domain.Users;

namespace AgriEnergyConnect.Application.Employee.ViewFarmers;

public sealed class ViewFarmersQueryHandler(IUserRepository userRepository)
    : IQueryHandler<ViewFarmersQuery, ViewFarmersResponse>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result<ViewFarmersResponse>> Handle(ViewFarmersQuery request, CancellationToken cancellationToken)
    {
        var farmers = await _userRepository.GetAllFarmersAsync(cancellationToken);
        var count = farmers.Count();
        return new ViewFarmersResponse()
        { 
            Count = count,
            Items = farmers.Select(farmer=> new FarmersModel
            {
                Id = farmer.Id.ToString(),
                FirstName = farmer.FirstName.Value.ToString(),
                LastName = farmer.LastName.Value.ToString(),
                Email = farmer.Email.Value.ToString()
            })
        };
    }
}  