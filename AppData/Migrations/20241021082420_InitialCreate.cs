using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppData.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeThis",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenDeThi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MonHoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayThi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianLamBai = table.Column<int>(type: "int", nullable: false),
                    SoLuongCauHoi = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeThis", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeThis");
        }
    }
}
