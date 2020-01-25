using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeByteForum.Migrations
{
    public partial class EditUserIdForAnswersAndPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_AspNetUsers_SenderId1",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_SenderId1",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_SenderId1",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Answer_SenderId1",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "SenderId1",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SenderId1",
                table: "Answer");

            migrationBuilder.AlterColumn<string>(
                name: "SenderId",
                table: "Posts",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "SenderId",
                table: "Answer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_SenderId",
                table: "Posts",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_SenderId",
                table: "Answer",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_AspNetUsers_SenderId",
                table: "Answer",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_SenderId",
                table: "Posts",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_AspNetUsers_SenderId",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_SenderId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_SenderId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Answer_SenderId",
                table: "Answer");

            migrationBuilder.AlterColumn<int>(
                name: "SenderId",
                table: "Posts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderId1",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SenderId",
                table: "Answer",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderId1",
                table: "Answer",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_SenderId1",
                table: "Posts",
                column: "SenderId1");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_SenderId1",
                table: "Answer",
                column: "SenderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_AspNetUsers_SenderId1",
                table: "Answer",
                column: "SenderId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_SenderId1",
                table: "Posts",
                column: "SenderId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
