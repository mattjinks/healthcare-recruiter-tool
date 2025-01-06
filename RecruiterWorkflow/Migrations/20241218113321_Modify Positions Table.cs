using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecruiterWorkflow.Migrations
{
    /// <inheritdoc />
    public partial class ModifyPositionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullTime",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "InOffice",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "PartTime",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "Permanent",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "Remote",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "Temporary",
                table: "Positions");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Positions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Type",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CandidateId", "Type" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "CandidateId", "Type" },
                values: new object[,]
                {
                    { 3, 1, 2 },
                    { 4, 1, 3 },
                    { 5, 2, 0 },
                    { 6, 2, 2 },
                    { 7, 2, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Positions");

            migrationBuilder.AddColumn<bool>(
                name: "FullTime",
                table: "Positions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "InOffice",
                table: "Positions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PartTime",
                table: "Positions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Permanent",
                table: "Positions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Remote",
                table: "Positions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Temporary",
                table: "Positions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FullTime", "InOffice", "PartTime", "Permanent", "Remote", "Temporary" },
                values: new object[] { true, false, true, true, false, true });

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CandidateId", "FullTime", "InOffice", "PartTime", "Permanent", "Remote", "Temporary" },
                values: new object[] { 2, true, false, false, true, false, true });
        }
    }
}
