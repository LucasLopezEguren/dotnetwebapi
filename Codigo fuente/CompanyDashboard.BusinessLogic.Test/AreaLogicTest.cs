using Microsoft.VisualStudio.TestTools.UnitTesting;
using CompanyDashboard.BusinessLogic;
using CompanyDashboard.Domain;
using Moq;
using CompanyDashboard.DataAccess.Interface;
using CompanyDashboard.BusinessLogic.Interface;
using CompanyDashboard.BusinessLogic.Exceptions;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace CompanyDashboard.BusinessLogic.Test
{
    [TestClass]
    public class AreaLogicTest
    {
       [TestMethod]
        public void CreateAreaTest()
        {
            var area = new Area
            {
                Name = "AreaTest",
                DataSource = "Server=localhost\\SQLEXPRESS;Database=DataSourceDB;Trusted_Connection=True;"
            };
            

            var mock = new Mock<IRepository<Area>>(MockBehavior.Strict);
            mock.Setup(m => m.Add(It.IsAny<Area>()));
            mock.Setup(m => m.Has(area)).Returns(false);
            mock.Setup(m => m.Save());

            IAreaLogic areaLogic = new AreaLogic(mock.Object);
            var result = areaLogic.AddArea(area);

            mock.VerifyAll();
            Assert.AreEqual(area.Name, result.Name);

        }

        [TestMethod]
        public void DeleteAreaTest()
        {
            var area = new Area
            {
                Name = "Name",
            };
            var mock = new Mock<IRepository<Area>>(MockBehavior.Strict);
            mock.Setup(m =>  m.GetByID(area.ID)).Returns(area);
            mock.Setup(m => m.Delete(It.IsAny<Area>()));
            mock.Setup(m => m.Update(It.IsAny<Area>()));
            mock.Setup(m => m.Save());

            IAreaLogic areaLogic = new AreaLogic(mock.Object);
            areaLogic.DeleteArea(area.ID);

            mock.VerifyAll();
        }


        [TestMethod]
        public void UpdateAreaTest()
        {
            var area = new Area
            {
                ID = 123,
                Name = "Name",
            };

            var mock = new Mock<IRepository<Area>>(MockBehavior.Strict);
            mock.Setup(m => m.Update(It.IsAny<Area>()));
            mock.Setup(m => m.Has(It.IsAny<Area>())).Returns(true);
            mock.Setup(m => m.Save());

            IAreaLogic areaLogic = new AreaLogic(mock.Object);
            area.Name = "NewName";
            areaLogic.UpdateArea(area);

            mock.VerifyAll();
            Assert.AreEqual(area.Name, "NewName");
        }

        [TestMethod]
        [ExpectedException(typeof(NotValidException))]
        public void UpdateInvalidAreaTest()
        {
            var area = new Area
            {
                ID = 123,
                Name = "Name",
            };

            var mock = new Mock<IRepository<Area>>(MockBehavior.Strict);
            mock.Setup(m => m.Update(It.IsAny<Area>()));
            mock.Setup(m => m.Has(It.IsAny<Area>())).Returns(true);
            mock.Setup(m => m.Save());

            IAreaLogic areaLogic = new AreaLogic(mock.Object);
            area.Name = "";
            areaLogic.UpdateArea(area);

            mock.VerifyAll();
            Assert.AreEqual(area.Name, "NewName");
        }        

        [TestMethod]
        [ExpectedException(typeof(DoesNotExistsException))]
        public void UpdateUnexistingAreaTest()
        {
            var area = new Area
            {
                ID = 123,
                Name = "Name",
            };

            var mock = new Mock<IRepository<Area>>(MockBehavior.Strict);
            mock.Setup(m => m.Update(It.IsAny<Area>()));
            mock.Setup(m => m.Has(It.IsAny<Area>())).Returns(false);
            mock.Setup(m => m.Save());

            IAreaLogic areaLogic = new AreaLogic(mock.Object);
            area.Name = "NewName";
            areaLogic.UpdateArea(area);

            mock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(AlreadyExistsException))]
        public void CreateExistingAreaTest()
        {
            var area = new Area
            {
                Name = "Name",
            };
            var mock = new Mock<IRepository<Area>>(MockBehavior.Strict);
            mock.Setup(m => m.Add(It.IsAny<Area>()));
            mock.Setup(m => m.Has(area)).Returns(true);
            mock.Setup(m => m.Save());

            IAreaLogic areaLogic = new AreaLogic(mock.Object);
            var result = areaLogic.AddArea(area);
            mock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NotValidException))]
        public void CreateInvalidAreaTest()
        {
            var area = new Area
            {
            };

            var mock = new Mock<IRepository<Area>>(MockBehavior.Strict);
            mock.Setup(m => m.Add(It.IsAny<Area>()));
            mock.Setup(m => m.Has(area)).Returns(false);
            mock.Setup(m => m.Save());

            IAreaLogic areaLogic = new AreaLogic(mock.Object);
            var result = areaLogic.AddArea(area);

            mock.VerifyAll();

        }

        [TestMethod]
        [ExpectedException(typeof(DoesNotExistsException))]
        public void AddUnexistingUserToAreaTest()
        {
            var area = new Area
            {
                Name = "Name",
            };
            var user = new User
            {
                Username = "Username",
            };

            var mock = new Mock<IRepository<Area>>(MockBehavior.Strict);
            mock.Setup(m => m.Has(area)).Returns(true);

            IAreaLogic areaLogic = new AreaLogic(mock.Object);
            areaLogic.AddUser(area, user);
            mock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(DoesNotExistsException))]
        public void AddUserToUnexistingAreaTest()
        {
            Area area = new Area();
            var user = new User
            {
                Username = "Username",
            };
            var mock = new Mock<IRepository<Area>>(MockBehavior.Strict);
            mock.Setup(m => m.Has(area)).Returns(false);

            IAreaLogic areaLogic = new AreaLogic(mock.Object);
            areaLogic.AddUser(area, user);
            mock.VerifyAll();
        }


        [TestMethod]
        [ExpectedException(typeof(DoesNotExistsException))]
        public void AddAdminToAreaTest()
        {
            Area area = new Area();
            var user = new User
            {
                Username = "Username",
                Admin = true,
            };

            var mock = new Mock<IRepository<Area>>(MockBehavior.Strict);
            mock.Setup(m => m.Has(area)).Returns(true);
            IAreaLogic areaLogic = new AreaLogic(mock.Object);

            areaLogic.AddUser(area, user);
            mock.VerifyAll();
        }

        [TestMethod]
        public void GetAreaIndicatorsTest()
        {
            Area area = new Area();
            var user = new User
            {
                Username = "Username",
                Admin = true,
            };

            var mock = new Mock<IRepository<Area>>(MockBehavior.Strict);
            mock.Setup(m => m.Has(area)).Returns(true);
            IAreaLogic areaLogic = new AreaLogic(mock.Object);

            areaLogic.GetIndicators(area);
            mock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(DoesNotExistsException))]
        public void GetUnexistingAreaIndicatorsTest()
        {
            Area area = new Area();
            var user = new User
            {
                Username = "Username",
                Admin = true,
            };

            var mock = new Mock<IRepository<Area>>(MockBehavior.Strict);
            mock.Setup(m => m.Has(area)).Returns(false);
            IAreaLogic areaLogic = new AreaLogic(mock.Object);

            areaLogic.GetIndicators(area);
            mock.VerifyAll();
        }                    

        [TestMethod]
        [ExpectedException(typeof(DoesNotExistsException))]
        public void AddIndicatorToUnexistingAreaTest()
        {
            Area area = new Area();
            Indicator indicator = new Indicator
            {
                Red = new BinaryOperator(),
                Yellow = new BinaryOperator(),
                Green = new BinaryOperator(),
            };

            var mock = new Mock<IRepository<Area>>(MockBehavior.Strict);
            mock.Setup(m => m.Has(area)).Returns(false);
            IAreaLogic areaLogic = new AreaLogic(mock.Object);

            areaLogic.AddIndicator(area, indicator);
            mock.VerifyAll();
        }

        // [TestMethod]
        // [ExpectedException(typeof(AlreadyExistsException))]
        // public void AddExistingIndicatorToAreaTest()
        // {
        //     Area area = new Area();

        //     Indicator indicator = new Indicator
        //     {
        //         Red = new BinaryOperator(),
        //         Yellow = new BinaryOperator(),
        //         Green = new BinaryOperator(),
        //     };
        //     indicator.Name = "asdf";

        //     var mock = new Mock<IRepository<Area>>(MockBehavior.Strict);
        //     mock.Setup(m => m.Has(area)).Returns(true);
        //     IAreaLogic areaLogic = new AreaLogic(mock.Object);

        //   //  area.Indicators.Add(indicator);
        //     areaLogic.AddIndicator(area, indicator);
        //     mock.VerifyAll();
        // }


    }
}
