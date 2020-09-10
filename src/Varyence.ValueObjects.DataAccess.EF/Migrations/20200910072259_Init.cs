using Microsoft.EntityFrameworkCore.Migrations;

namespace Varyence.ValueObjects.DataAccess.EF.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Dbo");

            migrationBuilder.CreateTable(
                name: "Suffix",
                schema: "Dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suffix", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                schema: "Dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    NameSuffixId = table.Column<int>(nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    GithubAccountUri = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Person_Suffix_NameSuffixId",
                        column: x => x.NameSuffixId,
                        principalSchema: "Dbo",
                        principalTable: "Suffix",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Person_NameSuffixId",
                schema: "Dbo",
                table: "Person",
                column: "NameSuffixId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Person",
                schema: "Dbo");

            migrationBuilder.DropTable(
                name: "Suffix",
                schema: "Dbo");
        }
    }
}
