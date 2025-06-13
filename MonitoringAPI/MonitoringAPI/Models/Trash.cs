using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Trash
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime DateCollected { get; set; }

    //[Required]
    [MaxLength(50)]
    public string DagCategorie { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string TypeAfval { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? WindRichting { get; set; }

    [Range(-50, 60)]
    public double Temperatuur { get; set; }

    [MaxLength(100)]
    public string? WeerOmschrijving { get; set; }

    [Required]
    [Range(0.01, 1.0, ErrorMessage = "Confidence moet tussen 0.01 en 1.0 liggen")]
    public double Confidence { get; set; }

    // Foreign key relatie
    [ForeignKey("Camera")]
    public int CameraId { get; set; }

    [JsonIgnore]
    public Camera? Camera { get; set; }
}
