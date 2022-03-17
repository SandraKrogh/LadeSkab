using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using LadeSkab;
using LadeSkab.Interfaces;
using System.IO;

namespace TestLadeSkab
{
    [TestFixture]
    public class TestStationControl
    {
        private StationControl uut;
        private IRfidReader _tempRfidReader;
        private IDoor _tempDoor;
        private IUsbCharger _tempCharger;
        private IDisplay _tempDisplay;

        [SetUp]
        public void Setup()
        {
            _tempRfidReader = Substitute.For<IRfidReader>();
            _tempDoor = Substitute.For<IDoor>();
            _tempCharger = Substitute.For<IUsbCharger>();
            _tempDisplay = Substitute.For<IDisplay>();

            uut = new StationControl(_tempDoor, _tempRfidReader);
        }

        [Test]
        public void HandleDoorStateChanged_CurrentStateFalse()
        {
            _tempDoor.DoorStateChanged += Raise.EventWith(new DoorStateEventArg {State = false});
            _tempDisplay.WriteLine("Tilslut Telefon");
       
        }

        [Test]
        public void HandleDoorStateChanged_CurrentStateTrue()
        {
            _tempDoor.DoorStateChanged += Raise.EventWith(new DoorStateEventArg { State = true });
            _tempDisplay.WriteLine("Indlæs FID");
           
        }

        [Test]
        public void HandleRfidReaderdetected_LadeSkabStateAvailable()
        {
           //Charger connected
            _tempCharger.Connected = true;
            
            _tempRfidReader.RfidDetected += Raise.EventWith(new RfidDetectedEventArgs {id = 5});
            Assert.That(uut._oldId, Is.EqualTo(5));

            //_tempDisplay.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
        }

        public void HandleRfidReaderdetected_LadeSkabStateLocked()
        {

        }

        public void HandleRfidReaderdetected_LadeSkabStateDoorOpen()
        {

        }
    }
}
