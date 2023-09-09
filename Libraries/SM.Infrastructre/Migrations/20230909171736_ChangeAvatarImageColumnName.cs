using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.Infrastructre.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAvatarImageColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvatarImageName",
                table: "AspNetUsers",
                newName: "AvatarImagePath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvatarImagePath",
                table: "AspNetUsers",
                newName: "AvatarImageName");
        }
    }
}
