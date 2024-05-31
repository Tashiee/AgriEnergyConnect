namespace AgriEnergyConnect.Application.Employee.ViewFarmers;

public  class ViewFarmersResponse
{
    public int Count { get; set; }
    public IEnumerable<FarmersModel>? Items { get; set; } = new List<FarmersModel>();
}