using CRUDEFCore.Data;
using CRUDEFCore.Models;
using Microsoft.EntityFrameworkCore;

using var db = new AppDbContext();


Employee AddEmployee(string name, string dept)
{
    var employee = new Employee 
    { 
        Name = name, 
        Department = dept 
    };
    db.Employees.Add(employee);
    db.SaveChanges();
    return employee;
}

Equipment AddEquipment(string name, string serial)
{
    var equipment = new Equipment
    {
        Name = name,
        SerialNumber = serial,
        LastCalibrationDate = DateTime.Now
    };
    db.Equipments.Add(equipment);
    db.SaveChanges();
    return equipment;
}

void AssignEquipmentToEmployee(int equipmentId, int employeeId)
{
    var equipment = db.Equipments.Include(e => e.Employees)
                                  .FirstOrDefault(e => e.Id == equipmentId);
    var employee = db.Employees.Find(employeeId);

    if (equipment != null && employee != null)
    {
        equipment.Employees.Add(employee);
        db.SaveChanges();
    }
}

void ListEquipments()
{
    var equipments = db.Equipments.Include(e => e.Employees).ToList();
    foreach (var eq in equipments)
    {
        Console.WriteLine($"{eq.Id} - {eq.Name} ({eq.SerialNumber})");
        foreach (var emp in eq.Employees)
            Console.WriteLine($"    dipegang oleh: {emp.Name}");
    }
}

void UpdateEquipment(int id, string newName)
{
    var eq = db.Equipments.Find(id);
    if (eq != null)
    {
        eq.Name = newName;
        db.SaveChanges();
    }
}

void DeleteEquipment(int id)
{
    var eq = db.Equipments.Find(id);
    if (eq != null)
    {
        db.Equipments.Remove(eq);
        db.SaveChanges();
    }
}

var budi = AddEmployee("Budi", "Lab RnD");
var ani = AddEmployee("Ani", "QA");
var microscope = AddEquipment("Microscope A", "SN-001");

AssignEquipmentToEmployee(microscope.Id, budi.Id);
AssignEquipmentToEmployee(microscope.Id, ani.Id);

ListEquipments();

Console.WriteLine("\n--- UPDATE ---");
UpdateEquipment(microscope.Id, "Microscope A (Updated)");
ListEquipments();

Console.WriteLine("\n--- DELETE ---");
DeleteEquipment(microscope.Id);
ListEquipments();