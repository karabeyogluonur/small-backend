using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.Infrastructre.Migrations
{
    /// <inheritdoc />
    public partial class AvatarImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarImageName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarImageName",
                table: "AspNetUsers");
        }
    }
}
