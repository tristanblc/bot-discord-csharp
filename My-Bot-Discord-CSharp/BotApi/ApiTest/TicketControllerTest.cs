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
    public class TicketControllerTest
    {
        private readonly HttpClient HttpClient = new HttpClient();

        private readonly string Url;
        private TicketService TicketService { get; set; }
        public TicketControllerTest()
        {

            var builder = new ConfigurationBuilder()
                                .AddJsonFile("app.json", optional: false, reloadOnChange: true)
                                .AddEnvironmentVariables();

            IConfiguration config = builder.Build();

            Url = config.GetSection("apiUrl").Value.ToString() + "/Ticket";

            TicketService = new TicketService(HttpClient,Url);
                
        }

        [TestMethod]
        public void GetAllTicketsTest()
        {
            Guid id = new Guid();
            var result = TicketService.GetAll();

            Assert.IsNotNull(result);
            var entities = result as IEnumerable<Ticket>;
            Assert.AreEqual(entities.Count(), 2);

        }

        [TestMethod]
        public void GetTicketTest()
        {
            Guid id = new Guid();
            var result =TicketService.Get(id).Result;



            Assert.IsNotNull(result);
            var entities = result as IEnumerable<Ticket>;
            Assert.AreEqual(entities.Count(), 2);


        }


        [TestMethod]
        public void AddTicketTest()
        {

            Guid id = new Guid();
            var result = TicketService.Get(id).Result;


            Assert.IsNotNull(result);
            var entities = result as IEnumerable<Ticket>;
            Assert.AreEqual(entities.Count(), 0);
        }


        [TestMethod]
        public void UpdateTicketTest()
        {


            Guid id = new Guid();
            var result = TicketService.Get(id).Result;

            var sent = TicketService.Update(result);

            //Assert

            Assert.IsNotNull(result);
            Assert.AreEqual(result, 200);
        }


        [TestMethod]
        public void DeleteTicketMethod()
        {
            Guid id = new Guid();

            var result = TicketService.Delete(id).Result;

            Assert.IsNotNull(result);

            Assert.AreEqual(result, 200);


        }
    }
}
