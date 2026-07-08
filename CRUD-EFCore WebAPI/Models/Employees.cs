namespace CRUDEFCore.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // FK ke Department (many-to-one: banyak Employee -> 1 Department)
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;

        // Many-to-many dengan Equipment
        public List<Equipment> EquipmentList { get; set; } = new();
    }
}
