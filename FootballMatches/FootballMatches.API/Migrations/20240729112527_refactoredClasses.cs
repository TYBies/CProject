using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballMatches.API.Migrations
{
    /// <inheritdoc />
    public partial class refactoredClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitions_Seasons_SeasonId",
                table: "Competitions");

            migrationBuilder.DropIndex(
                name: "IX_Competitions_SeasonId",
                table: "Competitions");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Seasons");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Seasons");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "SeasonId",
                table: "Competitions");

            migrationBuilder.AddColumn<string>(
                name: "CompetitionId",
                table: "Seasons",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "DLProviderId",
                table: "Matches",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Seasons_CompetitionId",
                table: "Seasons",
                column: "CompetitionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Seasons_Competitions_CompetitionId",
                table: "Seasons",
                column: "CompetitionId",
                principalTable: "Competitions",
                principalColumn: "CompetitionId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seasons_Competitions_CompetitionId",
                table: "Seasons");

            migrationBuilder.DropIndex(
                name: "IX_Seasons_CompetitionId",
                table: "Seasons");

            migrationBuilder.DropColumn(
                name: "CompetitionId",
                table: "Seasons");

            migrationBuilder.DropColumn(
                name: "DLProviderId",
                table: "Matches");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Seasons",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Seasons",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Matches",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Matches",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SeasonId",
                table: "Competitions",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_SeasonId",
                table: "Competitions",
                column: "SeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Competitions_Seasons_SeasonId",
                table: "Competitions",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "SeasonId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
