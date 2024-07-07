using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisentiaTwin_API.Migrations
{
    /// <inheritdoc />
    public partial class updateSchema_20240524a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VTSystems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estimatorString = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VTSystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VTComponents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<float>(type: "real", nullable: false),
                    estimatorString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VTNodeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VTComponents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VTNodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VTSystemId = table.Column<int>(type: "int", nullable: true),
                    SelectedComponentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VTNodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VTNodes_VTComponents_SelectedComponentId",
                        column: x => x.SelectedComponentId,
                        principalTable: "VTComponents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VTNodes_VTSystems_VTSystemId",
                        column: x => x.VTSystemId,
                        principalTable: "VTSystems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VTNodeComponents",
                columns: table => new
                {
                    VTNodeId = table.Column<int>(type: "int", nullable: false),
                    VTComponentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VTNodeComponents", x => new { x.VTNodeId, x.VTComponentId });
                    table.ForeignKey(
                        name: "FK_VTNodeComponents_VTComponents_VTComponentId",
                        column: x => x.VTComponentId,
                        principalTable: "VTComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VTNodeComponents_VTNodes_VTNodeId",
                        column: x => x.VTNodeId,
                        principalTable: "VTNodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VTComponents_VTNodeId",
                table: "VTComponents",
                column: "VTNodeId");

            migrationBuilder.CreateIndex(
                name: "IX_VTNodeComponents_VTComponentId",
                table: "VTNodeComponents",
                column: "VTComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_VTNodes_SelectedComponentId",
                table: "VTNodes",
                column: "SelectedComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_VTNodes_VTSystemId",
                table: "VTNodes",
                column: "VTSystemId");

            migrationBuilder.AddForeignKey(
                name: "FK_VTComponents_VTNodes_VTNodeId",
                table: "VTComponents",
                column: "VTNodeId",
                principalTable: "VTNodes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VTComponents_VTNodes_VTNodeId",
                table: "VTComponents");

            migrationBuilder.DropTable(
                name: "VTNodeComponents");

            migrationBuilder.DropTable(
                name: "VTNodes");

            migrationBuilder.DropTable(
                name: "VTComponents");

            migrationBuilder.DropTable(
                name: "VTSystems");
        }
    }
}
