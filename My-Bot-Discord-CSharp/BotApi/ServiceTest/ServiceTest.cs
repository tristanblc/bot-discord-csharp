using BusClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReaderClassLibrary.Services;
using System.Collections.Generic;
using System.Net.Http;

namespace BotApi
{
    [TestClass]
    public class ServiceTest
    {

        private ArretService arretService { get; set; } 


        public ServiceTest()
        {
            HttpClient http = new HttpClient();


            arretService = new ArretService(http, "https://localhost:7167/api/Arret/");



        }



        [TestMethod]
        public void TestGetAll()
        {
            var resultat = arretService.GetAll();   

            Assert.IsNotNull(resultat);
            Assert.IsInstanceOfType(resultat, typeof(IEnumerable<Arret>));


           
        }

        [TestMethod]
        public void TestGet()
        {
            var resultat = arretService.Get();

            Assert.IsNotNull(resultat);
            Assert.IsInstanceOfType(resultat, typeof(Arret));

        }
    }
}