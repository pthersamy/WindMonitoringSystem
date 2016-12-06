using System;
using System.Collections.Specialized;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindMonitoringSystem.Controllers;
using WindMonitoringSystem.Models;
using WindMonitoringSystem.Tests.Mock;

namespace WindMonitoringSystem.Tests
{
    [TestClass]
    public class WindMonitoringTest
    {
        
        [TestMethod]
        public void IndexTest()
        {
            var controller = new WindMonitoringController();
            var result = controller.Index();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void GetInitialDataTest()
        {
            var controller = new WindMonitoringController();
            var result = controller.GetInitialData() as JsonResult;
            Assert.IsInstanceOfType(result, typeof(JsonResult));
            Assert.AreEqual(4, ((WindMonitoring)(result.Data)).StateList.Count);
        }

        [TestMethod]
        public void GetPredictedSpeedTest()
        {
            var controller = new WindMonitoringController();
            var result = controller.GetPredictedSpeed("KA-BA-01") as JsonResult;
            Assert.IsInstanceOfType(result, typeof(JsonResult));
            Assert.AreEqual(1, ((int)(result.Data)));
        }
    }
}
