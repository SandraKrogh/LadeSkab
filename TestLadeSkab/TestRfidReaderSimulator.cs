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
        private RfidReaderSimulator _uut;
        private RfidDetectedEventArgs _receivedEventArgs;

        [SetUp]
        public void Setup()
        {
            /*
            _heater = Substitute.For<IHeater>();
            _tempsensor = Substitute.For<ITempSensor>();
            _logger = Substitute.For<ILogger>();

            uut = new ECS(1, _heater, _tempsensor, _logger);
            */

            _receivedEventArgs = null;

            _uut = new RfidReaderSimulator();

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

        [Test]
        public void SetCurrentId_IdSetToNewValue_CorrectNewIdReceived()
        {
            _uut.SimulateDetected(5);
            Assert.That(_receivedEventArgs.id, Is.EqualTo(5));
        }

        /*
        [TestCase(7, 1)]
        [TestCase(1, 0)]
        public void TestRegulateIfStatement(int threshold, int result)
        {
            uut.SetThreshold(threshold);
            uut.Regulate();
            _heater.Received(1).TurnOn();
        }
        */
    }
}
