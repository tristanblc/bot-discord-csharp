using ApiApplication;
using ApiApplication.Controllers;
using AutoFixture;
using AutoMapper;
using BusClassLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReaderClassLibrary.Reader;
using System.Collections.Generic;
using System.Net.Http;

namespace BotApi
{
    [TestClass]
    public class ReaderApiTest
    {

        private Fixture Fixture { get; set; }

        private IEnumerable<Trip> Lignes { get; set; }

        private IMapper Mapper { get; init; }

        private ApplicationDbContext Context { get; init; }

        private GenericApiReader<Trip> TripApiReader { get; set; }


        private readonly string url = "https://localhost:7167/api/Trip";


        public ReaderApiTest()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddMaps(typeof(TripController)));
            Mapper = new Mapper(configuration);
            HttpClient httpClient1 = new HttpClient();
            HttpClient httpClient = httpClient1;
            Context = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());

            TripApiReader = new GenericApiReader<Trip>(httpClient, url);


        }

        public void InitTest()
        {
            Fixture = new Fixture();
            Lignes = Fixture.CreateMany<Trip>();

        }


        [TestMethod]
        public void TestGet()
        {

            var resultat = TripApiReader.GetAll();
            var mapped = Mapper.Map<List<Trip>>(resultat);
            var count_mapped = mapped.Count == 1;
            Assert.IsTrue(count_mapped);


        }


        [TestMethod]
        public void TestGetAll()
        {
            var resultat = TripApiReader.GetAll();
            var mapped = Mapper.Map<List<Trip>>(resultat);
            var count_mapped = mapped.Count > 0;
            Assert.IsTrue(count_mapped);

        }



        [TestMethod]
        public void TestPost()
        {


            Trip trip = new Trip(new System.Guid(), "hedsign", 1, new System.Guid());

            var resultat = TripApiReader.Add(trip);


            Assert.IsNotNull(resultat);

            Assert.AreEqual(resultat.Status, 200);



        }


        [TestMethod]
        public void TestPut()
        {


            Trip trip = new Trip(new System.Guid(), "hedsign", 1, new System.Guid());

            var resultat = TripApiReader.Update(trip);


            Assert.IsNotNull(resultat);

            Assert.AreEqual(resultat.Status, 200);


        }


        [TestMethod]
        public void TestDelete()
        {

            Trip trip = new Trip(new System.Guid(), "hedsign", 1, new System.Guid());

            var resultat = TripApiReader.Delete(trip);


            Assert.IsNotNull(resultat);

            Assert.AreEqual(resultat.Status, 200);


        }
    }
}