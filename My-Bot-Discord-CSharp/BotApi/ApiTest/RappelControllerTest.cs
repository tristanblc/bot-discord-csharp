using BotClassLibrary;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReaderClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BotApi.ApiTest
{
    [TestClass]
    public class RappelControllerTest
    {
        private readonly HttpClient HttpClient = new HttpClient();

        private readonly string Url;
        private RappelService RappelService { get; set; }
        public RappelControllerTest()
        {

            var builder = new ConfigurationBuilder()
                                .AddJsonFile("app.json", optional: false, reloadOnChange: true)
                                .AddEnvironmentVariables();

            IConfiguration config = builder.Build();

            Url = config.GetSection("apiUrl").Value.ToString() + "/Rappel";

            RappelService = new RappelService(HttpClient, Url);

        }

        [TestMethod]
        public void GetAllRapplTest()
        {
           
            Guid id = new Guid();
            var result = RappelService.GetAll();

            Assert.IsNotNull(result);
            var entities = result as IEnumerable<Rappel>;
            Assert.AreEqual(entities.Count(), 2);


        }

        [TestMethod]
        public void GetRappelTest()
        {

           

           
            Guid id = new Guid();
            var result = RappelService.Get(id).Result;

            

            Assert.IsNotNull(result);
            var entities = result as IEnumerable<Rappel>;
            Assert.AreEqual(entities.Count(), 2);


        }


        [TestMethod]
        public void AddRappelTest()
        {

          
            Guid id = new Guid();
            var result = RappelService.Get(id).Result;


            Assert.IsNotNull(result);
            var entities = result as IEnumerable<Rappel>;
            Assert.AreEqual(entities.Count(), 0);
        }


        [TestMethod]
        public void UpdateRappelTest()
        {

           
            Guid id = new Guid();
            var result = RappelService.Get(id).Result ;
          
            var sent = RappelService.Update(result);

            //Assert

            Assert.IsNotNull(result);
            Assert.AreEqual(result),200);

        }


        [TestMethod]
        public void DeleteRappelMethod()
        {
            Guid id = new Guid();
       
            var result = RappelService.Delete(id).Result;

      

            Assert.IsNotNull(result);

            Assert.AreEqual(result, 200);

        }

    }
}
