using Microsoft.EntityFrameworkCore.Migrations;

namespace InventaryApp.Server.Migrations
{
    public partial class FixAccountEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Bussiness_BussinnessId",
                table: "Account");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                table: "Account");

            migrationBuilder.RenameTable(
                name: "Account",
                newName: "Accounts");

            migrationBuilder.RenameColumn(
                name: "BussinnessId",
                table: "Accounts",
                newName: "BussinessId");

            migrationBuilder.RenameIndex(
                name: "IX_Account_BussinnessId",
                table: "Accounts",
                newName: "IX_Accounts_BussinessId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Bussiness_BussinessId",
                table: "Accounts",
                column: "BussinessId",
                principalTable: "Bussiness",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Bussiness_BussinessId",
                table: "Accounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "Account");

            migrationBuilder.RenameColumn(
                name: "BussinessId",
                table: "Account",
                newName: "BussinnessId");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_BussinessId",
                table: "Account",
                newName: "IX_Account_BussinnessId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                table: "Account",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Bussiness_BussinnessId",
                table: "Account",
                column: "BussinnessId",
                principalTable: "Bussiness",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
