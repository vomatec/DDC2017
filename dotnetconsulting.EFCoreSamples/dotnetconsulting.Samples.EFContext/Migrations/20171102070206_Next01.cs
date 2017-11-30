using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace dotnetconsulting.Samples.EFContext.Migrations
{
    public partial class Next01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                schema: "dnc",
                table: "VenueSetup",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "dnc",
                table: "VenueSetup",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                schema: "dnc",
                table: "VenueSetup",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                schema: "dnc",
                table: "TechEvents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "dnc",
                table: "TechEvents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                schema: "dnc",
                table: "TechEvents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                schema: "dnc",
                table: "SpeakerSessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "dnc",
                table: "SpeakerSessions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                schema: "dnc",
                table: "SpeakerSessions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "dnc",
                table: "Speakers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                schema: "dnc",
                table: "Speakers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "dnc",
                table: "Sessions",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "dnc",
                table: "Sessions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                schema: "dnc",
                table: "Sessions",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                schema: "dnc",
                table: "VenueSetup");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "dnc",
                table: "VenueSetup");

            migrationBuilder.DropColumn(
                name: "Updated",
                schema: "dnc",
                table: "VenueSetup");

            migrationBuilder.DropColumn(
                name: "Created",
                schema: "dnc",
                table: "TechEvents");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "dnc",
                table: "TechEvents");

            migrationBuilder.DropColumn(
                name: "Updated",
                schema: "dnc",
                table: "TechEvents");

            migrationBuilder.DropColumn(
                name: "Created",
                schema: "dnc",
                table: "SpeakerSessions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "dnc",
                table: "SpeakerSessions");

            migrationBuilder.DropColumn(
                name: "Updated",
                schema: "dnc",
                table: "SpeakerSessions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "dnc",
                table: "Speakers");

            migrationBuilder.DropColumn(
                name: "Updated",
                schema: "dnc",
                table: "Speakers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "dnc",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "Updated",
                schema: "dnc",
                table: "Sessions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "dnc",
                table: "Sessions",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");
        }
    }
}
