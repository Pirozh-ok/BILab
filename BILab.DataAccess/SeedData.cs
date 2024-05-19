using BILab.Domain;
using BILab.Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace BILab.DataAccess {
    public class SeedData {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public SeedData(UserManager<User> userManager, ApplicationDbContext applicationDbContext) {
            _userManager = userManager;
            _context = applicationDbContext;
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

            var empl1Email = "birkinvlad123@gmail.com";
            if (_userManager.FindByEmailAsync(empl1Email).Result is null) {
                var user = new User() {
                    Email = empl1Email,
                    EmailConfirmed = true,
                    FirstName = "Ирина",
                    LastName = "Бордакова",
                    Patronymic = "Николаевна",
                    Sex = Domain.Enums.Sex.Female,
                    DateOfBirth = new DateTime(1977, 11, 07),
                    RegisterDate = DateTime.Now,
                    PhoneNumber = "+79500890504"
                };

                var result = await _userManager.CreateAsync(user, "password");

                if (result.Succeeded) {
                    await _userManager.AddToRoleAsync(user, Constants.NameRoleEmployee);
                }
            }

            var empl2Email = "birkasanvlad123@gmail.com";
            if (_userManager.FindByEmailAsync(empl2Email).Result is null) {
                var user = new User() {
                    Email = empl2Email,
                    EmailConfirmed = true,
                    FirstName = "Светлана",
                    LastName = "Донская",
                    Patronymic = "Николаевна",
                    Sex = Domain.Enums.Sex.Female,
                    DateOfBirth = new DateTime(1974, 11, 07),
                    RegisterDate = DateTime.Now,
                    PhoneNumber = "+79500890504"
                };

                var result = await _userManager.CreateAsync(user, "password");

                if (result.Succeeded) {
                    await _userManager.AddToRoleAsync(user, Constants.NameRoleEmployee);
                }
            }
        }

        public async Task SeedProcedures() {
            _context.Procedures.AddRange(new Procedure {
                Id = Guid.NewGuid(),
                Name = "Плазмолифтинг",
                Description = "По своей эффективности плазмолифтинг практически не имеет аналогов и позволяет решить широчайший спектр проблем, связанных с лечением угревой болезни. В сравнении с прочими методиками лечения акне, плазмолифтинг отличается своей безопасностью. Благодаря тому, что используется плазма пациента, отсутствует возможность отторжения биоматериала, и процесс регенерации проходит без осложнений и аллергических реакций.",
                Type = "Инъекция",
                Price = 1500,
                Picture = "https://institut-clinic.ru/wp-content/uploads/2021/04/ili-ili.jpg"
            },

            new Procedure {
                Id = Guid.NewGuid(),
                Name = "Мезотерапия",
                Description = "Мезотерапия представляет собой инъекционную методику, которая позволяет доставлять активные вещества прямо в проблемные зоны кожи. Она эффективно используется для омоложения, увлажнения, лечения пигментации и укрепления волос.",
                Type = "Инъекция",
                Price = 2000,
                Picture = "https://newscosmetology.com/wp-content/uploads/2021/04/mezoterapiya-chto-eto-takoe.jpg"
            },
            new Procedure {
                Id = Guid.NewGuid(),
                Name = "Лазерная эпиляция",
                Description = "Лазерная эпиляция позволяет избавиться от нежелательных волос на длительный срок. Процедура проводится с помощью современного лазерного оборудования, которое обеспечивает максимальную эффективность и минимальные болевые ощущения.",
                Type = "Аппаратная",
                Price = 2500,
                Picture = "https://likewot.ru/wp-content/uploads/3/4/c/34cf4826b3c214b52b8666b0d2d90d88.jpeg"
            },
            new Procedure {
                Id = Guid.NewGuid(),
                Name = "Контурная пластика",
                Description = "Контурная пластика позволяет скорректировать форму лица и губ с помощью инъекций филлеров. Процедура обеспечивает мгновенный результат и является альтернативой хирургическим методам омоложения.",
                Type = "Инъекция",
                Price = 3000,
                Picture = "https://mia-bags.ru/wp-content/uploads/c/f/a/cfa71578bd35b6c17c2fed99602dbcef.jpeg"
            },
            new Procedure {
                Id = Guid.NewGuid(),
                Name = "Массаж лица",
                Description = "Массаж лица способствует улучшению кровообращения, укреплению мышц и устранению мелких морщин. Процедура проводится с использованием специальных масел и кремов, которые усиливают эффект массажа.",
                Type = "Массаж",
                Price = 1000,
                Picture = "https://filllin.ru/api/media/offer/e8583ac1-110d-4bbe-957d-8d4ce220e1eb-61962932e62d532a07d55aa1.jpg"
            },
            new Procedure {
                Id = Guid.NewGuid(),
                Name = "Фракционная мезотерапия",
                Description = "Фракционная мезотерапия – это инновационная процедура, которая сочетает в себе преимущества мезотерапии и фракционного воздействия. Она позволяет улучшить качество кожи, устранить пигментацию и мелкие морщины.",
                Type = "Инъекция",
                Price = 3500,
                Picture = "https://orimos.ru/wp-content/uploads/4/2/7/427d7f9c869979043e31a01e6d3c8a7c.jpeg"
            });

            await _context.SaveChangesAsync();
        }
    }
}
