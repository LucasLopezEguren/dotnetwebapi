using Microsoft.VisualStudio.TestTools.UnitTesting;
using CompanyDashboard.BusinessLogic;
using CompanyDashboard.Domain;
using Moq;
using CompanyDashboard.DataAccess.Interface;
using CompanyDashboard.BusinessLogic.Interface;
using CompanyDashboard.BusinessLogic.Exceptions;
using System.Collections.Generic;
using System.Linq;
using CompanyDashboard.DataAccess;
using System;

namespace CompanyDashboard.BusinessLogic.Test
{
    [TestClass]
    public class UserLogicTest
    {
        [TestMethod]
        public void CreateUserTest()
        {
            var user = new User
            {
                Username = "Username",
                Password = "Password",
                Name = "John",
                Lastname = "Bravo",
                Mail = "John@Bravo",
            };

            var mock = new Mock<IRepository<User>>(MockBehavior.Strict);
            mock.Setup(m => m.Add(It.IsAny<User>()));
            mock.Setup(m => m.Has(user)).Returns(false);
            mock.Setup(m => m.Save());

            IUserLogic userLogic = new UserLogic(mock.Object);
            var result = userLogic.AddUser(user);

            mock.VerifyAll();
            Assert.AreEqual(user.Username, result.Username);

        }

        [TestMethod]
        [ExpectedException(typeof(NotValidException))]
        public void CreateInvalidUserTest()
        {
            var user = new User
            {
            };

            var mock = new Mock<IRepository<User>>(MockBehavior.Strict);
            mock.Setup(m => m.Add(It.IsAny<User>()));
            mock.Setup(m => m.Has(user)).Returns(false);
            mock.Setup(m => m.Save());

            IUserLogic userLogic = new UserLogic(mock.Object);
            var result = userLogic.AddUser(user);

            mock.VerifyAll();

        }



        [TestMethod]
        public void DeleteUserTest()
        {
            var user = new User
            {
                ID = 123,
                Username = "Username",
                Password = "Password",
            };

            var mock = new Mock<IRepository<User>>(MockBehavior.Strict);
            mock.Setup(m => m.Has(It.IsAny<User>())).Returns(true);
            mock.Setup(m => m.Delete(It.IsAny<User>()));
            mock.Setup(m => m.Save());

            IUserLogic userLogic = new UserLogic(mock.Object);
            userLogic.DeleteUser(user.ID);

            mock.VerifyAll();
        }

        [TestMethod]
        public void GetUserByID()
        {
            var user = new User
            {
                ID = 123,
                Username = "Username",
                Password = "Password",
                Name = "John",
                Lastname = "Bravo",
                Mail = "John@Bravo",
            };

            var mock = new Mock<IRepository<User>>();
            mock.Setup(m => m.GetByID(user.ID)).Returns(user);

            IUserLogic userLogic = new UserLogic(mock.Object);

            User result = userLogic.GetUserByID(user.ID);

            mock.VerifyAll();
            Assert.AreEqual(result.ID, user.ID);
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            var user = new User
            {
                Username = "Username",
                Password = "Password",
                Name = "Name",
                Lastname = "Lastname",
                Mail = "asfd@aswd.com"
            };

            var mock = new Mock<IRepository<User>>(MockBehavior.Strict);
            mock.Setup(m => m.Update(It.IsAny<User>()));
            mock.Setup(m => m.Has(user)).Returns(true);
            mock.Setup(m => m.Save());

            IUserLogic userLogic = new UserLogic(mock.Object);
            user.Username = "NewUsername";
            userLogic.UpdateUser(user);

            mock.VerifyAll();
            Assert.AreEqual(user.Username, "NewUsername");
        }

        [TestMethod]
        [ExpectedException(typeof(NotValidException))]
        public void UpdateInvalidUserTest()
        {
            var user = new User
            {
                Username = "Username",
                Password = "Password",
            };

            var mock = new Mock<IRepository<User>>(MockBehavior.Strict);
            mock.Setup(m => m.Update(It.IsAny<User>()));
            mock.Setup(m => m.Has(user)).Returns(true);
            mock.Setup(m => m.Save());

            IUserLogic userLogic = new UserLogic(mock.Object);
            user.Username = "";
            userLogic.UpdateUser(user);

            mock.VerifyAll();
            Assert.AreEqual(user.Username, "NewUsername");
        }

