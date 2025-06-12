public class TrashDTO
{
    public int Id { get; set; }
    public DateTime DateCollected { get; set; }
    public string DagCategorie { get; set; } = string.Empty;
    public string TypeAfval { get; set; } = string.Empty;
    public string? WindRichting { get; set; }
    public double Temperatuur { get; set; }
    public string? WeerOmschrijving { get; set; }
    public double Confidence { get; set; }
    public int CameraId { get; set; }
}
