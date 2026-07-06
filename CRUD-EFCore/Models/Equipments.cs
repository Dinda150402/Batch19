namespace CRUDEFCore.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public DateTime LastCalibrationDate { get; set; }
        public string? RequiredDepartment { get; set; }
        public List<Employee> Employees { get; set; } = new ();
    }
}

