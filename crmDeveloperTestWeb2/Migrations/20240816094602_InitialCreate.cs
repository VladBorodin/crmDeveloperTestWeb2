using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace crmDeveloperTestWeb2.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalendarDays",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Weekday = table.Column<string>(type: "TEXT", nullable: false),
                    DayType = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarDays", x => x.Date);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalendarDays");
        }
    }
}
