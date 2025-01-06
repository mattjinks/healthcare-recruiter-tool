using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecruiterWorkflow.Migrations
{
    /// <inheritdoc />
    public partial class AddPositionData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Candidates_CandidateId",
                table: "Positions");

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "CandidateId", "FullTime", "InOffice", "PartTime", "Permanent", "Remote", "Temporary" },
                values: new object[,]
                {
                    { 1, 1, true, false, true, true, false, true },
                    { 2, 2, true, false, false, true, false, true }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Candidates_CandidateId",
                table: "Positions",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Candidates_CandidateId",
                table: "Positions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Positions",
                table: "Positions");

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameTable(
                name: "Positions",
                newName: "Positions");

            migrationBuilder.RenameIndex(
                name: "IX_Positions_CandidateId",
                table: "Positions",
                newName: "IX_Positions_CandidateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Positions",
                table: "Positions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Candidates_CandidateId",
                table: "Positions",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
