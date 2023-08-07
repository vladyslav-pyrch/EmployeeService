using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeService.Infrastructure.DataAccess.Migrations
{
    public partial class Addedcascadedeleteoptionforsomeentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_departments_companies_company_model_id",
                table: "departments");

            migrationBuilder.DropForeignKey(
                name: "fk_employees_departments_department_model_id",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "fk_employees_passports_passport_model_id",
                table: "employees");

            migrationBuilder.AddForeignKey(
                name: "fk_departments_companies_company_model_id",
                table: "departments",
                column: "company_id",
                principalTable: "companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_employees_departments_department_model_id",
                table: "employees",
                column: "department_id",
                principalTable: "departments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_employees_passports_passport_model_id",
                table: "employees",
                column: "passport_id",
                principalTable: "passports",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_departments_companies_company_model_id",
                table: "departments");

            migrationBuilder.DropForeignKey(
                name: "fk_employees_departments_department_model_id",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "fk_employees_passports_passport_model_id",
                table: "employees");

            migrationBuilder.AddForeignKey(
                name: "fk_departments_companies_company_model_id",
                table: "departments",
                column: "company_id",
                principalTable: "companies",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_employees_departments_department_model_id",
                table: "employees",
                column: "department_id",
                principalTable: "departments",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_employees_passports_passport_model_id",
                table: "employees",
                column: "passport_id",
                principalTable: "passports",
                principalColumn: "id");
        }
    }
}
