using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceCenterWeb.Models;
using ServiceCenterWeb.Models.Database;

namespace ServiceCenterWeb.Controllers
{
    [Authorize(Roles = "Админ")]
    public class RolesController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public RolesController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name)) {
                var result = await _roleManager.CreateAsync(new IdentityRole(name));

                if (result.Succeeded) {
                    return RedirectToAction("Roles", "AdminPanel");
                } // if

                foreach (var error in result.Errors) {
                    ModelState.AddModelError(string.Empty, error.Description);
                } // foreach
            } // if
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role != null) {
                /*var result = */await _roleManager.DeleteAsync(role);
            } // if

            return RedirectToAction("Roles", "AdminPanel");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            // Получаем пользователя
            var user = await _userManager.FindByIdAsync(id);

            if (user != null) {
                // Получаем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                var model = new ChangeRoleViewModel {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };

                return View(model);
            } // if

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            // Получаем пользователя
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null) {
                // Получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                // Получаем все роли
                //var allRoles = _roleManager.Roles.ToList();
                // Получаем список ролей, которые были добавлены
                var addedRoles = roles.Except(userRoles);
                // Получаем роли, которые были удалены
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);
                var masterRole = addedRoles.FirstOrDefault(r => r == "Мастер");

                // Если была присвоена роль мастера - регистрируем пользователя в таблице мастеров.
                // Поле guid будет идентифицировать пользователя в Identity.
                if (!string.IsNullOrWhiteSpace(masterRole)) {
                    var newMaster = new Master {
                        Id = 0,
                        FirstName = "Имя",
                        LastName = "Фамилия",
                        Reputation = 0,
                        MobilePhone = "+380000000000", 
                        Guid = user.Id
                    };

                    if (!await _db.Masters.AnyAsync(master => master.Guid == newMaster.Guid)) {
                        await _db.Masters.AddAsync(newMaster);
                        await _db.SaveChangesAsync();
                    } // if
                } // if

                // TODO: Можно также реализовать удаление из таблицы клиентов, к примеру,
                //       если с таковых снимается соответствующая роль

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("Users", "AdminPanel");
            } // if

            return NotFound();
        }
    }
}