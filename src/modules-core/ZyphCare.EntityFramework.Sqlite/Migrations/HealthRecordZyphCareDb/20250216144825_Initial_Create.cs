using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZyphCare.EntityFramework.Sqlite.Migrations.HealthRecordZyphCareDb
{
    /// <inheritdoc />
    public partial class Initial_Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HealthRecords",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    PatientId = table.Column<string>(type: "TEXT", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthRecords", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HealthRecord_CreatedDate",
                table: "HealthRecords",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_HealthRecord_FileName",
                table: "HealthRecords",
                column: "FileName");

            migrationBuilder.CreateIndex(
                name: "IX_HealthRecord_Type",
                table: "HealthRecords",
                column: "Type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HealthRecords");
        }
    }
}
