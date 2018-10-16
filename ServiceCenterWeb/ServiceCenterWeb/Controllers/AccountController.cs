using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceCenterWeb.Models;
using ServiceCenterWeb.Models.Database;
using ServiceCenterWeb.Utils;

namespace ServiceCenterWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _db;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, 
                                 ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register() => View();

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid) {
                // Логика такова 2:
                // Сначала регистрируем пользователя в Identity, затем клиента в таблице Clients,
                // где поле guid будет идентифицировать пользователя в Identity.

                var user = new User {
                    Email = model.Email,
                    UserName = model.Email,
                    PhoneNumber = model.MobilePhone,
                    AvatarPath = @"/images/avatars/default.jpg"
                };

                var resCreateUser = await _userManager.CreateAsync(user, model.Password);
                var resAddToRole1 = await _userManager.AddToRoleAsync(user, "Пользователь");
                var resAddToRole2 = await _userManager.AddToRoleAsync(user, "Клиент");

                var userCreate = await _userManager.Users.FirstAsync(u => u.Email == model.Email);
                if (userCreate != null) {
                    var client = new Client {
                        Id = 0,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        MobilePhone = model.MobilePhone,
                        Reputation = 0,
                        Guid = userCreate.Id
                    };

                    await _db.Clients.AddAsync(client);
                    await _db.SaveChangesAsync();
                } // if

                if (resCreateUser.Succeeded) {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action("ConfirmEmail", 
                                                 "Account",
                                                 new { userId = user.Id, token },
                                                 HttpContext.Request.Scheme);
                    await EmailService.SendEmailAsync(model.Email, 
                                                      "Подтвердите Ваш аккаунт",
                                                      $"Подтвердите регистрацию, перейдя по <a href='{callbackUrl}'>ссылке</a>.");

                    await _signInManager.SignInAsync(user, false);

                    return RedirectToAction("Index", "Home");
                } // if

                AddErrors(resCreateUser);
                AddErrors(resAddToRole1);
                AddErrors(resAddToRole2);
            } // if

            return View(model);
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login(string returnUrl = null) => View(new LoginViewModel { ReturnUrl = returnUrl });

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid) {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded) {
                    // Проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl)) {
                        return Redirect(model.ReturnUrl);
                    } // if

                    return RedirectToAction("Index", "Home");
                } // if

                ModelState.AddModelError("", "Неправильный e-mail и (или) пароль");
            } // if

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userid, string token)
        {
            if (userid == null || token == null) {
                return RedirectToAction("Index", "Home");
            } // if

            var user = await _userManager.FindByIdAsync(userid);

            if (user == null) {
                return View("Error");
            } // if

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded) {
                return RedirectToPage("/Identity/Account/ConfirmEmail");
            } // if

            return View("Register");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid) {
                var user = await _userManager.FindByNameAsync(model.Email);

                if (user == null || !await _userManager.IsEmailConfirmedAsync(user)) {
                    return View("ForgotPasswordConfirmation");
                } // if
                
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);

                var callbackUrl = Url.Action("ResetPassword", "Account", 
                                             new { userId = user.Id, code }, 
                                             HttpContext.Request.Scheme);

                await EmailService.SendEmailAsync(model.Email, "Сброс пароля",
                                                  $"Для сброса пароля пройдите по <a href='{callbackUrl}'>ссылке</a>.");

                return View("ForgotPasswordConfirmation");
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string userid, string code)
        {
            var model = new ResetPasswordViewModel {
                Token = code
            };

            return code == null ? View("Error") : View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid) {
                return View(model);
            } // if

            var user = await _userManager.FindByNameAsync(model.Email);

            if (user == null) {
                return RedirectToAction("Index", "Home");
            } // if

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (result.Succeeded) {
                return RedirectToAction("Login", "Account");
            } // if

            AddErrors(result);

            return View();
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors) {
                ModelState.AddModelError(string.Empty, error.Description);
            } // foreach
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}