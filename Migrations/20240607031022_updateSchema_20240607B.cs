using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisentiaTwin_API.Migrations
{
    /// <inheritdoc />
    public partial class updateSchema_20240607B : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VTNodes_VTSystems_VTSystemId",
                table: "VTNodes");

            migrationBuilder.AddForeignKey(
                name: "FK_VTNodes_VTSystems_VTSystemId",
                table: "VTNodes",
                column: "VTSystemId",
                principalTable: "VTSystems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VTNodes_VTSystems_VTSystemId",
                table: "VTNodes");

            migrationBuilder.AddForeignKey(
                name: "FK_VTNodes_VTSystems_VTSystemId",
                table: "VTNodes",
                column: "VTSystemId",
                principalTable: "VTSystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
