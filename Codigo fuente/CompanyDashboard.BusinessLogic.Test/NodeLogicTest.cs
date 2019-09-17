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
    public class NodeLogicTest
    {
        [TestMethod]
        public void CompareStrings()
        {
            var Left = new Operator
            {
                Text = "operador",
            };
            var Right = new Operator
            {
                Text = "operador",
            };
            NodeLogic nodeLogic = new NodeLogic(null);
            Assert.IsTrue(nodeLogic.Compare(Left, Right, "="));

        }
        [TestMethod]
        public void CompareEqualsNumeric()
        {
            var Left = new Operator
            {
                Type = 2,
                Text = "123",
            };
            var Right = new Operator
            {
                Type = 2,
                Text = "123",
            };
            NodeLogic nodeLogic = new NodeLogic(null);
            Assert.IsTrue(nodeLogic.Compare(Left, Right, "="));
        }
        [TestMethod]
        public void CompareLesserNumeric()
        {
            var Left = new Operator
            {
                Type = 2,
                Text = "4",
            };
            var Right = new Operator
            {
                Type = 2,
                Text = "123",
            };

            NodeLogic nodeLogic = new NodeLogic(null);
            Assert.IsTrue(nodeLogic.Compare(Left, Right, "<"));
        }
        [TestMethod]
        public void CompareBiggerNumeric()
        {
            var Left = new Operator
            {
                Type = 2,
                Text = "40000",
            };
            var Right = new Operator
            {
                Type = 2,
                Text = "123",
            };
            NodeLogic nodeLogic = new NodeLogic(null);
            Assert.AreEqual(nodeLogic.Evaluate(Right)[1], "123");
            Assert.IsTrue(nodeLogic.CompareBigger(Left, Right));
        }
        [TestMethod]
        public void CompareSQLNumeric()
        {
            var area = new Area{
                DataSource = "Server=localhost\\SQLEXPRESS;Database=DataSourceDB;Trusted_Connection=True;",
            };
            var Left = new Operator
            {
                Type = 3,
                Text = "SELECT count(*) FROM ACCOUNT",
                Area = 1,
            };
            var Right = new Operator
            {
                Type = 2,
                Text = "5",
            };
            NodeLogic nodeLogic = new NodeLogic(null);
            Assert.IsTrue(nodeLogic.Compare(Left, Right, ">"));
        }
        [TestMethod]
        public void CompareSQLString()
        {
            var area = new Area{
                DataSource = "Server=localhost\\SQLEXPRESS;Database=DataSourceDB;Trusted_Connection=True;",
            };
            var Left = new Operator
            {
                Type = 3,
                Text = "SELECT UserId FROM ACCOUNT WHERE UserId = 'ALFKI'",
                Area = 1,
            };
            var Right = new Operator
            {
                Type = 1,
                Text = "ALFKI",
            };
            NodeLogic nodeLogic = new NodeLogic(null);
            Assert.IsTrue(nodeLogic.Compare(Left, Right, "="));
        }
        [TestMethod]
        public void CompareBoolOperator()
        {
            var area = new Area{
                DataSource = "Server=localhost\\SQLEXPRESS;Database=DataSourceDB;Trusted_Connection=True;",
            };
            var Left1 = new Operator
            {
                Type = 3,
                Text = "SELECT UserId FROM ACCOUNT WHERE UserId = 'ALFKI'",
                Area = 1,
            };
            var Right1 = new Operator
            {
                Type = 1,
                Text = "ALFKI",
            };

            var binaryLeft = new BinaryOperator
            {
                Type = 4,
                Left = Left1,
                Right = Right1,
                Sign = "=",
            };

            var Left2 = new Operator
            {
                Type = 3,
                Text = "SELECT count(*) FROM ACCOUNT",
                Area = 1,
            };
            var Right2 = new Operator
            {
                Type = 2,
                Text = "5",
            };
            var binaryRight = new BinaryOperator
            {
                Type = 4,
                Left = Left2,
                Right = Right2,
                Sign = ">",
            };

            NodeLogic nodeLogic = new NodeLogic(null);
            Assert.IsTrue(nodeLogic.Compare(binaryLeft, binaryRight, "AND"));
        }
    }
}
