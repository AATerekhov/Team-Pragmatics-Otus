using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddedDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User_testing",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Logo = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    Name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    Surname = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    Email = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_testing", x => x.UserID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_testing_Email",
                table: "User_testing",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_testing_Logo",
                table: "User_testing",
                column: "Logo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User_testing");
        }
    }
}
