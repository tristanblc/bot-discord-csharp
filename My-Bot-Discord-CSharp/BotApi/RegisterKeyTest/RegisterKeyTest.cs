using BotClassLibrary.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReaderClassLibrary.Services;
using System.Collections.Generic;
using System.Net.Http;

namespace BotApi.RegisterKeyTest
{
    [TestClass]
    public class RegisterKeyTest
    {

        private RegisterKeyBotClass  registerBotKey { get; init; }
        public RegisterKeyTest()
        {
            registerBotKey = new RegisterKeyBotClass();
        }



        [TestMethod]
        public void CreateKeyTest()
        {

            var resultat = registerBotKey.CreateRegisterKey();

            Assert.IsNotNull(resultat);
            Assert.IsInstanceOfType(resultat, typeof(ActionResult));
            Assert.AreEqual(resultat, 200);

        }


        [TestMethod]
        public void DestructKeyTest()
        {
            var resultat = registerBotKey.DestructRegisterKey();

            Assert.IsNotNull(resultat);
            Assert.IsInstanceOfType(resultat, typeof(ActionResult));
            Assert.AreEqual(resultat, 200);

        }

        [TestMethod]
        public void ExistsKeyTest()
        {
            var resultat = registerBotKey.CreateRegisterKey();

            Assert.IsNotNull(resultat);
            Assert.IsInstanceOfType(resultat, typeof(bool));
            Assert.AreEqual(resultat, true);

        }

    }
}