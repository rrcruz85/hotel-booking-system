
using Reservation.Management.Model;

namespace Reservation.Management.Service.Interfaces
{
    public interface ISearchService
    {        
        Task<List<Room>> Search(SearchCriteria searchCriteria);        
    }
}
