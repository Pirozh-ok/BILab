using BILab.Domain;
using BILab.Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace BILab.DataAccess {
    public class SeedData {
        private readonly UserManager<User> _userManager;

        public SeedData(UserManager<User> userManager) {
            _userManager = userManager;
        }

        public async Task SeedUsers() {
            var adminEmail = "birkinvlad@gmail.com";

            if (_userManager.FindByEmailAsync(adminEmail).Result is null) {
                var user = new User() {
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FirstName = "Владислав",
                    LastName = "Биркин",
                    Patronymic = "Фёдорович",
                    Sex = Domain.Enums.Sex.Male,
                    DateOfBirth = new DateTime(2002, 05, 31),
                    RegisterDate = DateTime.Now,
                    PhoneNumber = "+79149845637"
                };

                var result = await _userManager.CreateAsync(user, "password");

                if (result.Succeeded) {
                    await _userManager.AddToRoleAsync(user, Constants.NameRoleAdmin);
                }
            }
        }
    }
}
