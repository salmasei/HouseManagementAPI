using HouseManagementAPI.CQRS.Queries;
using HouseManagementAPI.Models;
using HouseManagementAPI.Repositories;
using System.Threading;
using System.Threading.Tasks;

public class GetHousesQueryHandler
{
    private readonly IHouseCacheService _houseCacheService;

    public GetHousesQueryHandler(IHouseCacheService houseCacheService)
    {
        _houseCacheService = houseCacheService;
    }

    public async Task<List<HouseModel>> Handle(GetHousesQuery query)
    {
        return await _houseCacheService.GetHousesAsync();
    }
}
