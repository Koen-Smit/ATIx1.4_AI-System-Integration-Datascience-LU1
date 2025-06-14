using System.ComponentModel.DataAnnotations;

namespace TrashDashboard.Models
{
    public class Camera
    {
        public int Id { get; set; }
        public string Naam { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Postcode { get; set; } = string.Empty;
        public ICollection<Trash> TrashRecords { get; set; } = new List<Trash>();
    }
}
