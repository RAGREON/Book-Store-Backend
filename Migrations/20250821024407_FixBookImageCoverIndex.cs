using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _.Migrations
{
    /// <inheritdoc />
    public partial class FixBookImageCoverIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BookImage_BookId_Type",
                table: "BookImage");

            migrationBuilder.CreateIndex(
                name: "IX_BookImage_BookId_Type",
                table: "BookImage",
                columns: new[] { "BookId", "Type" },
                unique: true,
                filter: "\"Type\" = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BookImage_BookId_Type",
                table: "BookImage");

            migrationBuilder.CreateIndex(
                name: "IX_BookImage_BookId_Type",
                table: "BookImage",
                columns: new[] { "BookId", "Type" },
                unique: true,
                filter: "[Type] = 0");
        }
    }
}
