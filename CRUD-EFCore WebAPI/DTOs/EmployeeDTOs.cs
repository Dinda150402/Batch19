namespace CRUDEFCore.DTOs
{
    public class EmployeeReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public List<string> AssignedEquipmentNames { get; set; } = new();
    }

    public class EmployeeCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
    }
}
