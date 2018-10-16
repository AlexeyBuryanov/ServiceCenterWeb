using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceCenterWeb.Models;

namespace ServiceCenterWeb.Controllers
{
    [Authorize(Roles = "Админ, Модератор, Менеджер")]
    public class AdminPanelController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminPanelController(UserManager<User> userManager, 
                                    RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: /AdminPanel/Index
        [HttpGet]
        public IActionResult Index() => View();

        // GET: /AdminPanel/Users
        [HttpGet]
        public async Task<IActionResult> Users(string email, int page = 1, 
                                               SortUserState sortOrder = SortUserState.UserNameAsc)
        {
            // NOTE: сначала надо выполнить фильтрацию, потом сортировку и в конце пагинацию
            const int pageSize = 3;

            // Фильтрация, он же поиск
            IEnumerable<User> users = await _userManager.Users.ToListAsync();

            if (!string.IsNullOrWhiteSpace(email)) {
                users = users
                        .AsParallel()
                        .WithDegreeOfParallelism(System.Environment.ProcessorCount)
                        .Where(u => u.Email.Contains(email));
            } // if

            // Сортировка
            switch (sortOrder) {
                case SortUserState.UserNameDesc:
                    users = users
                            .AsParallel()
                            .WithDegreeOfParallelism(System.Environment.ProcessorCount)
                            .OrderByDescending(s => s.UserName);
                    break;
                case SortUserState.EmailAsc:
                    users = users
                            .AsParallel()
                            .WithDegreeOfParallelism(System.Environment.ProcessorCount)
                            .OrderBy(s => s.Email);
                    break;
                case SortUserState.EmailDesc:
                    users = users
                            .AsParallel()
                            .WithDegreeOfParallelism(System.Environment.ProcessorCount)
                            .OrderByDescending(s => s.Email);
                    break;
                case SortUserState.ConfirmEmailAsc:
                    users = users
                            .AsParallel()
                            .WithDegreeOfParallelism(System.Environment.ProcessorCount)
                            .OrderBy(s => s.EmailConfirmed);
                    break;
                case SortUserState.ConfirmEmailDesc:
                    users = users
                            .AsParallel()
                            .WithDegreeOfParallelism(System.Environment.ProcessorCount)
                            .OrderByDescending(s => s.EmailConfirmed);
                    break;
                case SortUserState.PhoneNumberAsc:
                    users = users
                            .AsParallel()
                            .WithDegreeOfParallelism(System.Environment.ProcessorCount)
                            .OrderBy(s => s.PhoneNumber);
                    break;
                case SortUserState.PhoneNumberDesc:
                    users = users
                            .AsParallel()
                            .WithDegreeOfParallelism(System.Environment.ProcessorCount)
                            .OrderByDescending(s => s.PhoneNumber);
                    break;
                case SortUserState.ConfirmPhoneNumberAsc:
                    users = users
                            .AsParallel()
                            .WithDegreeOfParallelism(System.Environment.ProcessorCount)
                            .OrderBy(s => s.PhoneNumberConfirmed);
                    break;
                case SortUserState.ConfirmPhoneNumberDesc:
                    users = users
                            .AsParallel()
                            .WithDegreeOfParallelism(System.Environment.ProcessorCount)
                            .OrderByDescending(s => s.PhoneNumberConfirmed);
                    break;
                case SortUserState.TwoFaAsc:
                    users = users
                            .AsParallel()
                            .WithDegreeOfParallelism(System.Environment.ProcessorCount)
                            .OrderBy(s => s.TwoFactorEnabled);
                    break;
                case SortUserState.TwoFaDesc:
                    users = users
                            .AsParallel()
                            .WithDegreeOfParallelism(System.Environment.ProcessorCount)
                            .OrderByDescending(s => s.TwoFactorEnabled);
                    break;
                case SortUserState.LockoutEndAsc:
                    users = users
                            .AsParallel()
                            .WithDegreeOfParallelism(System.Environment.ProcessorCount)
                            .OrderBy(s => s.LockoutEnd);
                    break;
                case SortUserState.LockoutEndDesc:
                    users = users
                            .AsParallel()
                            .WithDegreeOfParallelism(System.Environment.ProcessorCount)
                            .OrderByDescending(s => s.LockoutEnd);
                    break;
                case SortUserState.LockoutEnabledAsc:
                    users = users
                            .AsParallel()
                            .WithDegreeOfParallelism(System.Environment.ProcessorCount)
                            .OrderBy(s => s.LockoutEnabled);
                    break;
                case SortUserState.LockoutEnabledDesc:
                    users = users
                            .AsParallel()
                            .WithDegreeOfParallelism(System.Environment.ProcessorCount)
                            .OrderByDescending(s => s.LockoutEnabled);
                    break;
                case SortUserState.AccessFailedCountAsc:
                    users = users
                            .AsParallel()
                            .WithDegreeOfParallelism(System.Environment.ProcessorCount)
                            .OrderBy(s => s.AccessFailedCount);
                    break;
                case SortUserState.AccessFailedCountDesc:
                    users = users
                            .AsParallel()
                            .WithDegreeOfParallelism(System.Environment.ProcessorCount)
                            .OrderByDescending(s => s.AccessFailedCount);
                    break;
                default:
                    users = users
                            .AsParallel()
                            .WithDegreeOfParallelism(System.Environment.ProcessorCount)
                            .OrderBy(s => s.UserName);
                    break;
            } // switch

            // Пагинация
            var count = users.Count();
            var items = users
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            var viewModel = new UsersListViewModel {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortUserViewModel(sortOrder),
                FilterUserViewModel = new FilterUserViewModel(email),
                Users = items
            };

            return View(viewModel);
        } 

        // GET: /AdminPanel/Roles
        [HttpGet]
        public async Task<IActionResult> Roles() => View(await _roleManager.Roles.ToListAsync());

        // GET: /AdminPanel/Masters
        [HttpGet]
        public IActionResult Masters() => View();

        // GET: /AdminPanel/Clients
        [HttpGet]
        public IActionResult Clients() => View();

        // GET: /AdminPanel/Manufacturers
        [HttpGet]
        public IActionResult Manufacturers() => View();

        // GET: /AdminPanel/TypeTechnics
        [HttpGet]
        public IActionResult TypeTechnics() => View();

        // GET: /AdminPanel/TypeWorks
        [HttpGet]
        public IActionResult TypeWorks() => View();

        // GET: /AdminPanel/Technics
        [HttpGet]
        public IActionResult Technics() => View();

        // GET: /AdminPanel/Orders
        [HttpGet]
        public IActionResult Orders() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}