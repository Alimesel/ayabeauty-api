using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AyaBeauty.API.Migrations
{
    /// <inheritdoc />
    public partial class AddAboutContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboutContent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhilosophyTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Paragraph1 = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Paragraph2 = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Paragraph3 = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ImageFileName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutContent", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutContent");
        }
    }
}
