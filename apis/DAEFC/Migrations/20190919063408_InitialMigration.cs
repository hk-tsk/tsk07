using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAEFC.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblCourse",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConcurrencyStamp = table.Column<string>(maxLength: 300, nullable: false),
                    Name = table.Column<string>(maxLength: 300, nullable: true),
                    Title = table.Column<string>(maxLength: 300, nullable: false),
                    CategoryRowColumnsCount = table.Column<int>(nullable: false),
                    IsNew = table.Column<bool>(nullable: false, defaultValue: false),
                    Img = table.Column<string>(maxLength: 300, nullable: true),
                    IntroInfo = table.Column<string>(maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCourse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblCategory",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConcurrencyStamp = table.Column<string>(maxLength: 300, nullable: false),
                    Name = table.Column<string>(maxLength: 300, nullable: true),
                    Title = table.Column<string>(maxLength: 300, nullable: true),
                    Description = table.Column<string>(maxLength: 300, nullable: true),
                    ImagePosition = table.Column<int>(nullable: false),
                    ImageTheme = table.Column<int>(nullable: false),
                    ImageUrl = table.Column<string>(maxLength: 300, nullable: true),
                    CourseId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblCategory_tblCourse_CourseId",
                        column: x => x.CourseId,
                        principalTable: "tblCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblContent",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConcurrencyStamp = table.Column<string>(maxLength: 300, nullable: false),
                    ContentType = table.Column<string>(maxLength: 300, nullable: true),
                    ContentText = table.Column<string>(maxLength: 300, nullable: true),
                    CategoryId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblContent_tblCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "tblCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblCategory_CourseId",
                table: "tblCategory",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCategory_Name",
                table: "tblCategory",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tblContent_CategoryId",
                table: "tblContent",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCourse_Name",
                table: "tblCourse",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblContent");

            migrationBuilder.DropTable(
                name: "tblCategory");

            migrationBuilder.DropTable(
                name: "tblCourse");
        }
    }
}
