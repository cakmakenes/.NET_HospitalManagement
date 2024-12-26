using BLL.Controllers.Bases;
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Experimental.ProjectCache;

namespace MVC.Controllers
{
    [Authorize]
    public class FavoritesController : MvcController
    {
        const string SESSIONKEY = "Favorites";

        private readonly HttpServiceBase _httpService;
        private readonly IService<Doctor,DoctorModel> _doctorService;

        public FavoritesController(HttpServiceBase httpServiceBase, IService<Doctor,DoctorModel> doctorService)
        {
            _httpService = httpServiceBase;
            _doctorService = doctorService;
        }

        private int GetUserId() => Convert.ToInt32(User.Claims.SingleOrDefault(c=>c.Type == "Id").Value);
        private List<FavoritesModel> GetSession(int userId)
        {
       
            var favorites = _httpService.GetSession<List<FavoritesModel>>(SESSIONKEY);
            return favorites?.Where(f => f.UserId == userId).ToList();
        }
        public IActionResult Get()
        {
            return View("List",GetSession(GetUserId()));
        }

        public IActionResult Remove(int DoctorId)
        {
            var favorites = GetSession(GetUserId());
            var favoritesItem = favorites.FirstOrDefault(c => c.DoctorId == DoctorId);
            favorites.Remove(favoritesItem);
            _httpService.SetSession(SESSIONKEY, favorites);
            return RedirectToAction(nameof(Get));
        }

        // GET: /Favorites/Add?DoctorId=17
        public IActionResult Add(int DoctorId)
        {
            int userId = GetUserId();
            var favorites = GetSession(userId);
            favorites = favorites ?? new List<FavoritesModel>();
    
            if (!favorites.Any(f => f.DoctorId == DoctorId))
            {
                var doctor = _doctorService.Query().SingleOrDefault(p => p.Record.Id == DoctorId);
                var favoritesItem = new FavoritesModel()
                {
                    DoctorId = DoctorId,
                    UserId = userId,
                    DoctorName = doctor.Name
                };
                favorites.Add(favoritesItem);
                _httpService.SetSession(SESSIONKEY, favorites);
                TempData["Message"] = $"\"{doctor.Name}\" added to favorites.";
            }
            return RedirectToAction("Index", "Doctors");
        }

    }
}
