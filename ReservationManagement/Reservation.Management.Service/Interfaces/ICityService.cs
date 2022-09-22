
namespace Reservation.Management.Service.Interfaces
{
    public interface ICityService
    {
        Task<int> CreateCityAsync(Model.City city);
        Task UpdateCityAsync(Model.City city);
        Task DeleteCityAsync(int cityId);
        Task<Model.City?> GetCityByAsync(int cityId);
        Task<List<Model.City>> GetCitiesByNameAsync(string name);        
        Task<List<Model.City>> GetCitiesByStateCountryAsync(string state, string country);
        Task<List<Model.City>> GetCitiesByCountryAsync(string country);
    }
}
