
namespace Hotel.Management.Model
{
    public class Hotel 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public int? CategoryId { get; set; }
    }
}
