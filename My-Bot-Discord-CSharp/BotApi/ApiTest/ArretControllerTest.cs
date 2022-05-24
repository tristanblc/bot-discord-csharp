using ApiApplication;
using ApiApplication.Controllers;
using AutoFixture;
using AutoMapper;
using BusClassLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReaderClassLibrary.Reader;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net.Http;

namespace BotApi
{
    [TestClass]
    public class ArretControllerTest
    {
        private Fixture Fixture { get; set; }


        private IEnumerable<Arret> Arrets { get; set; }

        private IMapper Mapper { get; init; } 

        private ApplicationDbContext Context { get; init; }



        private GenericApiReader<Arret> ArretApiReader { get;set; }


        private readonly string url = "https://localhost:7167/api/Arret";


        public ArretControllerTest()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddMaps(typeof(ArretController)));
            Mapper = new Mapper(configuration);
            HttpClient httpClient = new HttpClient();
            Context = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());

            ArretApiReader = new GenericApiReader<Arret>(httpClient, url);

     
        }

        public void InitTest()
        {
            Fixture = new Fixture();
            Arrets = Fixture.CreateMany<Arret>();     
     
        }


        [TestMethod]
        public void TestGet()
        {

            var resultat = ArretApiReader.GetAll();
            var mapped = Mapper.Map<List<Arret>>(resultat);
            var count_mapped = mapped.Count  ==  1;
            Assert.IsTrue(count_mapped);


        }


        [TestMethod]
        public void TestGetAll()
        {
            var resultat = ArretApiReader.GetAll();
            var mapped = Mapper.Map<List<Arret>>(resultat);
            var count_mapped = mapped.Count > 0;
            Assert.IsTrue(count_mapped);
          
        }



        [TestMethod]
        public void TestPost()
        {
            Arret arret = new Arret("stop", 40.21f, 40.3f, "france");


            var resultat = ArretApiReader.Add(arret);


            Assert.IsNotNull(resultat);

            Assert.AreEqual(resultat.Status, 200);



        }


        [TestMethod]
        public void TestPut()
        {

            Arret arret = new Arret("stop", 40.21f, 40.3f, "france");


            var resultat = ArretApiReader.Update(arret);


            Assert.IsNotNull(resultat);

            Assert.AreEqual(resultat.Status, 200);


        }


        [TestMethod]
        public void TestDelete()
        {
            Arret arret = new Arret("stop", 40.21f, 40.3f, "france");

            var resultat = ArretApiReader.Delete(arret);


            Assert.IsNotNull(resultat);

            Assert.AreEqual(resultat.Status, 200);


        }
    }
}