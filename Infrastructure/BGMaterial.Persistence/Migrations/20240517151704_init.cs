using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BGMaterial.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Engine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ListPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockMrk = table.Column<int>(type: "int", nullable: false),
                    StockLzm = table.Column<int>(type: "int", nullable: false),
                    StockAnk = table.Column<int>(type: "int", nullable: false),
                    StockAdn = table.Column<int>(type: "int", nullable: false),
                    StockErz = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Materials");
        }
    }
}
