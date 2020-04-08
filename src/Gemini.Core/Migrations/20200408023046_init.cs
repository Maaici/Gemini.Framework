using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gemini.Core.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mobile = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    Password = table.Column<string>(maxLength: 50, nullable: true),
                    RealName = table.Column<string>(maxLength: 50, nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    BirthDay = table.Column<DateTime>(maxLength: 10, nullable: true),
                    Education = table.Column<string>(maxLength: 20, nullable: true),
                    SchoolName = table.Column<string>(maxLength: 100, nullable: true),
                    RegisterTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
