using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUD_EFCore.Migrations
{
    /// <inheritdoc />
    public partial class AddExplicitManyToManyMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEquipment_Employees_EmployeesId",
                table: "EmployeeEquipment");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEquipment_Equipments_EquipmentListId",
                table: "EmployeeEquipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeEquipment",
                table: "EmployeeEquipment");

            migrationBuilder.RenameTable(
                name: "EmployeeEquipment",
                newName: "EquipmentEmployee");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeEquipment_EquipmentListId",
                table: "EquipmentEmployee",
                newName: "IX_EquipmentEmployee_EquipmentListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EquipmentEmployee",
                table: "EquipmentEmployee",
                columns: new[] { "EmployeesId", "EquipmentListId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentEmployee_Employees_EmployeesId",
                table: "EquipmentEmployee",
                column: "EmployeesId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentEmployee_Equipments_EquipmentListId",
                table: "EquipmentEmployee",
                column: "EquipmentListId",
                principalTable: "Equipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentEmployee_Employees_EmployeesId",
                table: "EquipmentEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentEmployee_Equipments_EquipmentListId",
                table: "EquipmentEmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EquipmentEmployee",
                table: "EquipmentEmployee");

            migrationBuilder.RenameTable(
                name: "EquipmentEmployee",
                newName: "EmployeeEquipment");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentEmployee_EquipmentListId",
                table: "EmployeeEquipment",
                newName: "IX_EmployeeEquipment_EquipmentListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeEquipment",
                table: "EmployeeEquipment",
                columns: new[] { "EmployeesId", "EquipmentListId" });

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
    }
}
