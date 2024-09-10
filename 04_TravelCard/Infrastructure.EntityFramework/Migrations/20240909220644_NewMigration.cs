using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    travel_ID = table.Column<int>(type: "integer", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    DateRegistration = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_ID);
                });

            migrationBuilder.CreateTable(
                name: "Travels",
                columns: table => new
                {
                    travel_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    user_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    start_point = table.Column<string>(type: "text", nullable: false),
                    finish_point = table.Column<string>(type: "text", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_private = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Travels", x => x.travel_ID);
                    table.ForeignKey(
                        name: "FK_Travels_Users_user_ID",
                        column: x => x.user_ID,
                        principalTable: "Users",
                        principalColumn: "user_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Travel_point",
                columns: table => new
                {
                    tp_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    point_map = table.Column<string>(type: "text", nullable: false),
                    point_desc = table.Column<string>(type: "text", nullable: false),
                    waiting_time = table.Column<double>(type: "double precision", nullable: false),
                    travel_ID = table.Column<int>(type: "integer", nullable: false),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Travel_point", x => x.tp_ID);
                    table.ForeignKey(
                        name: "FK_Travel_point_Travels_travel_ID",
                        column: x => x.travel_ID,
                        principalTable: "Travels",
                        principalColumn: "travel_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Travel_point_travel_ID",
                table: "Travel_point",
                column: "travel_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Travels_user_ID",
                table: "Travels",
                column: "user_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Travel_point");

            migrationBuilder.DropTable(
                name: "Travels");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
