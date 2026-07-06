namespace CRUDEFCore.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public List<Equipment> EquipmentsList { get; set; } = new ();
    }
}