using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SunnyHomeCare;
using SunnyHomeCare.Controllers;

namespace SunnyHomeCare.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Login()
        {
            // Arrange
            LoginController controller = new LoginController();

            // Act
            ViewResult result = controller.Login() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
/*
        [TestMethod]
        public void Logout()
        {
            // Arrange
            LoginController controller = new LoginController();

            // Act
            ViewResult result = controller.Logout(user) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }*/
    }
}
