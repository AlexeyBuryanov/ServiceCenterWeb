using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceCenterWeb.Models;
using ServiceCenterWeb.Models.Database;

namespace ServiceCenterWeb.Controllers
{
    public class PersonalCabinetController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment _appEnv;

        public PersonalCabinetController(UserManager<User> userManager, ApplicationDbContext db, 
                                         IHostingEnvironment appEnv)
        {
            _userManager = userManager;
            _db = db;
            _appEnv = appEnv;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var currentClient = new Client();
            var currentMaster = new Master();

            if (User.IsInRole("Клиент")) {
                currentClient = await _db.Clients.FirstAsync(c => c.Guid == currentUser.Id);
            } else if (User.IsInRole("Мастер")) {
                currentMaster = await _db.Masters.FirstAsync(m => m.Guid == currentUser.Id);
            } // if
            
            var viewModel = new PersonalCabinetViewModel {
                CurrentClient = currentClient,
                CurrentUser = currentUser,
                CurrentMaster = currentMaster
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeAvatar(IFormFile uploadedFile)
        {
            if (uploadedFile != null) {
                var currentUser = await _userManager.GetUserAsync(User);
                var path = $"/images/avatars/{currentUser.Id}{Path.GetExtension(uploadedFile.FileName).ToLower()}";

                using (var fileStream = new FileStream($"{_appEnv.WebRootPath}{path}", FileMode.Create)) {
                    await uploadedFile.CopyToAsync(fileStream);
                } // using

                await SaveUserAvatarPath(path);
            } // if

            return RedirectToAction("Index");
        }

        private async Task SaveUserAvatarPath(string path)
        {
            await Task.Run(async () => {
                var currentUser = await _userManager.GetUserAsync(User);

                if (currentUser != null) {
                    // Удаляем старый аватар, если это не дефолтный вариант
                    if (!currentUser.AvatarPath.Contains("default.jpg")) {
                        System.IO.File.Delete($"{_appEnv.WebRootPath}{currentUser.AvatarPath}");
                    } // if

                    // Меняем на новый
                    currentUser.AvatarPath = path;
                    
                    await _userManager.UpdateAsync(currentUser);
                } // if
            });
        }
    }
}