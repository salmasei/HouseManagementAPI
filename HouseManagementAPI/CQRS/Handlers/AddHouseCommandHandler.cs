using HouseManagementAPI.CQRS.Commands;
using HouseManagementAPI.Caching;
using HouseManagementAPI.Notifications;
using System.Threading.Tasks;
using HouseManagementAPI.Repositories;

public class AddHouseCommandHandler
{
    private readonly IHouseCacheService _houseCacheService;
    private readonly HouseNotificationService _notificationService;

    // Inject HouseNotificationService along with cache service
    public AddHouseCommandHandler(IHouseCacheService houseCacheService, HouseNotificationService notificationService)
    {
        _houseCacheService = houseCacheService;
        _notificationService = notificationService;
    }

    public async Task Handle(AddHouseCommand command)
    {
        // Update cache
        await _houseCacheService.AddHouseAsync(command.House);

        // Notify that the house has been added
        await _notificationService.NotifyHouseAdded(command.House.Address);
    }
}
