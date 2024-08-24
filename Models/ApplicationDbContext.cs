using Microsoft.EntityFrameworkCore;
using HarmonyHotles.Models;

namespace HarmonyHotles.Data // تأكد أن `namespace` يتماشى مع بنية مشروعك
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Roomtype> Roomtypes { get; set; }
        public DbSet<Image> Images { get; set; }
        // أضف باقي الـ DbSet للكيانات الأخرى هنا
    }
}
