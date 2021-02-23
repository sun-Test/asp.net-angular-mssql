using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using sunny_dn_01.DataContext;

namespace sunny_dn_01.Domains
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                SeedData(context);

            }
        }

        public static void SeedData(AppDbContext context)
        {
            if (context.Users.Any()) //&& context.Votings.Any()
            {
                Console.WriteLine("already seeded");
                //return;   // Database has been seeded
            }
            else
            {
                Console.WriteLine("db is empty, start seeding");
                User user = new User { Email = "ttt@tt.com" };
                context.Users.AddRange(
                    new User
                    {
                        Email = "sunny@outlook.com"
                });

                context.SaveChanges();
                //Voting voting = new Voting { CandidateID = user.ID };
                //context.Votings.AddRange(voting);

                //context.SaveChanges();

            }
        }
    }
}
