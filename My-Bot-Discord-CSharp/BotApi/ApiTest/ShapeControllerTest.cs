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
    public class ShapeControllerTest
    {
        private Fixture Fixture { get; set; }


        private IEnumerable<Shape> Lignes { get; set; }

        private IMapper Mapper { get; init; }

        private ApplicationDbContext Context { get; init; }



        private GenericApiReader<Shape> ShapeApiReader { get; set; }


        private readonly string url = "https://localhost:7167/api/Ligne";


        public ShapeControllerTest()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddMaps(typeof(ShapeController)));
            Mapper = new Mapper(configuration);
            HttpClient httpClient1 = new HttpClient();
            HttpClient httpClient = httpClient1;
            Context = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());

            ShapeApiReader = new GenericApiReader<Shape>(httpClient, url);


        }

        public void InitTest()
        {
            Fixture = new Fixture();
            Lignes = Fixture.CreateMany<Shape>();

        }


        [TestMethod]
        public void TestGet()
        {

            var resultat = ShapeApiReader.GetAll();
            var mapped = Mapper.Map<List<Shape>>(resultat);
            var count_mapped = mapped.Count == 1;
            Assert.IsTrue(count_mapped);


        }


        [TestMethod]
        public void TestGetAll()
        {
            var resultat =  ShapeApiReader.GetAll();
            var mapped = Mapper.Map<List<Shape>>(resultat);
            var count_mapped = mapped.Count > 0;
            Assert.IsTrue(count_mapped);

        }



        [TestMethod]
        public void TestPost()
        {
            

            Shape shape = new Shape(1.0f, 1.0f, 1);

            var resultat = ShapeApiReader.Add(shape);


            Assert.IsNotNull(resultat);

            Assert.AreEqual(resultat.Status, 200);



        }


        [TestMethod]
        public void TestPut()
        {


            Shape shape = new Shape(1.0f, 1.0f, 1);
            var resultat = ShapeApiReader.Update(shape);


            Assert.IsNotNull(resultat);

            Assert.AreEqual(resultat.Status, 200);


        }


        [TestMethod]
        public void TestDelete()
        {
          

            Shape shape = new Shape(1.0f, 1.0f, 1);

            var resultat = ShapeApiReader.Delete(shape);


            Assert.IsNotNull(resultat);

            Assert.AreEqual(resultat.Status, 200);


        }
    }
}