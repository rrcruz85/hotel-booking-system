
namespace Hotel.Booking.Common.Contract.Services
{
    public interface IConfigurationView
    {
        public string AppSettings(string name);
        public string ConnectionStrings(string name);
    }
}
