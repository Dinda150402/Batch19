namespace CRUDEFCore.DTOs
{
    public class MaintenanceLogReadDto
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; } = string.Empty;
        public DateTime MaintenanceDate { get; set; }
        public string PerformedBy { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
    }

    public class MaintenanceLogCreateDto
    {
        public int EquipmentId { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public string PerformedBy { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
    }
}
