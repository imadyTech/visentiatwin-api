using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisentiaTwin_API.Migrations
{
    /// <inheritdoc />
    public partial class updateSchema_20240607A : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VTNodes_VTSystems_VTSystemId",
                table: "VTNodes");

            migrationBuilder.AlterColumn<int>(
                name: "VTSystemId",
                table: "VTNodes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VTNodes_VTSystems_VTSystemId",
                table: "VTNodes",
                column: "VTSystemId",
                principalTable: "VTSystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VTNodes_VTSystems_VTSystemId",
                table: "VTNodes");

            migrationBuilder.AlterColumn<int>(
                name: "VTSystemId",
                table: "VTNodes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_VTNodes_VTSystems_VTSystemId",
                table: "VTNodes",
                column: "VTSystemId",
                principalTable: "VTSystems",
                principalColumn: "Id");
        }
    }
}
