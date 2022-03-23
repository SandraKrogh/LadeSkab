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
            _tempDisplay = Substitute.For<IDisplay>();
            _tempChargerControl = Substitute.For<IChargeControl>();

            uut = new StationControl(_tempDoor, _tempRfidReader,_tempChargerControl,_tempDisplay);
        }

        [Test]
        public void HandleDoorStateChanged_CurrentStateFalse()
        {
            _tempDoor.DoorStateChanged += Raise.EventWith(new DoorStateEventArg {State = false});
            _tempDisplay.Received(1).WriteLine("Tilslut Telefon");


        }

        [Test]
        public void HandleDoorStateChanged_CurrentStateTrue()
        {
            _tempDoor.DoorStateChanged += Raise.EventWith(new DoorStateEventArg { State = true });
            _tempDisplay.Received(1).WriteLine("Indlæs RFID");           
        }

        
        [TestCase(true, "Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.")]
        [TestCase(false, "Din telefon er ikke ordentlig tilsluttet. Prøv igen.")]
        public void HandleRfidReaderdetected_LadeSkabStateAvailable(bool connected, string result)
        {
            //_tempCharger.Connected = connected;
            _tempChargerControl.IsConnected().Returns(connected);

            _tempRfidReader.RfidDetected += Raise.EventWith(new RfidDetectedEventArgs {id = 5});
            _tempDisplay.Received(1).WriteLine(result);
        }

        
        [TestCase(5,5, "Tag din telefon ud af skabet og luk døren")]
        [TestCase(5,6, "Forkert RFID tag")]
        public void HandleRfidReaderdetected_LadeSkabStateLocked(int id, int oldid, string result)
        {
            _tempChargerControl.IsConnected().Returns(true);

            _tempRfidReader.RfidDetected += Raise.EventWith(new RfidDetectedEventArgs { id = id });
            _tempRfidReader.RfidDetected += Raise.EventWith(new RfidDetectedEventArgs { id = oldid });
            _tempDisplay.Received(1).WriteLine(result);
        }

    }
}
