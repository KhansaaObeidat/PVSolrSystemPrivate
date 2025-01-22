using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PVSolrSystemPrivate.Migrations
{
    /// <inheritdoc />
    public partial class custmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BillAfter",
                table: "customers",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BillBefore",
                table: "customers",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Consumption",
                table: "customers",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentPath",
                table: "customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SolarSystemSize",
                table: "customers",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillAfter",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "BillBefore",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "Consumption",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "DocumentPath",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "SolarSystemSize",
                table: "customers");
        }
    }
}
