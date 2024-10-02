using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace HouseManagementAPI.Notifications
{
    public class HouseNotificationService
    {
        private readonly ILogger<HouseNotificationService> _logger;

        public HouseNotificationService(ILogger<HouseNotificationService> logger)
        {
            _logger = logger;
        }

        // Method to notify when a house is added
        public async Task NotifyHouseAdded(string address)
        {
            _logger.LogInformation($"Notification: A new house has been added at {address}.");
            await Task.CompletedTask; // Simulate async behavior
        }

        // Method to notify when a house is updated
        public async Task NotifyHouseUpdated(string address)
        {
            _logger.LogInformation($"Notification: The house at {address} has been updated.");
            await Task.CompletedTask;
        }

        // Method to notify when a house is deleted
        public async Task NotifyHouseDeleted(string address)
        {
            _logger.LogInformation($"Notification: The house at {address} has been deleted.");
            await Task.CompletedTask;
        }
    }
}
