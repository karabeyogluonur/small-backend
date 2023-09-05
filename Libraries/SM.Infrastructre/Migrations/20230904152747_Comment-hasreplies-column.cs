using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.Infrastructre.Migrations
{
    /// <inheritdoc />
    public partial class Commenthasrepliescolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasReplies",
                table: "Comment",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasReplies",
                table: "Comment");
        }
    }
}
