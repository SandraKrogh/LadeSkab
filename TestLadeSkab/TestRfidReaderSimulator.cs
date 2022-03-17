using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using LadeSkab;
using LadeSkab.Interfaces;

namespace TestLadeSkab
{
    [TestFixture]
    public class TestRfidReaderSimulator
    {
        private RfidReader _uut;
        private RfidDetectedEventArgs _receivedEventArgs;

        [SetUp]
        public void Setup()
        {
            _receivedEventArgs = null;

            _uut = new RfidReader();

            // Set up event listener
            _uut.RfidDetected += (obj, args) =>
            {
                _receivedEventArgs = args;
            };
        }

        [Test]
        public void SetCurrentId_IdSetToNewValue_EventFired()
        {
            _uut.SimulateDetected(5);
            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        [TestCase(5,5)]
        [TestCase(10, 10)]
        public void SetCurrentId_IdSetToNewValue_CorrectNewIdReceived(int id, int result)
        {
            _uut.SimulateDetected(id);
            Assert.That(_receivedEventArgs.id, Is.EqualTo(result));
        }
    }
}
