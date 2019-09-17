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
    public class AreaControllerTest
    {
        [TestMethod]
        public void GetAllAreasTest()
        {

        }

        [TestMethod]
        public void CreateValidAreaTest()
        {
            var area = new AreaModel
            {
                Name="AreaName",
                DataSource="\\SQLEXPRESS\algo",
            };

            var mock = new Mock<IAreaLogic>(MockBehavior.Strict);
            mock.Setup(m => m.AddArea(It.IsAny<Area>())).Returns(area.ToEntity());
            var controller = new AreaController(mock.Object);

            var actionResult = controller.Post(area);

            var createdResult = actionResult as OkObjectResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(200, createdResult.StatusCode);       
        }

        [TestMethod]
        public void DeleteAreaTest()
        {
            int id = 123;
            var mock = new Mock<IAreaLogic>(MockBehavior.Strict);
            mock.Setup(m => m.DeleteArea(id));
            var controller = new AreaController(mock.Object);

            var result = controller.Delete(id);
            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public void UpdateAreaTest()
        {
            var areaModel = new AreaModel
            {
                Name="AreaName",
                DataSource="\\SQLEXPRESS\algo",
            };

            var area = new Area
            {
                Name="AreaName",
                DataSource="\\SQLEXPRESS\algo",
            };            
            int id = 12;
            var mock = new Mock<IAreaLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetAreaByID(id)).Returns(area);
            mock.Setup(m => m.UpdateArea(area));
            var controller = new AreaController(mock.Object);

            var result = controller.Put(id, areaModel);
            
            var createdResult = result as OkObjectResult;
            mock.VerifyAll();
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(200, createdResult.StatusCode);
        }

        [TestMethod]
        public void GetAreaTest()
        {
            var area = new Area
            {
                Name = "AreaName",
                DataSource = "\\SQLEXPRESS\algo",
            };
            var mock = new Mock<IAreaLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetAreaByID(area.ID)).Returns(area);
            var controller = new AreaController(mock.Object);

            var result = controller.Get(area.ID);

            mock.VerifyAll();
        }        
   
        
    }
}
