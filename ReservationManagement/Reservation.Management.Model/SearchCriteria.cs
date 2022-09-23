using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Management.Model
{
    public class SearchCriteria
    {
        public string City { get; set; }
        public string? State { get; set; }
        public string Country { get; set; }
        public int? CityId { get; set; } = null;
        public string? Hotel { get; set; }
        public int? HotelId { get; set; } = null;
        public int? HotelCategoryId { get; set; } = null;
        public int? RoomType { get; set; }        
        public decimal? MinPrice { get; set; } = null;
        public decimal? MaxPrice { get; set; } = null;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
