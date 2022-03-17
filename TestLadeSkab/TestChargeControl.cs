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
    public class TestChargeControl
    {

        private IChargeControl _uut;
        private IUsbCharger _usbCharger;

        [SetUp]
        public void Setup()
        {
            _usbCharger = Substitute.For<IUsbCharger>();

            _uut = new ChargeControl(_usbCharger);

        }

        [TestCase(5,5)]
        [TestCase(250, 250)]
        public void CurrentChanged_CurrentCurrentIsCorrect(int current, int result)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current });
            Assert.That(_uut.CurrentCurrent, Is.EqualTo(result));
        }

        [TestCase(1, "Telefonen er fuldt opladet\r\n")]
        [TestCase(3, "Telefonen er fuldt opladet\r\n")]
        [TestCase(5, "Telefonen er fuldt opladet\r\n")]
        [TestCase(6, "Telefonen lader\r\n")]
        [TestCase(250, "Telefonen lader\r\n")]
        [TestCase(500, "Telefonen lader\r\n")]
        [TestCase(501, "Fejl - Der er noget galt!\r\n")]
        [TestCase(600, "Fejl - Der er noget galt!\r\n")]
        public void CurrentChanged_CorrectOutput(int current, string result)
        {
            var stringwriter = new StringWriter();
            Console.SetOut(stringwriter);

            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current });

            Assert.AreEqual(result, stringwriter.ToString());
        }

        [Test]
        public void StartCharge_CorrectCallToUsbCharger()
        {
            _uut.StartCharge();
            _usbCharger.Received(1).StartCharge();
        }
        [Test]
        public void StopCharge_CorrectCallToUsbCharger()
        {
            _uut.StopCharge();
            _usbCharger.Received(1).StopCharge();
        }
        [Test]
        public void IsConnected_ValueTrue_ReturnedFromUsbCharger()
        {
            _uut.IsConnected();

            Assert.AreEqual(true, _usbCharger.Connected);
        }

    }
}

