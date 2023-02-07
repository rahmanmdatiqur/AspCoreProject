using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Auth_Cls.Migrations
{
    public partial class Check : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diseses",
                columns: table => new
                {
                    DiseseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiseseName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diseses", x => x.DiseseId);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientName = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Phone = table.Column<int>(nullable: false),
                    Picture = table.Column<string>(nullable: true),
                    MaritialStatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientId);
                });

            migrationBuilder.CreateTable(
                name: "TestEntries",
                columns: table => new
                {
                    TestEntryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(nullable: false),
                    DiseseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestEntries", x => x.TestEntryId);
                    table.ForeignKey(
                        name: "FK_TestEntries_Diseses_DiseseId",
                        column: x => x.DiseseId,
                        principalTable: "Diseses",
                        principalColumn: "DiseseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestEntries_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestEntries_DiseseId",
                table: "TestEntries",
                column: "DiseseId");

            migrationBuilder.CreateIndex(
                name: "IX_TestEntries_PatientId",
                table: "TestEntries",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestEntries");

            migrationBuilder.DropTable(
                name: "Diseses");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
