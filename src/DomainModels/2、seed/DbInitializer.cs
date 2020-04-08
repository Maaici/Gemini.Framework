using DomainModels.context;
using System;
using System.Linq;
using ViewModels.Enums;

namespace DomainModels
{
    public static class DbInitializer
    {
        public static void Initialize(MyDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
            new User{ Mobile="18888888888",Password="123456", RealName = "maaici",Gender =Genders.Man,BirthDay = new DateTime(1992,6,24)},
            new User{ Mobile="19999999999",Password="123456", RealName = "wangtt",Gender =Genders.Woman,BirthDay = new DateTime(1993,8,2)}
            };
            foreach (var s in users)
            {
                context.users.Add(s);
            }
            context.SaveChanges();
        }
    }
}