        [TestMethod]
        [ExpectedException(typeof(DoesNotExistsException))]
        public void UpdateUnexistingUserTest()
        {
            var user = new User
            {
                ID = 123,
                Username = "Username",
                Password = "Password",
            };

            var mock = new Mock<IRepository<User>>(MockBehavior.Strict);
            mock.Setup(m => m.Update(It.IsAny<User>()));
            mock.Setup(m => m.Has(user)).Returns(false);
            mock.Setup(m => m.Save());

            IUserLogic userLogic = new UserLogic(mock.Object);
            user.Username = "NewUsername";
            userLogic.UpdateUser(user);

            mock.VerifyAll();
        }

        [TestMethod]
        public void GetUserByNameTest()
        {
            var user = new User
            {
                ID = 123,
                Username = "Username",
                Password = "Password",
            };

            var mock = new Mock<IRepository<User>>(MockBehavior.Strict);
            mock.Setup(m => m.GetByName(user.Username)).Returns(user);

            IUserLogic userLogic = new UserLogic(mock.Object);
            User result = userLogic.GetUserByName(user.Username);

            mock.VerifyAll();
            Assert.AreEqual(user, result);
        }

        [TestMethod]
        [ExpectedException(typeof(DoesNotExistsException))]
        public void DeleteUnexistingUserTest()
        {
            var user = new User
            {
                ID = 123,
                Username = "Username",
                Password = "Password",
            };

            var mock = new Mock<IRepository<User>>(MockBehavior.Strict);
            mock.Setup(m => m.Has(It.IsAny<User>())).Returns(false);
            mock.Setup(m => m.Delete(It.IsAny<User>()));
            mock.Setup(m => m.Save());

            IUserLogic userLogic = new UserLogic(mock.Object);
            userLogic.DeleteUser(user.ID);

            mock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(AlreadyExistsException))]
        public void CreateExistingUserTest()
        {
            var user = new User
            {
                Username = "Username",
                Password = "Password",
            };

            var mock = new Mock<IRepository<User>>(MockBehavior.Strict);
            mock.Setup(m => m.Add(It.IsAny<User>()));
            mock.Setup(m => m.Has(user)).Returns(true);
            mock.Setup(m => m.Save());

            IUserLogic userLogic = new UserLogic(mock.Object);
            var result = userLogic.AddUser(user);

            mock.VerifyAll();
        }

