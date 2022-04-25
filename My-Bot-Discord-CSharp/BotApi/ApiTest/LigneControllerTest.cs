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
    public class LigneControllerTest
    {
        private Fixture Fixture { get; set; }


        private IEnumerable<Ligne> Lignes { get; set; }

        private IMapper Mapper { get; init; }

        private ApplicationDbContext Context { get; init; }



        private GenericApiReader<Ligne> LigneApiReader { get; set; }


        private readonly string url = "https://localhost:7167/api/Ligne";


        public LigneControllerTest()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddMaps(typeof(LigneController)));
            Mapper = new Mapper(configuration);
            HttpClient httpClient = new HttpClient();
            Context = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());

            LigneApiReader = new GenericApiReader<Ligne>(httpClient, url);


        }

        public void InitTest()
        {
            Fixture = new Fixture();
            Lignes = Fixture.CreateMany<Ligne>();

        }


        [TestMethod]
        public void TestGet()
        {

            var resultat = LigneApiReader.GetAll();
            var mapped = Mapper.Map<List<Ligne>>(resultat);
            var count_mapped = mapped.Count == 1;
            Assert.IsTrue(count_mapped);


        }


        [TestMethod]
        public void TestGetAll()
        {
            var resultat = LigneApiReader.GetAll();
            var mapped = Mapper.Map<List<Ligne>>(resultat);
            var count_mapped = mapped.Count > 0;
            Assert.IsTrue(count_mapped);

        }



        [TestMethod]
        public void TestPost()
        {
            Ligne ligne = new Ligne("t2c", "desc", "type", "color");


            var resultat = LigneApiReader.Add(ligne);


            Assert.IsNotNull(resultat);

            Assert.AreEqual(resultat.Status, 200);



        }


        [TestMethod]
        public void TestPut()
        {

            Ligne ligne = new Ligne("t2c", "desc", "type", "color");
            var resultat = LigneApiReader.Update(ligne);


            Assert.IsNotNull(resultat);

            Assert.AreEqual(resultat.Status, 200);


        }


        [TestMethod]
        public void TestDelete()
        {
            Ligne ligne = new Ligne("t2c", "desc", "type", "color");

            var resultat = LigneApiReader.Delete(ligne);


            Assert.IsNotNull(resultat);

            Assert.AreEqual(resultat.Status, 200);


        }
    }
}