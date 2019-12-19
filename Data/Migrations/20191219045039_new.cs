using Microsoft.EntityFrameworkCore.Migrations;

namespace mvc.Data.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    FavoriteId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FavoriteCategory = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.FavoriteId);
                });

            migrationBuilder.CreateTable(
                name: "River",
                columns: table => new
                {
                    RiverId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RiverName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Class = table.Column<string>(nullable: true),
                    RunLength = table.Column<string>(nullable: true),
                    PutIn = table.Column<string>(nullable: true),
                    TakeOut = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    StationNumber = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_River", x => x.RiverId);
                    table.ForeignKey(
                        name: "FK_River_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteRiver",
                columns: table => new
                {
                    FavoriteRiverId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RiverId = table.Column<int>(nullable: false),
                    FavoriteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteRiver", x => x.FavoriteRiverId);
                    table.ForeignKey(
                        name: "FK_FavoriteRiver_Favorites_FavoriteId",
                        column: x => x.FavoriteId,
                        principalTable: "Favorites",
                        principalColumn: "FavoriteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteRiver_River_RiverId",
                        column: x => x.RiverId,
                        principalTable: "River",
                        principalColumn: "RiverId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteRiver_FavoriteId",
                table: "FavoriteRiver",
                column: "FavoriteId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteRiver_RiverId",
                table: "FavoriteRiver",
                column: "RiverId");

            migrationBuilder.CreateIndex(
                name: "IX_River_UserId",
                table: "River",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoriteRiver");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "River");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
