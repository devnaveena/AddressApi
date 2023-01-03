using AddressApi;
using AddressApi.Contracts;
using AddressApi.Controllers;
using AddressApi.Entities.DTOs;
using AddressApi.Repository;
using AddressApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.ControllerTest
{
    public class AuthControllerTest
    {
        private readonly Mock<AuthController> _mock;
        private IConfiguration _configuration;
        private readonly AuthController controller;
        
        private readonly IJWTManagerRepository jTManagerRepository ;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthControllerTest()
        {
            jTManagerRepository= new JWTManagerRepository(TestData.inmemory(),_configuration,httpContextAccessor);
            
            controller = new AuthController(jTManagerRepository);
        }
        [Fact]
        public void signinTest_returnToken()
        {
            var config = new Startup(_configuration);

            var jTManagerRepository = new JWTManagerRepository(TestData.inmemory(),_configuration, httpContextAccessor);
            var controller= new AuthController(jTManagerRepository);
           

            SiginInDto user = new SiginInDto() { UserName = "sara",Password = "Navee@2002" };

            var result = controller.Authenticate(user);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void signoutTest_returnUnAuthorized() {
            var jTManagerRepository = new JWTManagerRepository(TestData.inmemory(), _configuration, httpContextAccessor);
            var controller = new AuthController(jTManagerRepository);


            SiginInDto user = new SiginInDto() { UserName = "sar", Password = "Navee@2002" };
            var result = controller.Authenticate(user);
            Assert.NotNull(result);
            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}
