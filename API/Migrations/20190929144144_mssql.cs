using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class mssql : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ppg",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    time = table.Column<DateTime>(nullable: false),
                    heartRate = table.Column<int>(nullable: false),
                    hrConfidence = table.Column<float>(nullable: true),
                    greenCount1 = table.Column<int>(nullable: true),
                    greenCount2 = table.Column<int>(nullable: true),
                    xAccel = table.Column<float>(nullable: true),
                    yAccel = table.Column<float>(nullable: true),
                    zAccel = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ppg", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "temp",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    temp = table.Column<float>(nullable: false),
                    time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_temp", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ppg");

            migrationBuilder.DropTable(
                name: "temp");
        }
    }
}
