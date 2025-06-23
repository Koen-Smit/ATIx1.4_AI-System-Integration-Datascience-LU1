namespace TrashDashboard.Models
{
    public class PredictedTrash
    {
        public DateTime Date { get; set; }
        public int PredictedTemprature { get; set; }
        public bool IsHoliday { get; set; }
        public int PredictedWasteCount { get; set; }
    }
}
