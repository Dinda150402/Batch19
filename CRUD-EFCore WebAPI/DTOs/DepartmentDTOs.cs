namespace CRUDEFCore.DTOs
{
    public class DepartmentReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int EmployeeCount { get; set; }
    }

    public class DepartmentCreateDto
    {
        public string Name { get; set; } = string.Empty;
    }
}
