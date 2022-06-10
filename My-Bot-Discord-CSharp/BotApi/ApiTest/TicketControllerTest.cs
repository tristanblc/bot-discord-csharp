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


        }

        [TestMethod]
        public void GetTicketTest()
        {



        }


        [TestMethod]
        public void AddTicketTest()
        {

        }


        [TestMethod]
        public void UpdateTicketTest()
        {


        }


        [TestMethod]
        public void DeleteTicketMethod()
        {

        }
    }
}
