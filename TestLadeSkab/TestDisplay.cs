using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using LadeSkab.Interfaces;
using LadeSkab;
using System.IO;

namespace TestLadeSkab
{
    [TestFixture]
    public class TestDisplay
    {
        private IDisplay _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new Display();
        }

        [TestCase("Tester\r\n")]
        [TestCase("Tester2\r\n")]
        public void WriteLineToConsol(string testString)
        {
            _uut.WriteLine(testString);

            Assert.AreEqual(_uut.LogResult, testString);
        }


    }
}

