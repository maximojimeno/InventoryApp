using Microsoft.EntityFrameworkCore.Migrations;

namespace InventaryApp.Server.Migrations
{
    public partial class OpenInventaryUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_openInventaries_Bussiness_BussinessId",
                table: "openInventaries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_openInventaries",
                table: "openInventaries");

            migrationBuilder.RenameTable(
                name: "openInventaries",
                newName: "OpenInventaries");

            migrationBuilder.RenameIndex(
                name: "IX_openInventaries_BussinessId",
                table: "OpenInventaries",
                newName: "IX_OpenInventaries_BussinessId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OpenInventaries",
                table: "OpenInventaries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OpenInventaries_Bussiness_BussinessId",
                table: "OpenInventaries",
                column: "BussinessId",
                principalTable: "Bussiness",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpenInventaries_Bussiness_BussinessId",
                table: "OpenInventaries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OpenInventaries",
                table: "OpenInventaries");

            migrationBuilder.RenameTable(
                name: "OpenInventaries",
                newName: "openInventaries");

            migrationBuilder.RenameIndex(
                name: "IX_OpenInventaries_BussinessId",
                table: "openInventaries",
                newName: "IX_openInventaries_BussinessId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_openInventaries",
                table: "openInventaries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_openInventaries_Bussiness_BussinessId",
                table: "openInventaries",
                column: "BussinessId",
                principalTable: "Bussiness",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
