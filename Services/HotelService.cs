using HarmonyHotles.Models;
using HarmonyHotles.Data; // إذا كنت تستخدم Entity Framework Core
using HarmonyHotles.Services;

namespace HarmonyHotles.Services
{
    public class HotelService
    {
        private readonly ApplicationDbContext _context;

        public HotelService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddImagesToHotel(decimal hotelId, List<string> imagePaths)
        {
            // البحث عن الفندق باستخدام معرف الفندق
            var hotel = _context.Hotels.Find(hotelId);

            if (hotel != null)
            {
                foreach (var imagePath in imagePaths)
                {
                    var image = new Image
                    {
                        Imagepath = imagePath,
                        Hotelid = hotel.Hotelid
                    };

                    hotel.Images.Add(image);
                }

                _context.SaveChanges(); // حفظ التغييرات في قاعدة البيانات
            }
        }
    }
}
