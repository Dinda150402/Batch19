namespace CRUDEFCore.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // One Department -> many Employee
        public List<Employee> Employees { get; set; } = new();

        // One Department -> many Equipment yang mensyaratkan department ini (optional)
        public List<Equipment> RestrictedEquipments { get; set; } = new();
    }
}
