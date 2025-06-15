using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Camera
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Naam { get; set; } = string.Empty;

    [Required]
    [Range(-90, 90, ErrorMessage = "Latitude moet tussen -90 en 90 zijn.")]
    public double Latitude { get; set; }

    [Required]
    [Range(-180, 180, ErrorMessage = "Longitude moet tussen -180 en 180 zijn.")]
    public double Longitude { get; set; }

    [Required]
    [MaxLength(10)]
    public string Postcode { get; set; } = string.Empty;


    [JsonIgnore]
    public ICollection<Trash> TrashRecords { get; set; } = new List<Trash>();
}