using AddressApi.Repository;
using AddressApi;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using FluentAssertions.Execution;

namespace Testing
{
    public static class TestData
    {
        public static RepositoryContext inmemory()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "MyDatabase").Options;
            var context = new RepositoryContext(options);
            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            data(context);

            return context;
        }
        private static void data(RepositoryContext context)
        {

            var user = new User();

            user.Id = Guid.Parse("c572c99e-ee1f-4d17-b69c-08dae952ed26");
            user.UserName = "sara";
            user.Password = "Navee@2002";
            user.FirstName = "Naveena";
            user.LastName = "T";
            user.Email = new List<Email>();

            Email email1 = new Email()
            {
                UserId = Guid.Parse("c572c99e-ee1f-4d17-b69c-08dae952ed26"),
                EmailId = Guid.Parse("0c889a57-1df9-47f8-f9ba-08dae952ed36"),
                EmailAddress = "1@gmail.com",
                Type = Guid.Parse("1398ff0d-2062-4594-23d4-08dac5f97923"),
            };
            user.Email.Add(email1);
            user.Address = new List<Address>();
            Address address1 = new Address()
            {
                UserId = Guid.Parse("c572c99e-ee1f-4d17-b69c-08dae952ed26"),
                AddressId = Guid.Parse("d7374434-18c3-4100-5e95-08dae952ed30"),
                Line1 = "12131",
                Line2 = "street",
                State = "TamilNadu",
                ZipCode = 69991,
                City = "Chennai",
                country = Guid.Parse("1398ff0d-2062-4594-23d4-08dac5f97914"),
                Type = Guid.Parse("1398ff0d-2062-4594-23d4-08dac5f97923"),
            };
            user.Address.Add(address1);
            user.PhoneNumber = new List<PhoneNumber>();
            PhoneNumber phoneNumber = new PhoneNumber()
            {
                UserId = Guid.Parse("c572c99e-ee1f-4d17-b69c-08dae952ed26"),
                phoneId = Guid.Parse("abd49ed6-1204-4472-7bca-08dae952ed37"),
                PhoneNo = "errt",
                Type = Guid.Parse("1398ff0d-2062-4594-23d4-08dac5f97923")
            };
            user.PhoneNumber.Add(phoneNumber);
            user.IsActive = true;
            context.User.Add(user);
            context.SaveChanges();

            var file = new FileModel();
            file.FileType = "application/pdf";
            file.UserId = Guid.Parse("c572c99e-ee1f-4d17-b69c-08dae952ed26");
            file.Id = Guid.Parse("f875eb1a-f830-49b3-5df6-08dae9686397");
            byte[] h = {10};
            file.file = h;
            context.Add(file);
            context.SaveChanges();

            context.RefSet.AddRange(new RefSet()
            {
                RefSetId = Guid.Parse("1398FF0D-2062-4594-23D4-08DAC5F97929"),
                Name = "PERSONAL",
                Description = "For  personal"
            },
            new RefSet()
            {
                RefSetId = Guid.Parse("1398FF0D-2062-4594-23D4-08DAC5F97920"),
                Name = "WORK",
                Description = "For the work"
            },
            new RefSet()
            {
                RefSetId = Guid.Parse("1398FF0D-2062-4594-23D4-08DAC5F97827"),
                Name = "ALTERNATE",
                Description = "For the alternate"
            },
                new RefSet()
                {
                    RefSetId = Guid.Parse("1398FF0D-2062-4594-23D4-08DAC5F97915"),
                    Name = "INDIA",
                    Description = "For the country India"
                },
                new RefSet()
                {
                    RefSetId = Guid.Parse("1298FF0D-2062-4594-23D4-08DAC5F97905"),
                    Name = "USA",
                    Description = "For the country USA"
                });

            context.SaveChanges();


        }

    }
}
