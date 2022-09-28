using Hotel.Booking.Common.Constant;
using Reservation.Management.Event.Processor.Interfaces;
using Reservation.Management.Service.Interfaces;
using System.Text.Json;
using System.Threading.Tasks;

namespace Reservation.Management.Event.Processor.Services
{
    public class RoleEventProcessor : IRoleEventProcessor
    {
        private readonly IRoleService _roleService;

        public RoleEventProcessor(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task ProcessRoleEventAsync(int eventType, string eventPayload)
        {
            switch (eventType)
            {
                case (int)RoleEventType.Created:
                    {
                        var @event = JsonSerializer.Deserialize<Model.Role>(eventPayload);
                        await _roleService.CreateRoleAsync(@event);
                    }
                    break;
                case (int)RoleEventType.Updated:
                    {
                        var @event = JsonSerializer.Deserialize<Model.Role>(eventPayload);
                        await _roleService.UpdateRoleAsync(@event);
                    }
                    break;
                case (int)RoleEventType.Deleted:
                    {
                        var @event = JsonSerializer.Deserialize<int>(eventPayload);
                        await _roleService.DeleteRoleAsync(@event);
                    }
                    break;
            }
        }
    }
}
