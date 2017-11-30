using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace dotnetconsulting.Samples.EFContext.Migrations
{
    public partial class Defaults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Infos",
                schema: "dnc",
                table: "Speakers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "(Keine Infos)",
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                schema: "dnc",
                table: "Speakers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "dnc",
                table: "Sessions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldDefaultValue: "<New Session>");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "dnc",
                table: "Sessions",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                schema: "dnc",
                table: "Speakers");

            migrationBuilder.AlterColumn<string>(
                name: "Infos",
                schema: "dnc",
                table: "Speakers",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "(Keine Infos)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "dnc",
                table: "Sessions",
                maxLength: 200,
                nullable: false,
                defaultValue: "<New Session>",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "dnc",
                table: "Sessions",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
