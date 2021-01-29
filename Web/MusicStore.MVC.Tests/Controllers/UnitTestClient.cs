using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicStore.Data.Service.Facade;
using MusicStore.MVC.Controllers;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MusicStore.MVC.Tests.Controllers
{
    [TestClass]
    public class UnitTestClient
    {
        private ClientController dbControllerClient = new ClientController();
        private ClientFacade dbModel = new ClientFacade();

        [TestMethod]
        public void TestIndex()
        {
            var oDataClient = dbControllerClient.Index() as ViewResult;
            Assert.IsNotNull(oDataClient);
        }

        [TestMethod]
        public void TestRegistrarClient()
        {
            string idValue = "123";
            string Name = "Unit User";
            string Mail = "UnitUser@hotmail.com";
            string Direction = "Cll 10";
            string Phone = "3003198765";
            var result = dbControllerClient.RegistrarClient(idValue, Name, Mail, Direction, Phone, string.Empty) as JsonResult;

            //assert     
            Assert.AreEqual("exist", (string)result.Data);
        }

        [TestMethod]
        public void TestEditarClient()
        {
            string idValue = "123";
            string Name = "Unit User";
            string Mail = "UnitUser@hotmail.com";
            string Direction = "Cll 10";
            string Phone = "3003198765";
            var result = dbControllerClient.EditarClient(idValue, Name, Mail, Direction, Phone) as JsonResult;

            //assert     
            Assert.AreEqual("save", (string)result.Data);
        }
    }
}
