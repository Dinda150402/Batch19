namespace CRUDEFCore.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;

        public List<Equipment> EquipmentList { get; set; } = new();
    }
}