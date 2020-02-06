using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeByteForum.Migrations
{
    public partial class Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvatarModel_AspNetUsers_OwnerId",
                table: "AvatarModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AvatarModel",
                table: "AvatarModel");

            migrationBuilder.RenameTable(
                name: "AvatarModel",
                newName: "Avatars");

            migrationBuilder.RenameIndex(
                name: "IX_AvatarModel_OwnerId",
                table: "Avatars",
                newName: "IX_Avatars_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Avatars",
                table: "Avatars",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Avatars_AspNetUsers_OwnerId",
                table: "Avatars",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avatars_AspNetUsers_OwnerId",
                table: "Avatars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Avatars",
                table: "Avatars");

            migrationBuilder.RenameTable(
                name: "Avatars",
                newName: "AvatarModel");

            migrationBuilder.RenameIndex(
                name: "IX_Avatars_OwnerId",
                table: "AvatarModel",
                newName: "IX_AvatarModel_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AvatarModel",
                table: "AvatarModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AvatarModel_AspNetUsers_OwnerId",
                table: "AvatarModel",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
