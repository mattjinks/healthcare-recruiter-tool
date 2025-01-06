using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecruiterWorkflow.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePositionsForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Candidates_CandidateId",
                table: "Positions");

            migrationBuilder.AlterColumn<int>(
                name: "CandidateId",
                table: "Positions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "Positions",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 1,
                column: "JobId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 2,
                column: "JobId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 3,
                column: "JobId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 4,
                column: "JobId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 5,
                column: "JobId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 6,
                column: "JobId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 7,
                column: "JobId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Positions_JobId",
                table: "Positions",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Candidates_CandidateId",
                table: "Positions",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Jobs_JobId",
                table: "Positions",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Candidates_CandidateId",
                table: "Positions");

            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Jobs_JobId",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Positions_JobId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Positions");

            migrationBuilder.AlterColumn<int>(
                name: "CandidateId",
                table: "Positions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
