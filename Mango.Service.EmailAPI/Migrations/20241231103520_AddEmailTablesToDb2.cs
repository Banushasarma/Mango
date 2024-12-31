using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mango.Service.EmailAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailTablesToDb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Enail",
                table: "EmailLoggers",
                newName: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "EmailLoggers",
                newName: "Enail");
        }
    }
}
