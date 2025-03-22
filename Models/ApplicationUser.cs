using Microsoft.AspNetCore.Identity;

namespace QuanLyDaoTaoWeb.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsApproved { get; set; } // Trạng thái xác thực (true = đã phê duyệt, false = chưa phê duyệt)
    }
}