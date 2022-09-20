using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class latestmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_BankConfigurations_BankConfigurationId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_BankConfigurationId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BankConfigurationId",
                table: "Customers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BankConfigurationId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BankConfigurationId",
                table: "Customers",
                column: "BankConfigurationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_BankConfigurations_BankConfigurationId",
                table: "Customers",
                column: "BankConfigurationId",
                principalTable: "BankConfigurations",
                principalColumn: "Id");
        }
    }
}
