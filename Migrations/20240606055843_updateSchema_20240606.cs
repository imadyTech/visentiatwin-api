using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisentiaTwin_API.Migrations
{
    /// <inheritdoc />
    public partial class updateSchema_20240606 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VTComponents_VTNodes_VTNodeId",
                table: "VTComponents");

            migrationBuilder.DropForeignKey(
                name: "FK_VTNodes_VTComponents_SelectedComponentId",
                table: "VTNodes");

            migrationBuilder.DropIndex(
                name: "IX_VTNodes_SelectedComponentId",
                table: "VTNodes");

            migrationBuilder.DropIndex(
                name: "IX_VTComponents_VTNodeId",
                table: "VTComponents");

            migrationBuilder.DropColumn(
                name: "SelectedComponentId",
                table: "VTNodes");

            migrationBuilder.DropColumn(
                name: "VTNodeId",
                table: "VTComponents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SelectedComponentId",
                table: "VTNodes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VTNodeId",
                table: "VTComponents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VTNodes_SelectedComponentId",
                table: "VTNodes",
                column: "SelectedComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_VTComponents_VTNodeId",
                table: "VTComponents",
                column: "VTNodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_VTComponents_VTNodes_VTNodeId",
                table: "VTComponents",
                column: "VTNodeId",
                principalTable: "VTNodes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VTNodes_VTComponents_SelectedComponentId",
                table: "VTNodes",
                column: "SelectedComponentId",
                principalTable: "VTComponents",
                principalColumn: "Id");
        }
    }
}
