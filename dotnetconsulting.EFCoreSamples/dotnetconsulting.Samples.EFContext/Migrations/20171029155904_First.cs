using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace dotnetconsulting.Samples.EFContext.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dnc");

            migrationBuilder.CreateTable(
                name: "Speakers",
                schema: "dnc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Homepage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Infos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speakers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VenueSetup",
                schema: "dnc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenueSetup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechEvents",
                schema: "dnc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Begin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Venue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VenueSetupId = table.Column<int>(type: "int", nullable: true),
                    WebSite = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechEvents_VenueSetup_VenueSetupId",
                        column: x => x.VenueSetupId,
                        principalSchema: "dnc",
                        principalTable: "VenueSetup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                schema: "dnc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContentDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Begin = table.Column<DateTime>(type: "time", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    Difficulty = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: true),
                    SpeakerId = table.Column<int>(type: "int", nullable: false),
                    TechEventId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: "<New Session>")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_TechEvents_TechEventId",
                        column: x => x.TechEventId,
                        principalSchema: "dnc",
                        principalTable: "TechEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpeakerSessions",
                schema: "dnc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    SpeakerId = table.Column<int>(type: "int", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeakerSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpeakerSessions_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalSchema: "dnc",
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpeakerSessions_Speakers_SpeakerId",
                        column: x => x.SpeakerId,
                        principalSchema: "dnc",
                        principalTable: "Speakers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_TechEventId",
                schema: "dnc",
                table: "Sessions",
                column: "TechEventId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_Title",
                schema: "dnc",
                table: "Sessions",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_SpeakerSessions_SessionId",
                schema: "dnc",
                table: "SpeakerSessions",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_SpeakerSessions_SpeakerId",
                schema: "dnc",
                table: "SpeakerSessions",
                column: "SpeakerId");

            migrationBuilder.CreateIndex(
                name: "IX_TechEvents_VenueSetupId",
                schema: "dnc",
                table: "TechEvents",
                column: "VenueSetupId",
                unique: true,
                filter: "[VenueSetupId] IS NOT NULL");

            migrationBuilder.ApplyCustomUp();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpeakerSessions",
                schema: "dnc");

            migrationBuilder.DropTable(
                name: "Sessions",
                schema: "dnc");

            migrationBuilder.DropTable(
                name: "Speakers",
                schema: "dnc");

            migrationBuilder.DropTable(
                name: "TechEvents",
                schema: "dnc");

            migrationBuilder.DropTable(
                name: "VenueSetup",
                schema: "dnc");

            migrationBuilder.ApplyCustomDown();
        }
    }
}
