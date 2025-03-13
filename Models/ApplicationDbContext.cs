using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuanLyDaoTao.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace QuanLyDaoTao.Models
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        // Bỏ qua cảnh cáo
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-2C56F5J\\SQLEXPRESS02;Database=QuanLyDaoTao;Trusted_Connection=True;TrustServerCertificate=True;");
            }
            optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Khoa> Khoas { get; set; }
        public DbSet<DeCuong> DeCuongs { get; set; }
        public DbSet<BaiGiang> BaiGiangs { get; set; }

        public void SeedData()
        {
            if (!this.Khoas.Any())
            {
                var khoa1 = new Khoa { TenKhoa = "Lập trình C#", MoTa = "Khóa học cơ bản về lập trình C#" };
                var khoa2 = new Khoa { TenKhoa = "Cơ sở dữ liệu", MoTa = "Khóa học về quản lý cơ sở dữ liệu" };

                this.Khoas.AddRange(khoa1, khoa2);
                this.SaveChanges();

                var deCuong1 = new DeCuong { TenDeCuong = "Đề cương Lập trình C#", KhoaId = khoa1.Id };
                var deCuong2 = new DeCuong { TenDeCuong = "Đề cương Cơ sở dữ liệu", KhoaId = khoa2.Id };

                this.DeCuongs.AddRange(deCuong1, deCuong2);
                this.SaveChanges();

                this.BaiGiangs.AddRange(
                    new BaiGiang 
                    { 
                        TenBaiGiang = "Bài 1: Các giai đoạn trong quản trị thiết kế một CSDL", 
                        NoiDung = "Nội dung bài học về các giai đoạn thiết kế CSDL", 
                        DeCuongId = deCuong1.Id, 
                        FilePDF = "https://i.pinimg.com/736x/87/d5/2f/87d52f42221de0c43a6c67c133004fc6.jpg", 
                        VideoUrl = "https://youtu.be/6CQWklzrUZQ?si=tiISTrBwkpEomZWA" 
                    },
                    new BaiGiang 
                    { 
                        TenBaiGiang = "Bài 2: Dẫn nhập", 
                        NoiDung = "Nội dung bài học dẫn nhập", 
                        DeCuongId = deCuong1.Id, 
                        FilePDF = "https://i.pinimg.com/236x/58/24/87/582487c207e347fd9ffb80a590c9a6bb.jpg", 
                        VideoUrl = "https://youtu.be/Ds9ASJ4tM1I?si=qRT9evcIQ77uJIZk" 
                    },
                    new BaiGiang 
                    { 
                        TenBaiGiang = "Bài 3: Mức tiều chính trong công việc thiết kế có sự hỗ trợ", 
                        NoiDung = "Nội dung bài học về mức tiêu chính", 
                        DeCuongId = deCuong1.Id, 
                        FilePDF = "https://i.pinimg.com/236x/84/e2/04/84e204ffaae2ff5d5ee4fcf6106429b7.jpg", 
                        VideoUrl = "https://youtu.be/wtbRapiOBrI?si=bTvFALXMsVbt3SAe" 
                    },

                    // Bài giảng cho Đề cương Cơ sở dữ liệu
                    new BaiGiang 
                    { 
                        TenBaiGiang = "Bài 2: Nội dung cơ bản của các giai đoạn thiết kế một CSDL", 
                        NoiDung = "Nội dung bài học về các giai đoạn cơ bản", 
                        DeCuongId = deCuong2.Id, 
                        FilePDF = "/files/noi-dung-co-ban.pdf", 
                        VideoUrl = "https://youtu.be/6CQWklzrUZQ?si=tiISTrBwkpEomZWA" 
                    },
                    new BaiGiang 
                    { 
                        TenBaiGiang = "Bài 3: Mở hình quản hệ và các phụ thuộc dữ liệu", 
                        NoiDung = "Nội dung bài học về mở hình quản hệ", 
                        DeCuongId = deCuong2.Id, 
                        FilePDF = "/files/mo-hinh-quan-he.pdf", 
                        VideoUrl = "https://youtu.be/Ds9ASJ4tM1I?si=qRT9evcIQ77uJIZk" 
                    }
                );
                this.SaveChanges();
            }
        }
    }
}