        [TestMethod]
        public void UserIsValidTest()
        {
            var user = new User
            {
                Username = "Username",
                Password = "Password",
                Name = "John",
                Lastname = "Bravo",
                Mail = "John@Bravo",
            };

            UserLogic userLogic = new UserLogic(null);
            bool result = userLogic.UserIsValid(user);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetAllUsersTest()
        {
            var user1 = new User
            {
                Username = "Username",
                Password = "Password",
            };
            var user2 = new User
            {
                Username = "Username",
                Password = "Password",
            };

            List<User> users = new List<User>();
            users.Add(user1);
            users.Add(user2);

            var mock = new Mock<IRepository<User>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Returns(users);

            IUserLogic userLogic = new UserLogic(mock.Object);

            IEnumerable<User> retorno = userLogic.GetAllUsers();
            List<User> result = userLogic.GetAllUsers().ToList();

            mock.VerifyAll();
            Assert.AreEqual(result.Count, users.Count);
        }

        [TestMethod]
        public void UserIsInvalidTest()
        {
            var user = new User
            {
            };

            UserLogic userLogic = new UserLogic(null);
            bool result = userLogic.UserIsValid(user);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void MailIsValidTest()
        {
            var user = new User
            {
                Mail = "asd@asd.com"
            };

            UserLogic userLogic = new UserLogic(null);
            bool result = userLogic.HasValidEmail(user);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void MailIsInvalidTest()
        {
            var user = new User
            {
                Mail = "asdcom"
            };

            UserLogic userLogic = new UserLogic(null);
            bool result = userLogic.HasValidEmail(user);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsAdminTest()
        {
            var user = new User
            {
                Admin = true
            };

            var mock = new Mock<IRepository<User>>(MockBehavior.Strict);
            mock.Setup(m => m.Has(It.IsAny<User>())).Returns(true);

            IUserLogic userLogic = new UserLogic(mock.Object);
            bool result = userLogic.IsAdmin(user);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsNotAdminTest()
        {
            var user = new User();

            var mock = new Mock<IRepository<User>>(MockBehavior.Strict);
            mock.Setup(m => m.Has(It.IsAny<User>())).Returns(true);

            IUserLogic userLogic = new UserLogic(mock.Object);
            bool result = userLogic.IsAdmin(user);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(DoesNotExistsException))]
        public void UnexistingUserAdminTest()
        {
            User user = new User();

            var mock = new Mock<IRepository<User>>(MockBehavior.Strict);
            mock.Setup(m => m.Has(It.IsAny<User>())).Returns(false);

            IUserLogic userLogic = new UserLogic(mock.Object);
            bool result = userLogic.IsAdmin(user);

            Assert.IsFalse(result);
        }

        // [TestMethod]
        // public void AddIndicatorToUserTest()
        // {
        //     Indicator indicator = new Indicator
        //     {
        //         Name = "Test",
        //         Red = new BinaryOperator(),
        //         Yellow = new BinaryOperator(),
        //         Green = new BinaryOperator(),
        //     };

        //     var user = new User{Username="Test"};

        //     var mock = new Mock<IRepository<User>>(MockBehavior.Strict);
        //     mock.Setup(m => m.Update(It.IsAny<User>()));
        //     mock.Setup(m => m.Has(It.IsAny<User>())).Returns(true);
        //     mock.Setup(m => m.Save());

        //     IUserLogic userLogic = new UserLogic(mock.Object);
        //     userLogic.AddIndicator(user, indicator);
            
        //     Assert.IsTrue(userLogic.GetIndicatorsByUser(user).Count() == 1);
        // }

        [TestMethod]
        [ExpectedException(typeof(UserCantBeAdminException))]
        public void AddIndicatorToAdminTest()
        {
            Indicator indicator = new Indicator {  };
            var user = new User();
            user.Admin = true;

            var mock = new Mock<IRepository<User>>(MockBehavior.Strict);
            mock.Setup(m => m.Has(It.IsAny<User>())).Returns(true);

            IUserLogic userLogic = new UserLogic(mock.Object);
            userLogic.AddIndicator(user, indicator);

            mock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AdGetActionsBetweenDatesListNullDateTest()
        {
            DateTime lowerDate = System.Convert.ToDateTime("11/03/2013");
            DateTime higherDate = System.Convert.ToDateTime(null);

            var mock = new Mock<IRepository<User>>(MockBehavior.Strict);
            mock.Setup(m => m.Has(It.IsAny<User>())).Returns(true);

            IUserLogic userLogic = new UserLogic(mock.Object);
            userLogic.GetActionsBetweenDatesList(lowerDate, higherDate);

            mock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AdGetActionsBetweenDatesListInvalidDate()
        {
            DateTime lowerDate = System.Convert.ToDateTime("11/03/2013");
            DateTime higherDate = System.Convert.ToDateTime("11/03/2011");

            var mock = new Mock<IRepository<User>>(MockBehavior.Strict);
            mock.Setup(m => m.Has(It.IsAny<User>())).Returns(true);

            IUserLogic userLogic = new UserLogic(mock.Object);
            userLogic.GetActionsBetweenDatesList(lowerDate, higherDate);

            mock.VerifyAll();
        }                

        // [TestMethod]
        // [ExpectedException(typeof(AlreadyExistsException))]
        // public void AddExistingIndicatorToUserTest()
        // {
        //     var user = new User
        //     {
        //         Username = "Username",
        //     };
        //     var indicator = new Indicator
        //     {
        //         Name = "Indicator",
        //         Red = new BinaryOperator(),
        //         Yellow = new BinaryOperator(),
        //         Green = new BinaryOperator(),
        //     };
        //     List<Indicator> list = new List<Indicator>();
        //     list.Add(indicator);
        //     var mock = new Mock<UserLogic>(MockBehavior.Strict);
        //     mock.Setup(m => m.GetIndicatorsByUser(user).ToList()

        //     mock.VerifyAll();
        // }

        // [TestMethod]
        // public void HideIndicatorTest()
        // {
        //     var user = new User
        //     {
        //         Username = "Username",
        //     };
        //     var indicator = new Indicator
        //     {
        //         Name = "Indicator",
        //         Red = new BinaryOperator(),
        //         Yellow = new BinaryOperator(),
        //         Green = new BinaryOperator(),
        //     };
        //     user.AvailableIndicators.Append(indicator.ID);
        //     user.VisibleIndicators.Append(indicator.ID);

        //     var mock = new Mock<IRepository<User>>(MockBehavior.Strict);
        //     mock.Setup(m => m.Update(It.IsAny<User>()));
        //     mock.Setup(m => m.Save());


        //     IUserLogic userLogic = new UserLogic(mock.Object);
        //     userLogic.HideIndicator(user, indicator);

        //     Assert.IsTrue(user.HiddenIndicators.Count() == 1);
        // }

        // [TestMethod]
        // [ExpectedException(typeof(DoesNotExistsException))]
        // public void HideUnexistingIndicatorTest()
        // {
        //     Indicator indicator = new Indicator
        //     {
        //         Red = new BinaryOperator(),
        //         Yellow = new BinaryOperator(),
        //         Green = new BinaryOperator(),
        //     };
        //     var user = new User();

        //     var mock = new Mock<IRepository<UserIndicator>>(MockBehavior.Strict);
        //     mock.Setup(m => m.Update(It.IsAny<User>()));

        //     UIRepository ui = new UIRepository(mock.Object);
        //     userLogic.HideIndicator(user, indicator);

        //     Assert.IsTrue(user.HiddenIndicators.Count() == 1);
        // }

        // [TestMethod]
        // [ExpectedException(typeof(AlreadyExistsException))]
        // public void HideHiddenIndicatorTest()
        // {
        //     var user = new User
        //     {
        //         Username = "Username",
        //     };
        //     var indicator = new Indicator
        //     {
        //         Name = "Indicator",
        //         Red = new BinaryOperator(),
        //         Yellow = new BinaryOperator(),
        //         Green = new BinaryOperator(),
        //     };
        //     user.AvailableIndicators.Append(indicator.ID);
        //     user.HiddenIndicators.Append(indicator.ID);

        //     var mock = new Mock<IRepository<User>>(MockBehavior.Strict);

        //     IUserLogic userLogic = new UserLogic(mock.Object);
        //     userLogic.HideIndicator(user, indicator);

        //     Assert.IsTrue(user.HiddenIndicators.Count() == 1);
        // }

        // [TestMethod]
        // public void ShowIndicatorTest()
        // {
        //     var user = new User
        //     {
        //         Username = "Username",
        //     };
        //     var indicator = new Indicator
        //     {
        //         Name = "Indicator",
        //         Red = new BinaryOperator(),
        //         Yellow = new BinaryOperator(),
        //         Green = new BinaryOperator(),
        //     };
        //     user.AvailableIndicators.Append(indicator.ID);
        //     user.HiddenIndicators.Append(indicator.ID);

        //     var mock = new Mock<IRepository<User>>(MockBehavior.Strict);
        //     mock.Setup(m => m.Update(It.IsAny<User>()));
        //     mock.Setup(m => m.Save());

        //     IUserLogic userLogic = new UserLogic(mock.Object);
        //     userLogic.ShowIndicator(user, indicator);

        //     Assert.IsTrue(user.VisibleIndicators.Count() == 1);
        // }

        // [TestMethod]
        // [ExpectedException(typeof(DoesNotExistsException))]
        // public void ShowUnexistingIndicatorTest()
        // {
        //     var user = new User
        //     {
        //         Username = "Username",
        //     };
        //     var indicator = new Indicator
        //     {
        //         Name = "Indicator",
        //         Red = new BinaryOperator(),
        //         Yellow = new BinaryOperator(),
        //         Green = new BinaryOperator(),
        //     };

        //     var mock = new Mock<IRepository<User>>(MockBehavior.Strict);

        //     IUserLogic userLogic = new UserLogic(mock.Object);
        //     userLogic.ShowIndicator(user, indicator);

        //     Assert.IsTrue(user.VisibleIndicators.Count() == 1);
        // }

        // [TestMethod]
        // [ExpectedException(typeof(AlreadyExistsException))]
        // public void ShowVisibleIndicatorTest()
        // {
        //     var user = new User
        //     {
        //         Username = "Username",
        //     };
        //     var indicator = new Indicator
        //     {
        //         Name = "Indicator",
        //         Red = new BinaryOperator(),
        //         Yellow = new BinaryOperator(),
        //         Green = new BinaryOperator(),
        //     };
        //     user.AvailableIndicators.Append(indicator.ID);
        //     user.VisibleIndicators.Append(indicator.ID);

        //     var mock = new Mock<IRepository<User>>(MockBehavior.Strict);

        //     IUserLogic userLogic = new UserLogic(mock.Object);
        //     userLogic.ShowIndicator(user, indicator);

        //     Assert.IsTrue(user.VisibleIndicators.Count() == 1);
        // }


    }
}
