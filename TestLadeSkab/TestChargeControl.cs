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
        private IDisplay _display;

        [SetUp]
        public void Setup()
        {
            _usbCharger = Substitute.For<IUsbCharger>();
            _display = Substitute.For<IDisplay>();

            _uut = new ChargeControl(_usbCharger, _display);

        }

        [TestCase(5,5)]
        [TestCase(250, 250)]
        public void CurrentChanged_CurrentCurrentIsCorrect(int current, int result)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current });
            Assert.That(_uut.CurrentCurrent, Is.EqualTo(result));
        }

        [TestCase(1, "Telefonen er fuldt opladet")]
        [TestCase(3, "Telefonen er fuldt opladet")]
        [TestCase(5, "Telefonen er fuldt opladet")]
        [TestCase(6, "Telefonen lader")]
        [TestCase(250, "Telefonen lader")]
        [TestCase(500, "Telefonen lader")]
        [TestCase(501, "Fejl - Der er noget galt!")]
        [TestCase(600, "Fejl - Der er noget galt!")]
        public void CurrentChanged_CorrectOutput(int current, string result)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current });

            _display.Received(1).WriteLine(result);
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
        [TestCase(true)]
        [TestCase(false)]
        public void IsConnected_CorrectValue_ReturnedFromUsbCharger(bool state)
        {
            _usbCharger.Connected = state;

            Assert.AreEqual(state, _uut.IsConnected());
        }

    }
}

