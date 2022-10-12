using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class CollabMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotesTable_UserTable_UserId",
                table: "NotesTable");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "NotesTable",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_NotesTable_UserId",
                table: "NotesTable",
                newName: "IX_NotesTable_UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_NotesTable_UserTable_UserID",
                table: "NotesTable",
                column: "UserID",
                principalTable: "UserTable",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotesTable_UserTable_UserID",
                table: "NotesTable");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "NotesTable",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_NotesTable_UserID",
                table: "NotesTable",
                newName: "IX_NotesTable_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotesTable_UserTable_UserId",
                table: "NotesTable",
                column: "UserId",
                principalTable: "UserTable",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
