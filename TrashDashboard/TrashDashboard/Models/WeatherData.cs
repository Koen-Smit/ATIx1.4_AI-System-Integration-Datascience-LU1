using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TrashDashboard.Models
{
    public class WeatherData
    {
        public string[] Dates { get; set; } = Array.Empty<string>();
        public double[] Temperaturen { get; set; } = Array.Empty<double>();
        public string[] WeerOmschrijvingen { get; set; } = Array.Empty<string>();
    }
}