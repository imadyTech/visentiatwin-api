using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisentiaTwin_API.Migrations
{
    /// <inheritdoc />
    public partial class updateSchema_20240607 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "VTNodes",
                newName: "VTNodeId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "VTComponents",
                newName: "VTComponentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VTNodeId",
                table: "VTNodes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "VTComponentId",
                table: "VTComponents",
                newName: "Id");
        }
    }
}
