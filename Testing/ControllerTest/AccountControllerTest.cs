using AddressApi;
using AddressApi.Contracts;
using AddressApi.Controllers;
using AddressApi.Entities.DTOs.RequestDto;
using AddressApi.Entities.DTOs.ResponseDto;
using AddressApi.Entities.Helper;
using AddressApi.Migrations;
using AddressApi.Repository;
using AddressApi.Service;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Testing.ControllerTest
{
    public class AccountControllerTest
    {
        private readonly AccountController controller;
        private readonly IAccountService _accountService;
        private readonly IAccountRepository _accountRepository;
        private readonly JWTManagerRepository _jwtManagerRepository;
        private readonly RepositoryContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;
        private readonly ILog _log;
        
        public AccountControllerTest()
        {

            _accountRepository = new AccountRepository(TestData.inmemory());
            _accountService = new AccountService(_accountRepository);
            controller = new AccountController(_accountService, _jwtManagerRepository,_logger);
           


        }
       
        [Fact]
        public void CountUser_ReturnOkStatus()
        {
           
            AccountController accountController = controller;
            IActionResult result = controller.CountUsers();

            OkObjectResult finalresult = Assert.IsType<OkObjectResult>(result);
            CountDto countdata = finalresult.Value as CountDto;

            Assert.NotNull(result);
            Assert.Equal(200, finalresult.StatusCode);
            Assert.Equal(2, countdata.Count);


        }

        /// <summary>
        /// Create API-test Ok status
        /// </summary>
        /// <returns></returns>
        [Fact]
        public void CreateUser_OK_ReturnOkStatus()
        {
            
            UserDto user = new UserDto();
            user.UserName = "k";
            user.Password = "Navee@2002";
            user.FirstName = "Naveena";
            user.LastName = "T";
            user.Address = new List<AddressDto>();
            user.Email = new List<EmailDto>();
            user.phones = new List<PhoneNumberDto>();
            AddressDto address1 = new AddressDto()
            {

                Line1 = "1222",
                Line2 = "1222",
                City = "Chennai",
                ZipCode = 624617,
                stateName = "TamilNadu",
                Type = new TypesDto() { Key = "WORK" },
                country = new TypesDto() { Key = "WORK" }
            };
            user.Address.Add(address1);
            EmailDto email1 = new EmailDto()
            {
                EmailAddress = "12@gmail.com",
                Type = new TypesDto() { Key = "WORK" },
            };
            user.Email.Add(email1);
            PhoneNumberDto phoneNumber1 = new PhoneNumberDto()
            {
                PhoneNumber = "9876543210",
                Type = new TypesDto() { Key = "WORK" },
            };
            user.phones.Add(phoneNumber1);
            var actionResult = controller.CreateUser(user);
            var result = actionResult as CreatedResult;
            string model = (string)result.Value;
            Assert.NotNull(model);
            Assert.IsType<string>(model);
            Assert.IsType<CreatedResult>(result);
        }

        /// <summary>
        /// <returns></returns>
        /// test and conflict if username already exist
        /// <returns></returns>
        /// </summary>
        [Fact]
        public void CreateUser_UserAlreadyExsist_ReturnConflict()
        {
            UserDto user = new UserDto();
            user.UserName = "sara";
            user.Password = "Navee@2002";
            user.FirstName = "Naveena";
            user.LastName = "T";
            user.Address = new List<AddressDto>();
            user.Email = new List<EmailDto>();
            user.phones = new List<PhoneNumberDto>();
            AddressDto address1 = new AddressDto()
            {

                Line1 = "1222",
                Line2 = "1222",
                City = "Chennai",
                ZipCode = 624617,
                stateName = "TamilNadu",
                Type = new TypesDto() { Key = "WORK" },
                country = new TypesDto() { Key = "WORK" }
            };
            user.Address.Add(address1);
            EmailDto email1 = new EmailDto()
            {
                EmailAddress = "1@gmail.com",
                Type = new TypesDto() { Key = "WORK" },
            };
            user.Email.Add(email1);
            PhoneNumberDto phoneNumber1 = new PhoneNumberDto()
            {
                PhoneNumber = "9876543210",
                Type = new TypesDto() { Key = "WORK" },
            };
            user.phones.Add(phoneNumber1);
            var result = controller.CreateUser(user);
            var action = result as ConflictObjectResult;
            string model = action.Value as string;
            Assert.NotNull(model);
            Assert.StrictEqual("Username already exists in the database", model);
            Assert.IsType<ConflictObjectResult>(result);
        }
        /// <summary>
        /// <returns></returns>
        /// test conflict email already exist
        /// <returns></returns>
        /// </summary>
        [Fact]
        public void CreateUser_EmailAlreadyExsist_ReturnConflict()
        {
            UserDto user = new UserDto();
            user.UserName = "saran";
            user.Password = "Navee@2002";
            user.FirstName = "Naveena";
            user.LastName = "T";
            user.Address = new List<AddressDto>();
            user.Email = new List<EmailDto>();
            user.phones = new List<PhoneNumberDto>();
            AddressDto address1 = new AddressDto()
            {

                Line1 = "1222",
                Line2 = "1222",
                City = "Chennai",
                ZipCode = 624617,
                stateName = "TamilNadu",
                Type = new TypesDto() { Key = "WORK" },
                country = new TypesDto() { Key = "WORK" }
            };
            user.Address.Add(address1);
            EmailDto email1 = new EmailDto()
            {
                EmailAddress = "1@gmail.com",
                Type = new TypesDto() { Key = "WORK" },
            };
            user.Email.Add(email1);
            PhoneNumberDto phoneNumber1 = new PhoneNumberDto()
            {
                PhoneNumber = "9876543210",
                Type = new TypesDto() { Key = "WORK" },
            };
            user.phones.Add(phoneNumber1);
            var result = controller.CreateUser(user);
            var action = result as ConflictObjectResult;
            string model = action.Value as string;
            Assert.NotNull(model);
            Assert.StrictEqual("Email already exists in the database", model);
            Assert.IsType<ConflictObjectResult>(result);
        }
        [Fact]
        public void CreateUser_NotFound_ReturnConflict()
        {
          
            UserDto user = new UserDto();
            user.UserName = "saran";
            user.Password = "Navee@2002";
            user.FirstName = "Naveena";
            user.LastName = "T";
            user.Address = new List<AddressDto>();
            user.Email = new List<EmailDto>();
            user.phones = new List<PhoneNumberDto>();
            AddressDto address1 = new AddressDto()
            {

                Line1 = "1222",
                Line2 = "1222",
                City = "Chennai",
                ZipCode = 624617,
                stateName = "TamilNadu",
                Type = new TypesDto() { Key = "WORK" },
                country = new TypesDto() { Key = "WORK" }
            };
            user.Address.Add(address1);
            EmailDto email1 = new EmailDto()
            {
                EmailAddress = "ab@gmail.com",
                Type = new TypesDto() { Key = "NIL" },
            };
            user.Email.Add(email1);
            PhoneNumberDto phoneNumber1 = new PhoneNumberDto()
            {
                PhoneNumber = "9876543210",
                Type = new TypesDto() { Key = "WORK" },
            };
            user.phones.Add(phoneNumber1);
            var result = controller.CreateUser(user);
            var action = result as NotFoundObjectResult;
            Assert.IsType<NotFoundObjectResult>(action);
        }
        [Fact]
        public void GetAllUser_ReturnOK()
        {
            var pagination = new Pagination()
            {
                pageNumber = 1,
                pageSize = 10,
                SortBy = "FirstName",
                SortOrder = "DSC",
            };
            UserForCreatingDto user = new UserForCreatingDto();
            user.FirstName = "Naveena";
            user.LastName = "T";
            user.Address = new List<AddressForCreatingDto>();
            user.Email = new List<EmailForCreatingDto>();
            user.Phone = new List<PhoneForCreatingDto>();
            AddressForCreatingDto address1 = new AddressForCreatingDto()
            {

                Line1 = "1222",
                Line2 = "1222",
                City = "Chennai",
                ZipCode = 624617,
                stateName = "TamilNadu",
                AddressId = Guid.Parse("d7374434-18c3-4100-5e95-08dae952ed30"),

                Type = new TypesDto() { Key = "WORK" },
                country = new TypesDto() { Key = "WORK" }
            };
            user.Address.Add(address1);
            EmailForCreatingDto email1 = new EmailForCreatingDto()
            {
                EmailAddress = "abi@gmail.com",
                Type = new TypesDto() { Key = "nIL" },
                EmailId = Guid.Parse("0c889a57-1df9-47f8-f9ba-08dae952ed36"),
            };
            user.Email.Add(email1);
            PhoneForCreatingDto phoneNumber1 = new PhoneForCreatingDto()
            {
                PhoneNumber = "9876543210",
                Type = new TypesDto() { Key = "WORK" },
                PhoneNumberId = Guid.Parse("abd49ed6-1204-4472-7bca-08dae952ed37"),
            };
            user.Phone.Add(phoneNumber1);
            IActionResult result = controller.GetAllUser(pagination);
            OkObjectResult action = result as OkObjectResult;
            List<UserForCreatingDto> model = action.Value as List<UserForCreatingDto>;
            Assert.NotNull(model);
            Assert.Equal(2, model.Count);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetbyId_User_ReturnUser()
        {

            Guid guid = Guid.Parse("c572c99e-ee1f-4d17-b69c-08dae952ed26");
            var result = controller.GetById(guid);
            UserForCreatingDto user = new UserForCreatingDto();
            user.Id = guid;
            user.FirstName = "Naveena";
            user.LastName = "T";
            user.Address = new List<AddressForCreatingDto>();
            user.Email = new List<EmailForCreatingDto>();
            user.Phone = new List<PhoneForCreatingDto>();
            AddressForCreatingDto address1 = new AddressForCreatingDto()
            {

                Line1 = "1222",
                Line2 = "1222",
                City = "Chennai",
                ZipCode = 624617,
                stateName = "TamilNadu",
                AddressId = Guid.Parse("d7374434-18c3-4100-5e95-08dae952ed30"),

                Type = new TypesDto() { Key = "WORK" },
                country = new TypesDto() { Key = "WORK" }
            };
            user.Address.Add(address1);
            EmailForCreatingDto email1 = new EmailForCreatingDto()
            {
                EmailAddress = "abi@gmail.com",
                Type = new TypesDto() { Key = "nIL" },
                EmailId = Guid.Parse("0c889a57-1df9-47f8-f9ba-08dae952ed36"),
            };
            user.Email.Add(email1);
            PhoneForCreatingDto phoneNumber1 = new PhoneForCreatingDto()
            {
                PhoneNumber = "9876543210",
                Type = new TypesDto() { Key = "WORK" },
                PhoneNumberId = Guid.Parse("abd49ed6-1204-4472-7bca-08dae952ed37"),
            };
            user.Phone.Add(phoneNumber1);
            OkObjectResult action = result as OkObjectResult;
            UserForCreatingDto model = action.Value as UserForCreatingDto;
            Assert.NotNull(model);
            Assert.Equal(user.Id, model.Id);
            Assert.IsType<OkObjectResult>(result);


        }

        /// <summary>
        /// test that returns Not Found if user not found
        /// </summary>
        [Fact]
        public void GetbyId_userNotFound_ReturnNotFound()
        {
  
            UserForCreatingDto user = new UserForCreatingDto();
            Guid guid = Guid.Parse("c572c99e-ee1f-4d17-b69c-08dae952ed29");
            var result = controller.GetById(guid);

            Assert.IsType<NotFoundObjectResult>(result);
        }
        /// <summary>
        /// test that returns Not Found if user not found
        /// </summary>
        [Fact]
        public void GetbyId_unAuthorize_ReturnUnAuthorize()
        {
           

            UserForCreatingDto user = new UserForCreatingDto();
            Guid guid = Guid.Parse("c572c90e-ee1f-4d17-b69c-08dae952ed29");
            var result = controller.GetById(guid);
            Assert.IsType<UnauthorizedResult>(result);
        }
        /// <summary>
        /// test that return updated user when it updates successfully
        /// </summary>
        [Fact]
        public void UpdateTest_updateById_ReturnUser()
        {
            Guid guid = Guid.Parse("c572c99e-ee1f-4d17-b69c-08dae952ed26");
            var db = TestData.inmemory();
            var accountrepository = new AccountRepository(db);
            var accountservice = new AccountService(accountrepository);
            var acc = new HttpContextAccessor();
            var usercheck = new JWTManagerRepository(db, _configuration, acc);
            var controller = new AccountController(accountservice, usercheck, _logger);
            UserDto user = new UserDto();
            user.UserName = "saranya";
            user.Password = "Navee@2002";
            user.FirstName = "Naveena";
            user.LastName = "T";
            user.Address = new List<AddressDto>();
            user.Email = new List<EmailDto>();
            user.phones = new List<PhoneNumberDto>();
            AddressDto address1 = new AddressDto()
            {

                Line1 = "1222",
                Line2 = "1222",
                City = "Chennai",
                ZipCode = 624617,
                stateName = "TamilNadu",
                Type = new TypesDto() { Key = "WORK" },
                country = new TypesDto() { Key = "WORK" }
            };
            user.Address.Add(address1);
            EmailDto email1 = new EmailDto()
            {
                EmailAddress = "saran@gmail.com",
                Type = new TypesDto() { Key = "WORK" },
            };
            user.Email.Add(email1);
            PhoneNumberDto phoneNumber1 = new PhoneNumberDto()
            {
                PhoneNumber = "9876543210",
                Type = new TypesDto() { Key = "WORK" },
            };
            user.phones.Add(phoneNumber1);
            UpdateDto user1 = new UpdateDto();
            user1.UserName = "saranya";
            user1.Password = "Navee@2002";
            user1.FirstName = "Naveena";
            user1.LastName = "T";
            user1.Address = new List<AddressDto>();
            user1.Email = new List<EmailDto>();
            user1.phoneNumber = new List<PhoneNumberDto>();
            AddressDto address2 = new AddressDto()
            {

                Line1 = "1222",
                Line2 = "1222",
                City = "Chennai",
                ZipCode = 624617,
                stateName = "TamilNadu",
                Type = new TypesDto() { Key = "WORK" },
                country = new TypesDto() { Key = "WORK" }
            };
            user1.Address.Add(address2);
            EmailDto email2 = new EmailDto()
            {
                EmailAddress = "sa@gmail.com",
                Type = new TypesDto() { Key = "WORK" },
            };
            user1.Email.Add(email2);
            PhoneNumberDto phoneNumber2 = new PhoneNumberDto()
            {
                PhoneNumber = "9876543210",
                Type = new TypesDto() { Key = "WORK" },
            };
            user1.phoneNumber.Add(phoneNumber2);
            var result = controller.Update(guid, user);
            var action = result as OkObjectResult;
            UpdateDto model = action.Value as UpdateDto;
            Assert.NotNull(model);
            Assert.Same(user.FirstName, model.FirstName);
            Assert.IsType<OkObjectResult>(result);

        }
        /// <summary>
        /// test that method returns conflict
        /// </summary>
        [Fact]
        public void Update_Exists_ReturnConflict()
        {
            Guid guid = Guid.Parse("c572c99e-ee1f-4d17-b69c-08dae952ed26");
            UserDto user = new UserDto();
            user.UserName = "sara";
            user.Password = "Navee@2002";
            user.FirstName = "Naveena";
            user.LastName = "T";
            user.Address = new List<AddressDto>();
            user.Email = new List<EmailDto>();
            user.phones = new List<PhoneNumberDto>();
            AddressDto address1 = new AddressDto()
            {

                Line1 = "1222",
                Line2 = "1222",
                City = "Chennai",
                ZipCode = 624617,
                stateName = "TamilNadu",
                Type = new TypesDto() { Key = "WORK" },
                country = new TypesDto() { Key = "WORK" }
            };
            user.Address.Add(address1);
            EmailDto email1 = new EmailDto()
            {
                EmailAddress = "1@gmail.com",
                Type = new TypesDto() { Key = "WORK" },
            };
            user.Email.Add(email1);
            PhoneNumberDto phoneNumber1 = new PhoneNumberDto()
            {
                PhoneNumber = "9876543210",
                Type = new TypesDto() { Key = "WORK" },
            };
            user.phones.Add(phoneNumber1);
            var result = controller.Update(guid, user);
            Assert.IsType<ConflictObjectResult>(result);

        }
        /// <summary>
        /// test that method return NotFound if user doesn't exist
        /// </summary>
        [Fact]
        public void UpdateTest_userNotFound_ReturnNotFound()
        {
            Guid guid = Guid.Parse("c572c99e-ee1f-4d17-b69c-085ae952ed26");
            UserDto user = new UserDto();
            user.UserName = "sara";
            user.Password = "Navee@2002";
            user.FirstName = "Naveena";
            user.LastName = "T";
            user.Address = new List<AddressDto>();
            user.Email = new List<EmailDto>();
            user.phones = new List<PhoneNumberDto>();
            AddressDto address1 = new AddressDto()
            {

                Line1 = "1222",
                Line2 = "1222",
                City = "Chennai",
                ZipCode = 624617,
                stateName = "TamilNadu",
                Type = new TypesDto() { Key = "WORK" },
                country = new TypesDto() { Key = "WORK" }
            };
            user.Address.Add(address1);
            EmailDto email1 = new EmailDto()
            {
                EmailAddress = "saran@gmail.com",
                Type = new TypesDto() { Key = "WORK" },
            };
            user.Email.Add(email1);
            PhoneNumberDto phoneNumber1 = new PhoneNumberDto()
            {
                PhoneNumber = "9876543210",
                Type = new TypesDto() { Key = "WORK" },
            };
            user.phones.Add(phoneNumber1);
            var result = controller.Update(guid, user);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        /// <summary>
        /// test that method deletes the user
        /// </summary>
        [Fact]
        public void Delete_ReturnDeleted()
        {
            Guid guid = Guid.Parse("c572c99e-ee1f-4d17-b69c-08dae952ed26");
            
            IActionResult result = controller.DeleteUser(guid);

            Assert.IsType<OkObjectResult>(result);
        }
        /// <summary>
        /// test that returns id not found
        /// </summary>
        [Fact]
        public void DeleteTest_IdNotFound_ReturnNotFound()
        {
           
            IActionResult result = controller.DeleteUser(Guid.Parse("c572c99e-ee1f-4d17-b69c-08dae952ed26"));
            Assert.Null(result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        /// <summary>
        /// test that returns unauthorize
        /// </summary>
        [Fact]
        public void DeleteTest_ReturnUnauthorize()
        {

            IActionResult result = controller.DeleteUser(Guid.Parse("c572c99e-ee1f-4d10-b69c-08dae952ed26"));
           
            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}