using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeService.Infrastructure.DataAccess.Migrations
{
    public partial class ModelReposition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_departments_companies_company_id",
                table: "departments");

            migrationBuilder.DropForeignKey(
                name: "fk_employees_departments_department_id",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "fk_employees_passports_passport_id",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "fk_passports_passport_types_passport_type_id",
                table: "passports");

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

            migrationBuilder.AddForeignKey(
                name: "fk_passports_passport_types_passport_type_model_id",
                table: "passports",
                column: "passport_type_id",
                principalTable: "passport_types",
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

            migrationBuilder.DropForeignKey(
                name: "fk_passports_passport_types_passport_type_model_id",
                table: "passports");

            migrationBuilder.AddForeignKey(
                name: "fk_departments_companies_company_id",
                table: "departments",
                column: "company_id",
                principalTable: "companies",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_employees_departments_department_id",
                table: "employees",
                column: "department_id",
                principalTable: "departments",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_employees_passports_passport_id",
                table: "employees",
                column: "passport_id",
                principalTable: "passports",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_passports_passport_types_passport_type_id",
                table: "passports",
                column: "passport_type_id",
                principalTable: "passport_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
