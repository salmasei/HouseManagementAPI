using HouseManagementAPI.Models;

namespace HouseManagementAPI.CQRS.Commands
{
    public class AddHouseCommand
    {
        public HouseModel House { get; set; }
    }
}
