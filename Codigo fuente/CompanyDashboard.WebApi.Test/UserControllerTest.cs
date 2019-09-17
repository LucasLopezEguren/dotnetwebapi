using System;
using CompanyDashboard.WebApi;
using CompanyDashboard.WebApi.Controllers;
using CompanyDashboard.WebApi.Models;
using CompanyDashboard.Domain;
using CompanyDashboard.BusinessLogic;
using CompanyDashboard.BusinessLogic.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;

namespace CompanyDashboard.WebApi.Test
{
    [TestClass]
    public class UserControllerTest
    {

        [TestMethod]
        public void CreateValidUserTest()
        {
            var user = new UserModel
            {
                Username = "Username",
                Password = "Password",
                Name = "CommonName",
                Lastname = "CommonLastName",
                Mail = "Rick@Bravo",
            };

            var mock = new Mock<IUserLogic>(MockBehavior.Strict);
            mock.Setup(m => m.AddUser(It.IsAny<User>())).Returns(user.ToEntity());
            var controller = new UserController(mock.Object);

            var actionResult = controller.Post(user);

            var createdResult = actionResult as OkObjectResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(200, createdResult.StatusCode);
        }

        [TestMethod]
        public void DeleteUserTest()
        {
            int id = 123;
            var mock = new Mock<IUserLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetUserByID(id)).Returns(It.IsAny<User>());
            mock.Setup(m => m.DeleteUser(id));

            var controller = new UserController(mock.Object);

            var result = controller.Delete(id);

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }


        [TestMethod]
        public void UpdateUserTest()
        {
            var userModel = new UserModel
            {
                Username = "Username",
                Password = "Password",
                Name = "CommonName",
                Lastname = "CommonLastName",
                Mail = "Rick@Bravo",
            };

            var user = new User
            {
                Username = "Username",
                Password = "Password",
                Name = "CommonName",
                Lastname = "CommonLastName",
                Mail = "Rick@Bravo",
            };
            int id = 12;
            var mock = new Mock<IUserLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetUserByID(id)).Returns(user);
            mock.Setup(m => m.UpdateUser(user));
            var controller = new UserController(mock.Object);

            var result = controller.Put(id, userModel);

            var createdResult = result as OkObjectResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(200, createdResult.StatusCode);
        }


    }
}
