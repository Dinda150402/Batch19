namespace CRUDEFCore.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public DateTime LastCalibrationDate { get; set; }

        // FK opsional ke Department yang disyaratkan buat assign (null = bebas semua department)
        public int? RequiredDepartmentId { get; set; }
        public Department? RequiredDepartment { get; set; }

        // Many-to-many dengan Employee
        public List<Employee> Employees { get; set; } = new();

        // One Equipment -> many MaintenanceLog (riwayat kalibrasi/maintenance)
        public List<MaintenanceLog> MaintenanceLogs { get; set; } = new();
    }
}
