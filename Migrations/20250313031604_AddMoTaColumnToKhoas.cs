using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyDaoTao.Migrations
{
    /// <inheritdoc />
    public partial class AddMoTaColumnToKhoas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "BaiGiangs",
                newName: "NoiDung");

            migrationBuilder.AddColumn<string>(
                name: "MoTa",
                table: "Khoas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoTa",
                table: "Khoas");

            migrationBuilder.RenameColumn(
                name: "NoiDung",
                table: "BaiGiangs",
                newName: "FilePath");
        }
    }
}
