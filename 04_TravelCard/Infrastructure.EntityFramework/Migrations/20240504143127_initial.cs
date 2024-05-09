using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Travels",
                columns: table => new
                {
                    travel_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    travel_desc = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Travels", x => x.travel_ID);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    user_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    travel_ID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.user_ID);
                    table.ForeignKey(
                        name: "FK_Managers_Travels_travel_ID",
                        column: x => x.travel_ID,
                        principalTable: "Travels",
                        principalColumn: "travel_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Travel_point",
                columns: table => new
                {
                    tp_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    point_map = table.Column<string>(type: "character varying(90)", maxLength: 90, nullable: false),
                    point_desc = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    waiting_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    travel_ID = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    travel_ID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_ID);
                    table.ForeignKey(
                        name: "FK_Users_Travels_travel_ID",
                        column: x => x.travel_ID,
                        principalTable: "Travels",
                        principalColumn: "travel_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Managers_travel_ID",
                table: "Managers",
                column: "travel_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Travel_point_travel_ID",
                table: "Travel_point",
                column: "travel_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_travel_ID",
                table: "Users",
                column: "travel_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "Travel_point");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Travels");
        }
    }
}
