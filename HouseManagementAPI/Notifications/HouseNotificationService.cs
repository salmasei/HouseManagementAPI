using System;
using System.Threading.Tasks;

namespace HouseManagementAPI.Notifications
{
    public class HouseNotificationService
    {
        // Method to notify when a house is added
        public async Task NotifyHouseAdded(string address)
        {
            // Logic to send notification (e.g., email, SMS, etc.)
            Console.WriteLine($"Notification: A new house has been added at {address}.");
            await Task.CompletedTask; // Simulate async behavior
        }

        // Method to notify when a house is updated
        public async Task NotifyHouseUpdated(string address)
        {
            Console.WriteLine($"Notification: The house at {address} has been updated.");
            await Task.CompletedTask;
        }

        // Method to notify when a house is deleted
        public async Task NotifyHouseDeleted(string address)
        {
            Console.WriteLine($"Notification: The house at {address} has been deleted.");
            await Task.CompletedTask;
        }
    }
}
