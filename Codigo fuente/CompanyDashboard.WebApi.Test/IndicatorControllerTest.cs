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
    public class IndicatorControllerTest
    {
        [TestMethod]
        public void GetAllIndicatorsTest()
        {

        }

        // [TestMethod]
        // public void CreateValidIndicatorTest()
        // {
        //     var indicator = new IndicatorModel
        //     {
        //         Name="IndicatorName"
        //     };

        //     var mock = new Mock<IIndicatorLogic>(MockBehavior.Strict);
        //     mock.Setup(m => m.AddIndicator(It.IsAny<Indicator>())).Returns(indicator.ToEntity());
        //     var controller = new IndicatorController(mock.Object);

        //     var actionResult = controller.Post(indicator);

        //     var createdResult = actionResult as OkObjectResult;
        //     Assert.IsNotNull(createdResult);
        //     Assert.AreEqual(200, createdResult.StatusCode);       
        // }

        [TestMethod]
        public void DeleteIndicatorTest()
        {
            int id = 123;
            var mock = new Mock<IIndicatorLogic>(MockBehavior.Strict);
            mock.Setup(m => m.DeleteIndicator(id));
            var controller = new IndicatorController(mock.Object);

            var result = controller.Delete(id);
            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        // [TestMethod]
        // public void UpdateIndicatorTest()
        // {
        //     var indicatorModel = new IndicatorModel
        //     {
        //         Name="Indicator",
        //     };

        //     var indicator = new Indicator
        //     {
        //         Name="Indicator",
        //     };            
        //     int id = 12;
        //     var mock = new Mock<IIndicatorLogic>(MockBehavior.Strict);
        //     mock.Setup(m => m.GetById(id)).Returns(indicator);
        //     mock.Setup(m => m.UpdateIndicator(indicator));
        //     var controller = new IndicatorController(mock.Object);

        //     var result = controller.Put(id, indicatorModel);
            
        //     var createdResult = result as OkObjectResult;
        //     mock.VerifyAll();
        //     Assert.IsNotNull(createdResult);
        //     Assert.AreEqual(200, createdResult.StatusCode);
        // }

        // [TestMethod]
        // public void GetAreaTest()
        // {
        //     var area = new Area
        //     {
        //         Name = "AreaName",
        //         DataSource = "\\SQLEXPRESS\algo",
        //     };
        //     var mock = new Mock<IAreaLogic>(MockBehavior.Strict);
        //     mock.Setup(m => m.GetAreaByID(area.ID)).Returns(area);
        //     var controller = new AreaController(mock.Object);

        //     var result = controller.Get(area.ID);

        //     mock.VerifyAll();
        // }        


      //  Metodo para probar los controllers contra la bd

        // [TestMethod]
        // public void Create()
        // {
        //     var area = new AreaModel
        //     {
        //         Name="AreaNameUpdated",
        //         DataSource="\\SQLEXPRESSpepepe",
        //     };

        //     var controller = new AreaController(null);

        //     controller.Delete(6);

           // var createdResult = actionResult as OkObjectResult;
           // Assert.IsNotNull(createdResult);
          //  Assert.AreEqual(200, createdResult.StatusCode);       
        //}
    
        
    }
}
