using Microsoft.EntityFrameworkCore;
using WorkersServer.Models.POCOs;

namespace WorkersServer.Data
{
    public class InitialSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var dbContext = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            if (!dbContext.Workers.Any())
            {
                dbContext.Workers.AddRange(new Worker
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Иван",
                        LastName = "Иванов",
                        MiddleName = "Иванович",
                        Birthday = new DateTime(1990, 4, 5),
                        HaveChildren = true,
                        Sex = true
                    },
                    new Worker
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Петр",
                        LastName = "Петров",
                        MiddleName = "Иванович",
                        Birthday = new DateTime(1980, 1, 5),
                        HaveChildren = true,
                        Sex = true
                    },
                    new Worker
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Евгений",
                        LastName = "Сидоров",
                        MiddleName = "Иванович",
                        Birthday = new DateTime(1985, 10, 15),
                        HaveChildren = true,
                        Sex = true
                    });
                dbContext.SaveChanges();
            }
        }
    }
}
