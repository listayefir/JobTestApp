using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestApplication.Abstract;
using TestApplication.Models;
using System.Linq;
using Moq;

namespace TestApplication.Tests
{
    [TestClass]
    public class UnitTest1
    {
        ILannisterRepository repo;
        Mock<ILannisterRepository> mock;

        public void Initialize()
        {
            mock = new Mock<ILannisterRepository>();
            mock.Setup(m => m.Lannisters).Returns(new Lannister[]{
                new Lannister { Id=0, Title="A", Created=new DateTime(1995,1,1)},
                new Lannister { Id=0, Title="B", Created=new DateTime(1990,1,1)},
                new Lannister { Id=0, Title="C", Created=new DateTime(1999,1,1)},
                new Lannister { Id=0, Title="D", Created=new DateTime(2000,1,1)},
            });
        }
        [TestMethod]
        public void DateTimeTest()
        {
            Initialize();
            var from = "11/11/1990";
            var to = "31/12/1999";
            var a = DateTime.Parse(from);
            var b = DateTime.Parse(to);
            var result = mock.Object.Lannisters.Where(x => x.Created.CompareTo(DateTime.Parse(from)) >= 0 && x.Created.CompareTo(DateTime.Parse(to)) <= 0).ToArray();

            Assert.AreEqual(2, result.Length);
            Assert.AreEqual("A", result[0].Title);
            Assert.AreEqual("C", result[1].Title);
            
        }
    }
}
