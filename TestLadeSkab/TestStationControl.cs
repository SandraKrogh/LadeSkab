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
        private IChargeControl _tempChargerControl;
        private IDisplay _tempDisplay;

        [SetUp]
        public void Setup()
        {
            _tempRfidReader = Substitute.For<IRfidReader>();
            _tempDoor = Substitute.For<IDoor>();
            _tempCharger = Substitute.For<IUsbCharger>();
            //_tempDisplay = Substitute.For<IDisplay>();
            _tempChargerControl = Substitute.For<IChargeControl>();

            uut = new StationControl(_tempDoor, _tempRfidReader,_tempChargerControl);
        }

        [Test]
        public void HandleDoorStateChanged_CurrentStateFalse()
        {
            _tempDoor.DoorStateChanged += Raise.EventWith(new DoorStateEventArg {State = false});
            Assert.That(uut._myDisplay.LogResult, Is.EqualTo("Tilslut Telefon")); //SPØRG

        }

        [Test]
        public void HandleDoorStateChanged_CurrentStateTrue()
        {
            _tempDoor.DoorStateChanged += Raise.EventWith(new DoorStateEventArg { State = true });
            Assert.That(uut._myDisplay.LogResult, Is.EqualTo("Indlæs RFID")); //SPØRG
           
        }

        //Den er nul da der ikke er noget subscribers, hvordan løse man det 
        [Test]
        public void HandleRfidReaderdetected_LadeSkabStateAvailable_ChargerConnected()
        {
            _tempChargerControl.
            
            _tempRfidReader.RfidDetected += Raise.EventWith(new RfidDetectedEventArgs {id = 5});

            Assert.That(uut._myDisplay.LogResult, Is.EqualTo("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.")); //SPØRG
        }

        [Test]
        public void HandleRfidReaderdetected_LadeSkabStateAvailable_ChargerNotConnected()
        {
            //Charger connected
            _tempCharger.Connected = false;

            _tempRfidReader.RfidDetected += Raise.EventWith(new RfidDetectedEventArgs { id = 5 });

            Assert.That(uut._myDisplay.LogResult, Is.EqualTo("Din telefon er ikke ordentlig tilsluttet. Prøv igen.")); //SPØRG
        }

        public void HandleRfidReaderdetected_LadeSkabStateLocked()
        {

        }

        public void HandleRfidReaderdetected_LadeSkabStateDoorOpen()
        {

        }
    }
}
