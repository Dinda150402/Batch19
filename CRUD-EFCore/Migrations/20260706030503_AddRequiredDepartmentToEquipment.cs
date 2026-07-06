using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUD_EFCore.Migrations
{
    /// <inheritdoc />
    public partial class AddRequiredDepartmentToEquipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEquipment_Employees_Employeesid",
                table: "EmployeeEquipment");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEquipment_Equipments_EquipmentsListID",
                table: "EmployeeEquipment");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Equipments",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Employees",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Employeesid",
                table: "EmployeeEquipment",
                newName: "EmployeesId");

            migrationBuilder.RenameColumn(
                name: "EquipmentsListID",
                table: "EmployeeEquipment",
                newName: "EquipmentListId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeEquipment_EquipmentsListID",
                table: "EmployeeEquipment",
                newName: "IX_EmployeeEquipment_EquipmentListId");

            migrationBuilder.AddColumn<string>(
                name: "RequiredDepartment",
                table: "Equipments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeEquipment_Employees_EmployeesId",
                table: "EmployeeEquipment",
                column: "EmployeesId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeEquipment_Equipments_EquipmentListId",
                table: "EmployeeEquipment",
                column: "EquipmentListId",
                principalTable: "Equipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEquipment_Employees_EmployeesId",
                table: "EmployeeEquipment");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEquipment_Equipments_EquipmentListId",
                table: "EmployeeEquipment");

            migrationBuilder.DropColumn(
                name: "RequiredDepartment",
                table: "Equipments");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Equipments",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Employees",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "EmployeesId",
                table: "EmployeeEquipment",
                newName: "Employeesid");

            migrationBuilder.RenameColumn(
                name: "EquipmentListId",
                table: "EmployeeEquipment",
                newName: "EquipmentsListID");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeEquipment_EquipmentListId",
                table: "EmployeeEquipment",
                newName: "IX_EmployeeEquipment_EquipmentsListID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeEquipment_Employees_Employeesid",
                table: "EmployeeEquipment",
                column: "Employeesid",
                principalTable: "Employees",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeEquipment_Equipments_EquipmentsListID",
                table: "EmployeeEquipment",
                column: "EquipmentsListID",
                principalTable: "Equipments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
