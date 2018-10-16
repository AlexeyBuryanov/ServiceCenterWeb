using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ServiceCenterWeb.Models;

namespace ServiceCenterWeb.Utils
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            const string password = "Aa1234568_";
            const string adminEmail = "admin@mail.com";
            const string userEmail = "default-user@mail.com";
            const string masEmail = "default-master@mail.com";
            const string clientEmail = "default-client@mail.com";

            // Инициализация ролей
            if (await roleManager.FindByNameAsync("Админ") == null) {
                await roleManager.CreateAsync(new IdentityRole("Админ"));
            } // if
            if (await roleManager.FindByNameAsync("Пользователь") == null) {
                await roleManager.CreateAsync(new IdentityRole("Пользователь"));
            } // if
            if (await roleManager.FindByNameAsync("Модератор") == null) {
                await roleManager.CreateAsync(new IdentityRole("Модератор"));
            } // if
            if (await roleManager.FindByNameAsync("Менеджер") == null) {
                await roleManager.CreateAsync(new IdentityRole("Менеджер"));
            } // if
            if (await roleManager.FindByNameAsync("Мастер") == null) {
                await roleManager.CreateAsync(new IdentityRole("Мастер"));
            } // if
            if (await roleManager.FindByNameAsync("Клиент") == null) {
                await roleManager.CreateAsync(new IdentityRole("Клиент"));
            } // if

            // Регистрируем первого админа, если его нет
            if (await userManager.FindByNameAsync(adminEmail) == null) {
                var admin = new User { Email = adminEmail, UserName = adminEmail };
                var result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded) {
                    await userManager.AddToRoleAsync(admin, "Админ");
                } // if
            } // if

            // Первый пользователь
            if (await userManager.FindByNameAsync(userEmail) == null) {
                var user = new User { Email = userEmail, UserName = userEmail };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded) {
                    await userManager.AddToRoleAsync(user, "Пользователь");
                } // if
            } // if

            // Клиент
            if (await userManager.FindByNameAsync(clientEmail) == null) {
                var client = new User { Email = clientEmail, UserName = clientEmail };
                var result = await userManager.CreateAsync(client, password);
                if (result.Succeeded) {
                    await userManager.AddToRoleAsync(client, "Клиент");
                    await userManager.AddToRoleAsync(client, "Пользователь");
                } // if
            } // if

            // Мастер
            if (await userManager.FindByNameAsync(masEmail) == null) {
                var master = new User { Email = masEmail, UserName = masEmail };
                var result = await userManager.CreateAsync(master, password);
                if (result.Succeeded) {
                    await userManager.AddToRoleAsync(master, "Мастер");
                    await userManager.AddToRoleAsync(master, "Пользователь");
                } // if
            } // if
        }
    }
}
