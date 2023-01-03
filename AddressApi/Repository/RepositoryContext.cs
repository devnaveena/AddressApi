using AddressApi;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressApi.Repository
{
    public class RepositoryContext : DbContext

    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) { }
        public DbSet<User> User { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<PhoneNumber> PhoneNumber { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<FileModel> File { get; set; }
        public DbSet<RefSet> RefSet { get; set; }
        public DbSet<SetRef> Setref { get; set; }
        public DbSet<Types> Type { get; set; }
        public DbSet<Refterm> Refterm { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefSet>().HasData(
                new RefSet()
                {
                    RefSetId = Guid.Parse("1398FF0D-2062-4594-23D4-08DAC5F97924"),
                    Name = "PERSONAL",
                    Description = "For  personal"
                },
            new RefSet()
            {
                RefSetId = Guid.Parse("1398FF0D-2062-4594-23D4-08DAC5F97923"),
                Name = "WORK",
                Description = "For the work"
            },
            new RefSet()
            {
                RefSetId = Guid.Parse("1398FF0D-2062-4594-23D4-08DAC5F97922"),
                Name = "ALTERNATE",
                Description = "For the alternate"
            },
                new RefSet()
                {
                    RefSetId = Guid.Parse("1398FF0D-2062-4594-23D4-08DAC5F97914"),
                    Name = "INDIA",
                    Description = "For the country India"
                },
                new RefSet()
                {
                    RefSetId = Guid.Parse("1298FF0D-2062-4594-23D4-08DAC5F97924"),
                    Name = "USA",
                    Description = "For the country USA"
                },
                 new RefSet()
                 {
                     RefSetId = Guid.Parse("1398FF0D-2062-4594-23D4-08DAC5F97921"),
                     Name = "UK",
                     Description = "for the country UK"
                 },
                  new RefSet()
                  {
                      RefSetId = Guid.Parse("1398FF0D-2062-5594-23D4-08DAC5F97924"),
                      Name = "JAPAN",
                      Description = "for the country Japan"
                  }

                );
            modelBuilder.Entity<User>().HasData(
            new User()
            {
                Id = Guid.Parse("1398FF0D-2062-4594-33D4-08DAC5F97924"),
                UserName = "Naveena",
                Password = "Navee@123",
                FirstName = "Naveena",
                LastName = "T",
                IsActive = true
            });
            modelBuilder.Entity<Refterm>().HasData(
            new Refterm()
            {
                Id = 1,
                key = "ADDRESS TYPE",
            },
            new Refterm()
            {
                Id = 2,
                key = "PHONE NUMBER TYPE",

            },
             new Refterm()
             {
                 Id = 3,
                 key = "EMAIL ADDRESS TYPE",

             },
              new Refterm()
              {
                  Id = 4,
                  key = "Country",

              },
              new Refterm()
              {
                  Id = 5,
                  key = "India",

              },
              new Refterm()
              {
                  Id = 6,
                  key = "UNITED STATES",

              }

            );

        }
    }
}
