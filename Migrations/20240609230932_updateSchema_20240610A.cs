using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisentiaTwin_API.Migrations
{
    /// <inheritdoc />
    public partial class updateSchema_20240610A : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "VTSystems",
                newName: "SystemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SystemId",
                table: "VTSystems",
                newName: "Id");
        }
    }
}
