
using Reservation.Management.Model;

namespace Reservation.Management.Service.Interfaces
{
    public interface IReservationRuleService
    {
        Task<bool> CheckRoomsAvailabiltyAsync(IReservationContext reservation);
        Task<IReservationRuleValidationResponse> CheckRulesOnCreateAsync(IReservationContext reservation);
        Task<IReservationRuleValidationResponse> CheckRulesOnUpdateAsync(IEditReservationContext reservation);
    }
}
