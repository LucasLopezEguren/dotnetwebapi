using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CompanyDashboard.BusinessLogic;
using CompanyDashboard.Domain;
using Moq;
using CompanyDashboard.DataAccess.Interface;
using CompanyDashboard.BusinessLogic.Interface;
using CompanyDashboard.BusinessLogic.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace CompanyDashboard.BusinessLogic.Test
{
    [TestClass]
    public class IndicatorLogicTest
    {
        [TestMethod]
        public void CreateIndicatorTest()
        {
            var indicator = new Indicator
            {
                Area = 1,
                Red = new BinaryOperator(),
                Yellow = new BinaryOperator(),
                Green = new BinaryOperator(),
            };

            var mock = new Mock<IRepository<Indicator>>(MockBehavior.Strict);
            mock.Setup(m => m.Add(It.IsAny<Indicator>()));
            mock.Setup(m => m.Has(indicator)).Returns(false);
            mock.Setup(m => m.Save());

            IIndicatorLogic indicatorLogic = new IndicatorLogic(mock.Object);
            var result = indicatorLogic.AddIndicator(indicator);

            mock.VerifyAll();
            Assert.AreEqual(indicator.Name, result.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(AlreadyExistsException))]
        public void CreateExistingIndicatorTest()
        {
            var indicator = new Indicator
            {
                Area = 1,
                Red = new BinaryOperator(),
                Yellow = new BinaryOperator(),
                Green = new BinaryOperator(),
            };

            var mock = new Mock<IRepository<Indicator>>(MockBehavior.Strict);
            mock.Setup(m => m.Add(It.IsAny<Indicator>()));
            mock.Setup(m => m.Has(indicator)).Returns(true);
            mock.Setup(m => m.Save());

            IIndicatorLogic indicatorLogic = new IndicatorLogic(mock.Object);
            var result = indicatorLogic.AddIndicator(indicator);

            mock.VerifyAll();
            Assert.AreEqual(indicator.Name, result.Name);
        }

        // [TestMethod]
        // public void DeleteIndicatorTest()
        // {
        //     var indicator = new Indicator
        //     {
        //         Area = 1,
        //         Red = new BinaryOperator(),
        //         Yellow = new BinaryOperator(),
        //         Green = new BinaryOperator(),
        //     };

        //     var mock = new Mock<IRepository<Indicator>>(MockBehavior.Strict);
        //     mock.Setup(m => m.Has(It.IsAny<Indicator>())).Returns(true);
        //     mock.Setup(m => m.Delete(It.IsAny<Indicator>()));
        //     mock.Setup(m => m.Save());

        //     IIndicatorLogic indicatorLogic = new IndicatorLogic(mock.Object);
        //     indicatorLogic.DeleteIndicator(indicator.ID);

        //     mock.VerifyAll();
        // }

        [TestMethod]
        [ExpectedException(typeof(DoesNotExistsException))]
        public void DeleteUnexistingIndicatorTest()
        {
            var indicator = new Indicator
            {
                Area = 1,
                Red = new BinaryOperator(),
                Yellow = new BinaryOperator(),
                Green = new BinaryOperator(),
            };

            var mock = new Mock<IRepository<Indicator>>(MockBehavior.Strict);
            mock.Setup(m => m.Has(It.IsAny<Indicator>())).Returns(false);
            mock.Setup(m => m.Delete(It.IsAny<Indicator>()));
            mock.Setup(m => m.Save());

            IIndicatorLogic indicatorLogic = new IndicatorLogic(mock.Object);
            indicatorLogic.DeleteIndicator(indicator.ID);

            mock.VerifyAll();
        }

        [TestMethod]
        public void UpdateIndicatorTest()
        {
            var indicator = new Indicator
            {
                Name = "Indicator",
                Area = 1,
                Red = new BinaryOperator(),
                Yellow = new BinaryOperator(),
                Green = new BinaryOperator(),
            };

            var mock = new Mock<IRepository<Indicator>>(MockBehavior.Strict);
            mock.Setup(m => m.Update(It.IsAny<Indicator>()));
            mock.Setup(m => m.Has(indicator)).Returns(true);
            mock.Setup(m => m.Save());

            IIndicatorLogic indicatorLogic = new IndicatorLogic(mock.Object);
            indicator.Name = "Indicator2";
            indicatorLogic.UpdateIndicator(indicator);

            mock.VerifyAll();
            Assert.AreEqual(indicator.Name, "Indicator2");
        }

        [TestMethod]
        [ExpectedException(typeof(DoesNotExistsException))]
        public void UpdateUnexistingIndicatorTest()
        {
            var indicator = new Indicator
            {
                Name = "Indicator",
                Area = 1,
                Red = new BinaryOperator(),
                Yellow = new BinaryOperator(),
                Green = new BinaryOperator(),
            };

            var mock = new Mock<IRepository<Indicator>>(MockBehavior.Strict);
            mock.Setup(m => m.Update(It.IsAny<Indicator>()));
            mock.Setup(m => m.Has(indicator)).Returns(false);
            mock.Setup(m => m.Save());

            IIndicatorLogic indicatorLogic = new IndicatorLogic(mock.Object);
            indicatorLogic.UpdateIndicator(indicator);
            mock.VerifyAll();
        }

        [TestMethod]
        public void GetAllIndicatorsTest()
        {
            var indicator1 = new Indicator
            {
                Name = "Name1",
                Area = 1,
                Red = new BinaryOperator(),
                Yellow = new BinaryOperator(),
                Green = new BinaryOperator(),
            };
            var indicator2 = new Indicator
            {
                Name = "Name2",
                Area = 1,
                Red = new BinaryOperator(),
                Yellow = new BinaryOperator(),
                Green = new BinaryOperator(),
            };

            List<Indicator> indicators = new List<Indicator>();
            indicators.Add(indicator1);
            indicators.Add(indicator2);

            var mock = new Mock<IRepository<Indicator>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Returns(indicators);

            IIndicatorLogic indicatorLogic = new IndicatorLogic(mock.Object);

            IEnumerable<Indicator> retorno = indicatorLogic.GetAllIndicators();
            List<Indicator> result = indicatorLogic.GetAllIndicators().ToList();

            mock.VerifyAll();
            Assert.AreEqual(result.Count, indicators.Count);
        }

        [TestMethod]
        public void CreateUserIndicator()
        {
            UserLogic ul = new UserLogic(null);
            User u = ul.GetUserByID(1);
            IndicatorLogic il = new IndicatorLogic(null);
            Indicator i2 = il.GetById(2);
            List<Indicator> ia = ul.GetAllIndicators(u);
            ul.AddIndicator(u, i2);
            ul.ShowIndicator(u, i2);
            List<Indicator> reorder = ul.GetAllIndicators(u);
            //il.DeleteIndicator(i2.ID);
            Assert.AreEqual(1, reorder.Count());

        }

        // [TestMethod]
        // public void CreateUser()
        // {
        //     // User us = new User{
        //     //     Username = "Username2",
        //     //     Password = "Password",
        //     //     Name = "Rodrigo",
        //     //     Lastname = "Ricardos",
        //     //     Mail = "Ro@jemeil",
        //     // };
        //     UserLogic ul = new UserLogic(null);
        //     AreaLogic al = new AreaLogic(null);
        //     User u = ul.GetUserByID(1);
        //     IndicatorLogic il = new IndicatorLogic(null);
        //     Indicator i2 = il.GetById(1);
        //     Area a = al.GetAreaByID(1);
        //     //al.AddUser(a, u);
        //     List<Indicator> ia = ul.GetAllIndicators(u);
        //     //ul.AddIndicator(u, i2);
        //     ul.ShowIndicator(u, i2);
        //     List<Indicator> reorder = ul.GetAllIndicators(u);
        //     //il.DeleteIndicator(i2.ID);
        //     Assert.AreEqual(2, reorder.Count());

        // }

        [TestMethod]
        public void CreateArea()
        {
            IAreaLogic areaLogic = new AreaLogic(null);
            Area area = new Area
            {
                Name = "Area 51",
                DataSource = "Server=.\\SQLEXPRESS; Database=DashboardDB; Trusted_Connection=True; MultipleActiveResultSets=true",
            };
            areaLogic.AddArea(area);
        }

        [TestMethod]
        public void CreateIndicator()
        {
            IAreaLogic areaLogic = new AreaLogic(null);
            var area = areaLogic.GetAreaByID(1);

            var Left1 = new Operator
            {
                Type = 5,
                Text = "22/5/2018",
                Area = area.ID,
            };
            var Right1 = new Operator
            {
                Type = 5,
                Text = "22/3/2018",
                Area = area.ID,
            };

            var binaryRed = new BinaryOperator
            {
                Type = 4,
                Left = Left1,
                Right = Right1,
                Sign = "<=",
                Area = area.ID,
            };

            var Left2 = new Operator
            {
                Type = 2,
                Text = "2",
                Area = area.ID,
            };
            var Right2 = new Operator
            {
                Type = 2,
                Text = "3",
                Area = area.ID,
            };
            var binaryGreenRight = new BinaryOperator
            {
                Type = 4,
                Left = Left2,
                Right = Right2,
                Sign = ">",
                Area = area.ID,
            };
            var binaryGreen = new BinaryOperator
            {
                Type = 4,
                Left = binaryRed,
                Right = binaryGreenRight,
                Sign = "OR",
                Area = area.ID,
            };
            var indicator = new Indicator
            {
                Area = area.ID,
                Name = "Prueba1",
                Green = binaryGreen,
                Red = binaryRed,
                Yellow = binaryRed,
            };

            IIndicatorLogic indicatorLogic = new IndicatorLogic(null);
            var result = indicatorLogic.AddIndicator(indicator);

            Assert.AreEqual(indicator.Name, result.Name);
        }

        // [TestMethod]
        // public void CreateIndicator()
        // {
        //     IAreaLogic areaLogic = new AreaLogic(null);
        //     var area = areaLogic.GetAreaByID(1);

        //     var Left1 = new Operator
        //     {
        //         Type = 3,
        //         Text = "SELECT UserId FROM ACCOUNT WHERE UserId = 'ALFKI'",
        //         Area = 1,
        //     };
        //     var Right1 = new Operator
        //     {
        //         Type = 1,
        //         Text = "ALFKI",
        //     };

        //     var binaryRed = new BinaryOperator
        //     {
        //         Type = 4,
        //         Left = Left1,
        //         Right = Right1,
        //         Sign = "=",
        //     };

        //     var Left2 = new Operator
        //     {
        //         Type = 3,
        //         Text = "SELECT count(*) FROM ACCOUNT",
        //         Area = 1,
        //     };
        //     var Right2 = new Operator
        //     {
        //         Type = 2,
        //         Text = "5",
        //     };
        //     var binaryGreen = new BinaryOperator
        //     {
        //         Type = 4,
        //         Left = Left2,
        //         Right = Right2,
        //         Sign = ">",
        //     };

        //     Operator red = new Operator();
        //     var indicator = new Indicator
        //     {
        //         Area = 1,
        //         Name = "Prueba",
        //         Green = binaryGreen,
        //         Red = binaryRed,
        //         Yellow = binaryRed,
        //     };

        //     IIndicatorLogic indicatorLogic = new IndicatorLogic(null);
        //     var result = indicatorLogic.AddIndicator(indicator);

        //     Assert.AreEqual(indicator.Name, result.Name);
        // }*/
    }
}
