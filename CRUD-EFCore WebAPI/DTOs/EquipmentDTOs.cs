namespace CRUDEFCore.DTOs
{
    public class EquipmentReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public DateTime LastCalibrationDate { get; set; }
        public string? RequiredDepartmentName { get; set; }
        public List<string> AssignedEmployeeNames { get; set; } = new();
        public int MaintenanceLogCount { get; set; }
    }

    public class EquipmentCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public int? RequiredDepartmentId { get; set; }
    }

    public class EquipmentUpdateDto
    {
        public string Name { get; set; } = string.Empty;
    }
}
