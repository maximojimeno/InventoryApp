using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InventaryApp.Server.Migrations
{
    public partial class OpenInventary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "openInventaries",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OpenDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CloseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BussinessId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StatusInventary = table.Column<bool>(type: "bit", nullable: false),
                    OldAmountInventary = table.Column<double>(type: "float", nullable: false),
                    ActualAmountInventary = table.Column<double>(type: "float", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_openInventaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_openInventaries_Bussiness_BussinessId",
                        column: x => x.BussinessId,
                        principalTable: "Bussiness",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_openInventaries_BussinessId",
                table: "openInventaries",
                column: "BussinessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "openInventaries");
        }
    }
}
