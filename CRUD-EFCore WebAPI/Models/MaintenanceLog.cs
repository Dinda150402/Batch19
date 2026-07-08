namespace CRUDEFCore.Models
{
    public class MaintenanceLog
    {
        public int Id { get; set; }

        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; } = null!;

        public DateTime MaintenanceDate { get; set; }
        public string PerformedBy { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

        public string Result { get; set; } = string.Empty;
    }
}
