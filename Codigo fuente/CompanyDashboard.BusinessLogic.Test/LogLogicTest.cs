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

namespace CompanyDashboard.BusinessLogic.Test
{
    [TestClass]
    public class LogLogicTest
    {
        [TestMethod]
        public void AddRegisterTest()
        {
            String user = "user";
            Log log = new Log{
                Username = user,
                Date = DateTime.Now,
            };

            var mock = new Mock<IRepository<Log>>(MockBehavior.Strict);
            mock.Setup(m => m.Add(It.IsAny<Log>()));
            mock.Setup(m => m.Save());

            ILog LogLogic = new LogLogic(mock.Object);
            var result = LogLogic.AddRegister(log);

            mock.VerifyAll();
            Assert.AreEqual(log.Username, result.Username);
        }
    }
}
