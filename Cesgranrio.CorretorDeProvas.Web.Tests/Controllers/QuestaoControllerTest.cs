using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cesgranrio.CorretorDeProvas.Web;
using Cesgranrio.CorretorDeProvas.Web.Controllers;

namespace Cesgranrio.CorretorDeProvas.Web.Tests.Controllers
{
    [TestClass]
    public class QuestaoControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            QuestaoController controller = new QuestaoController();

            // Act
            ViewResult result = controller.Lista() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        //[TestMethod]
        //public void About()
        //{
        //    // Arrange
        //    QuestaoController controller = new QuestaoController();

        //    // Act
        //    ViewResult result = controller.About() as ViewResult;

        //    // Assert
        //    Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        //}

        //[TestMethod]
        //public void Contact()
        //{
        //    // Arrange
        //    QuestaoController controller = new QuestaoController();

        //    // Act
        //    ViewResult result = controller.Contact() as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(result);
        //}
    }
}
