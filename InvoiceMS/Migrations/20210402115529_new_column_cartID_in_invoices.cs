using Microsoft.EntityFrameworkCore.Migrations;

namespace InvoiceMS.Migrations
{
    public partial class new_column_cartID_in_invoices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartID",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShoppingCartID",
                table: "Invoices");
        }
    }
}
