﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyDaoTao.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Tạo các bảng của ASP.NET Core Identity (giữ nguyên)
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            // 2. Tạo bảng Khoa
            migrationBuilder.CreateTable(
                name: "Khoa",
                columns: table => new
                {
                    MaKhoa = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenKhoa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Khoa", x => x.MaKhoa);
                });

            // 3. Tạo các bảng Identity liên quan (giữ nguyên)
            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // 4. Tạo bảng ChuongTrinhDaoTao
            migrationBuilder.CreateTable(
                name: "ChuongTrinhDaoTao",
                columns: table => new
                {
                    MaCTDT = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenCTDT = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NamBatDau = table.Column<int>(type: "int", nullable: false),
                    MaKhoa = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChuongTrinhDaoTao", x => x.MaCTDT);
                    table.ForeignKey(
                        name: "FK_ChuongTrinhDaoTao_Khoa_MaKhoa",
                        column: x => x.MaKhoa,
                        principalTable: "Khoa",
                        principalColumn: "MaKhoa",
                        onDelete: ReferentialAction.SetNull);
                });

            // 5. Tạo bảng GiangVien
            migrationBuilder.CreateTable(
                name: "GiangVien",
                columns: table => new
                {
                    MaGV = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaKhoa = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    NgayNhanViec = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiangVien", x => x.MaGV);
                    table.ForeignKey(
                        name: "FK_GiangVien_Khoa_MaKhoa",
                        column: x => x.MaKhoa,
                        principalTable: "Khoa",
                        principalColumn: "MaKhoa",
                        onDelete: ReferentialAction.SetNull);
                });

            // 6. Tạo bảng MonHoc
            migrationBuilder.CreateTable(
                name: "MonHoc",
                columns: table => new
                {
                    MaMH = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenMH = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoTinChi = table.Column<int>(type: "int", nullable: false),
                    MaKhoa = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonHoc", x => x.MaMH);
                    table.ForeignKey(
                        name: "FK_MonHoc_Khoa_MaKhoa",
                        column: x => x.MaKhoa,
                        principalTable: "Khoa",
                        principalColumn: "MaKhoa",
                        onDelete: ReferentialAction.SetNull);
                });

            // 7. Tạo bảng SinhVien
            migrationBuilder.CreateTable(
                name: "SinhVien",
                columns: table => new
                {
                    MaSV = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaKhoa = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhVien", x => x.MaSV);
                    table.ForeignKey(
                        name: "FK_SinhVien_Khoa_MaKhoa",
                        column: x => x.MaKhoa,
                        principalTable: "Khoa",
                        principalColumn: "MaKhoa",
                        onDelete: ReferentialAction.SetNull);
                });

            // 8. Tạo bảng LopHoc
            migrationBuilder.CreateTable(
                name: "LopHoc",
                columns: table => new
                {
                    MaLop = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenLop = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaCTDT = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LopHoc", x => x.MaLop);
                    table.ForeignKey(
                        name: "FK_LopHoc_ChuongTrinhDaoTao_MaCTDT",
                        column: x => x.MaCTDT,
                        principalTable: "ChuongTrinhDaoTao",
                        principalColumn: "MaCTDT",
                        onDelete: ReferentialAction.Cascade);
                });

            // 9. Tạo bảng BaiGiang
            migrationBuilder.CreateTable(
                name: "BaiGiang",
                columns: table => new
                {
                    MaBG = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaMH = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaiGiang", x => x.MaBG);
                    table.ForeignKey(
                        name: "FK_BaiGiang_MonHoc_MaMH",
                        column: x => x.MaMH,
                        principalTable: "MonHoc",
                        principalColumn: "MaMH",
                        onDelete: ReferentialAction.Cascade);
                });

            // 10. Tạo bảng DeCuong
            migrationBuilder.CreateTable(
                name: "DeCuong",
                columns: table => new
                {
                    MaDC = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaMH = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MucTieu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeCuong", x => x.MaDC);
                    table.ForeignKey(
                        name: "FK_DeCuong_MonHoc_MaMH",
                        column: x => x.MaMH,
                        principalTable: "MonHoc",
                        principalColumn: "MaMH",
                        onDelete: ReferentialAction.Cascade);
                });

            // 11. Tạo bảng DanhGia
            migrationBuilder.CreateTable(
                name: "DanhGia",
                columns: table => new
                {
                    MaDG = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaSV = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaMH = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DiemDanhGia = table.Column<int>(type: "int", nullable: false),
                    NhanXet = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NgayDanhGia = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhGia", x => x.MaDG);
                    table.ForeignKey(
                        name: "FK_DanhGia_MonHoc_MaMH",
                        column: x => x.MaMH,
                        principalTable: "MonHoc",
                        principalColumn: "MaMH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DanhGia_SinhVien_MaSV",
                        column: x => x.MaSV,
                        principalTable: "SinhVien",
                        principalColumn: "MaSV",
                        onDelete: ReferentialAction.Cascade);
                });

            // 12. Tạo bảng DangKyLopHoc
            migrationBuilder.CreateTable(
                name: "DangKyLopHoc",
                columns: table => new
                {
                    MaSV = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaLopHoc = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NgayDangKy = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DangKyLopHoc", x => new { x.MaSV, x.MaLopHoc });
                    table.ForeignKey(
                        name: "FK_DangKyLopHoc_LopHoc_MaLopHoc",
                        column: x => x.MaLopHoc,
                        principalTable: "LopHoc",
                        principalColumn: "MaLop",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DangKyLopHoc_SinhVien_MaSV",
                        column: x => x.MaSV,
                        principalTable: "SinhVien",
                        principalColumn: "MaSV",
                        onDelete: ReferentialAction.Cascade);
                });

            // 13. Tạo bảng PhanCongGiangDay
            migrationBuilder.CreateTable(
                name: "PhanCongGiangDay",
                columns: table => new
                {
                    MaPCGD = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaGV = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    MaMH = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaLop = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    HocKy = table.Column<int>(type: "int", nullable: false),
                    NamHoc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanCongGiangDay", x => x.MaPCGD);
                    table.ForeignKey(
                        name: "FK_PhanCongGiangDay_GiangVien_MaGV",
                        column: x => x.MaGV,
                        principalTable: "GiangVien",
                        principalColumn: "MaGV",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PhanCongGiangDay_LopHoc_MaLop",
                        column: x => x.MaLop,
                        principalTable: "LopHoc",
                        principalColumn: "MaLop",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhanCongGiangDay_MonHoc_MaMH",
                        column: x => x.MaMH,
                        principalTable: "MonHoc",
                        principalColumn: "MaMH",
                        onDelete: ReferentialAction.Cascade);
                });

            // 14. Tạo bảng TaiLieu
            migrationBuilder.CreateTable(
                name: "TaiLieu",
                columns: table => new
                {
                    MaTL = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaBG = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenTL = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DuongDan = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiLieu", x => x.MaTL);
                    table.ForeignKey(
                        name: "FK_TaiLieu_BaiGiang_MaBG",
                        column: x => x.MaBG,
                        principalTable: "BaiGiang",
                        principalColumn: "MaBG",
                        onDelete: ReferentialAction.Cascade);
                });

            // 15. Tạo các index
            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BaiGiang_MaMH",
                table: "BaiGiang",
                column: "MaMH");

            migrationBuilder.CreateIndex(
                name: "IX_ChuongTrinhDaoTao_MaKhoa",
                table: "ChuongTrinhDaoTao",
                column: "MaKhoa");

            migrationBuilder.CreateIndex(
                name: "IX_DangKyLopHoc_MaLopHoc",
                table: "DangKyLopHoc",
                column: "MaLopHoc");

            migrationBuilder.CreateIndex(
                name: "IX_DangKyLopHoc_MaSV",
                table: "DangKyLopHoc",
                column: "MaSV");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGia_MaMH",
                table: "DanhGia",
                column: "MaMH");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGia_MaSV",
                table: "DanhGia",
                column: "MaSV");

            migrationBuilder.CreateIndex(
                name: "IX_DeCuong_MaMH",
                table: "DeCuong",
                column: "MaMH");

            migrationBuilder.CreateIndex(
                name: "IX_GiangVien_MaKhoa",
                table: "GiangVien",
                column: "MaKhoa");

            migrationBuilder.CreateIndex(
                name: "IX_LopHoc_MaCTDT",
                table: "LopHoc",
                column: "MaCTDT");

            migrationBuilder.CreateIndex(
                name: "IX_MonHoc_MaKhoa",
                table: "MonHoc",
                column: "MaKhoa");

            migrationBuilder.CreateIndex(
                name: "IX_PhanCongGiangDay_MaGV",
                table: "PhanCongGiangDay",
                column: "MaGV");

            migrationBuilder.CreateIndex(
                name: "IX_PhanCongGiangDay_MaLop",
                table: "PhanCongGiangDay",
                column: "MaLop");

            migrationBuilder.CreateIndex(
                name: "IX_PhanCongGiangDay_MaMH",
                table: "PhanCongGiangDay",
                column: "MaMH");

            migrationBuilder.CreateIndex(
                name: "IX_SinhVien_MaKhoa",
                table: "SinhVien",
                column: "MaKhoa");

            migrationBuilder.CreateIndex(
                name: "IX_TaiLieu_MaBG",
                table: "TaiLieu",
                column: "MaBG");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "DangKyLopHoc");

            migrationBuilder.DropTable(
                name: "DanhGia");

            migrationBuilder.DropTable(
                name: "DeCuong");

            migrationBuilder.DropTable(
                name: "PhanCongGiangDay");

            migrationBuilder.DropTable(
                name: "TaiLieu");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "SinhVien");

            migrationBuilder.DropTable(
                name: "GiangVien");

            migrationBuilder.DropTable(
                name: "LopHoc");

            migrationBuilder.DropTable(
                name: "BaiGiang");

            migrationBuilder.DropTable(
                name: "ChuongTrinhDaoTao");

            migrationBuilder.DropTable(
                name: "MonHoc");

            migrationBuilder.DropTable(
                name: "Khoa");
        }
    }
}