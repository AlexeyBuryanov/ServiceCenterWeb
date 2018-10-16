using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceCenterWeb.Models;

namespace ServiceCenterWeb.Controllers
{
    [Authorize(Roles = "Админ")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid) {
                var user = new User { Email = model.Email, UserName = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded) {
                    return RedirectToAction("Users", "AdminPanel");
                } // if

                foreach (var error in result.Errors) {
                    ModelState.AddModelError(string.Empty, error.Description);
                } // foreach
            } // if

            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null) {
                return NotFound();
            } // if

            var model = new EditUserViewModel { Id = user.Id, Email = user.Email };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid) {
                var user = await _userManager.FindByIdAsync(model.Id);

                if (user != null) {
                    user.Email = model.Email;
                    user.UserName = model.Email;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded) {
                        return RedirectToAction("Users", "AdminPanel");
                    } // if

                    foreach (var error in result.Errors) {
                        ModelState.AddModelError(string.Empty, error.Description);
                    } // foreach
                } // if
            } // if

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null) {
                var result = await _userManager.DeleteAsync(user);
            } // if

            return RedirectToAction("Users", "AdminPanel");
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null) {
                return NotFound();
            } // if

            var model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid) {
                var user = await _userManager.FindByIdAsync(model.Id);

                if (user != null) {
                    var passwordValidator = HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
                    var passwordHasher = HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

                    var result = await passwordValidator?.ValidateAsync(_userManager, user, model.NewPassword);

                    if (result.Succeeded) {
                        user.PasswordHash = passwordHasher?.HashPassword(user, model.NewPassword);
                        await _userManager.UpdateAsync(user);
                        return RedirectToAction("Users", "AdminPanel");
                    } // if

                    foreach (var error in result.Errors) {
                        ModelState.AddModelError(string.Empty, error.Description);
                    } // foreach
                } else {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                } // if
            } // if

            return View(model);

            /* Для ввода старого и нового пароля
            if (ModelState.IsValid) {
                var user = await _userManager.FindByIdAsync(model.Id);

                if (user != null) {
                    var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                    if (result.Succeeded) {
                        return RedirectToAction("Index");
                    } // if

                    foreach (var error in result.Errors) {
                        ModelState.AddModelError(string.Empty, error.Description);
                    } // foreach
                } else {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                } // if
            } // if

            return View(model);
            */
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}