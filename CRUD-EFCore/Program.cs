using CRUDEFCore.Data;
using CRUDEFCore.Services;

using var db = new AppDbContext();
var employeeService = new EmployeeService(db);
var equipmentService = new EquipmentService(db);

bool running = true;

while (running)
{
    ShowMenu();
    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1": await AddEmployeeMenu(); break;
        case "2": await AddEquipmentMenu(); break;
        case "3": await AssignEquipmentMenu(); break;
        case "4": await ListEquipmentsMenu(); break;
        case "5": await ListEmployeesMenu(); break;
        case "6": await UpdateEquipmentMenu(); break;
        case "7": await DeleteEquipmentMenu(); break;
        case "8": await SearchEquipmentMenu(); break;
        case "9": await FilterEmployeeMenu(); break;
        case "0": running = false; break;
        default: Console.WriteLine("Pilihan tidak valid."); break;
    }
}

void ShowMenu()
{
    Console.WriteLine("\n=== MENU CRUD EQUIPMENT-EMPLOYEE ===");
    Console.WriteLine("1. Tambah Employee");
    Console.WriteLine("2. Tambah Equipment");
    Console.WriteLine("3. Assign Equipment ke Employee");
    Console.WriteLine("4. Lihat semua Equipment");
    Console.WriteLine("5. Lihat semua Employee");
    Console.WriteLine("6. Update Equipment");
    Console.WriteLine("7. Delete Equipment");
    Console.WriteLine("8. Cari Equipment");
    Console.WriteLine("9. Filter Employee berdasarkan Department");
    Console.WriteLine("0. Keluar");
    Console.Write("Pilih menu: ");
}

async Task AddEmployeeMenu()
{
    Console.Write("Nama employee: ");
    string name = Console.ReadLine() ?? "";
    Console.Write("Department: ");
    string dept = Console.ReadLine() ?? "";

    var employee = await employeeService.AddAsync(name, dept);
    Console.WriteLine($"Employee '{employee.Name}' berhasil ditambahkan dengan ID {employee.Id}");
}

async Task AddEquipmentMenu()
{
    Console.Write("Nama equipment: ");
    string name = Console.ReadLine() ?? "";
    Console.Write("Serial number: ");
    string serial = Console.ReadLine() ?? "";
    Console.Write("Khusus department? (kosongkan jika bebas semua): ");
    string dept = Console.ReadLine() ?? "";

    var equipment = await equipmentService.AddAsync(name, serial, string.IsNullOrWhiteSpace(dept) ? null : dept);
    Console.WriteLine($"Equipment '{equipment.Name}' berhasil ditambahkan dengan ID {equipment.Id}");
}

async Task AssignEquipmentMenu()
{
    var equipments = await equipmentService.GetAllAsync();
    var employees = await employeeService.GetAllAsync();

    if (!equipments.Any() || !employees.Any())
    {
        Console.WriteLine("Tambahkan employee dan equipment dulu sebelum assign.");
        return;
    }

    Console.WriteLine("--- Daftar Equipment ---");
    foreach (var eq in equipments)
    {
        string restriction = eq.RequiredDepartment == null ? "bebas" : $"khusus {eq.RequiredDepartment}";
        Console.WriteLine($"{eq.Id} - {eq.Name} ({restriction})");
    }

    Console.WriteLine("--- Daftar Employee ---");
    foreach (var emp in employees)
        Console.WriteLine($"{emp.Id} - {emp.Name} ({emp.Department})");

    Console.Write("Masukkan ID Equipment: ");
    int equipmentId = int.Parse(Console.ReadLine() ?? "0");
    Console.Write("Masukkan ID Employee: ");
    int employeeId = int.Parse(Console.ReadLine() ?? "0");

    var (success, message) = await equipmentService.AssignToEmployeeAsync(equipmentId, employeeId);
    Console.WriteLine((success ? "Sukses " : "Gagal") + message);
}

async Task ListEquipmentsMenu()
{
    var equipments = await equipmentService.GetAllAsync();

    Console.WriteLine("\n--- Daftar Equipment ---");
    foreach (var eq in equipments)
    {
        Console.WriteLine($"{eq.Id} - {eq.Name} ({eq.SerialNumber})");
        if (!eq.Employees.Any())
            Console.WriteLine("    (belum ada yang pegang)");
        else
            foreach (var emp in eq.Employees)
                Console.WriteLine($"    dipegang oleh: {emp.Name}");
    }
}

async Task ListEmployeesMenu()
{
    var employees = await employeeService.GetAllAsync();

    Console.WriteLine("\n--- Daftar Employee ---");
    foreach (var emp in employees)
    {
        string equipmentNames = emp.EquipmentList.Any()
            ? string.Join(", ", emp.EquipmentList.Select(e => e.Name))
            : "(tidak pegang equipment)";

        Console.WriteLine($"{emp.Id} - {emp.Name} ({emp.Department}) -> {equipmentNames}");
    }
}

async Task SearchEquipmentMenu()
{
    Console.Write("Cari equipment berdasarkan nama (bisa sebagian): ");
    string keyword = Console.ReadLine() ?? "";

    var results = await equipmentService.SearchByNameAsync(keyword);

    if (!results.Any())
    {
        Console.WriteLine("Tidak ditemukan.");
        return;
    }

    Console.WriteLine($"Ditemukan {results.Count} hasil:");
    foreach (var eq in results)
        Console.WriteLine($"{eq.Id} - {eq.Name} ({eq.SerialNumber})");
}

async Task FilterEmployeeMenu()
{
    Console.Write("Cari employee di department: ");
    string dept = Console.ReadLine() ?? "";

    var results = await employeeService.FilterByDepartmentAsync(dept);

    if (!results.Any())
    {
        Console.WriteLine("Tidak ditemukan.");
        return;
    }

    foreach (var emp in results)
        Console.WriteLine($"{emp.Id} - {emp.Name} ({emp.Department})");
}

async Task UpdateEquipmentMenu()
{
    Console.Write("Masukkan ID Equipment yang mau diupdate: ");
    int id = int.Parse(Console.ReadLine() ?? "0");

    Console.Write("Nama baru: ");
    string newName = Console.ReadLine() ?? "";

    bool success = await equipmentService.UpdateNameAsync(id, newName);
    Console.WriteLine(success ? "Equipment berhasil diupdate." : "Equipment tidak ditemukan.");
}

async Task DeleteEquipmentMenu()
{
    Console.Write("Masukkan ID Equipment yang mau dihapus: ");
    int id = int.Parse(Console.ReadLine() ?? "0");

    bool success = await equipmentService.DeleteAsync(id);
    Console.WriteLine(success ? "Equipment berhasil dihapus." : "Equipment tidak ditemukan.");
}