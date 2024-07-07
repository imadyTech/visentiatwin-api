using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisentiaTwin_API.Migrations
{
    /// <inheritdoc />
    public partial class updateSchema_20240616A : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "posX",
                table: "VTNodeComponents",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "posY",
                table: "VTNodeComponents",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "posZ",
                table: "VTNodeComponents",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "rotX",
                table: "VTNodeComponents",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "rotY",
                table: "VTNodeComponents",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "rotZ",
                table: "VTNodeComponents",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "sclX",
                table: "VTNodeComponents",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "sclY",
                table: "VTNodeComponents",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "sclZ",
                table: "VTNodeComponents",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "posX",
                table: "VTNodeComponents");

            migrationBuilder.DropColumn(
                name: "posY",
                table: "VTNodeComponents");

            migrationBuilder.DropColumn(
                name: "posZ",
                table: "VTNodeComponents");

            migrationBuilder.DropColumn(
                name: "rotX",
                table: "VTNodeComponents");

            migrationBuilder.DropColumn(
                name: "rotY",
                table: "VTNodeComponents");

            migrationBuilder.DropColumn(
                name: "rotZ",
                table: "VTNodeComponents");

            migrationBuilder.DropColumn(
                name: "sclX",
                table: "VTNodeComponents");

            migrationBuilder.DropColumn(
                name: "sclY",
                table: "VTNodeComponents");

            migrationBuilder.DropColumn(
                name: "sclZ",
                table: "VTNodeComponents");
        }
    }
}
