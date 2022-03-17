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

        [SetUp]
        public void Setup()
        {
            _tempRfidReader = Substitute.For<IRfidReader>();
            _tempDoor = Substitute.For<IDoor>();
            
            uut = new StationControl(_tempDoor, _tempRfidReader);
        }

      
        public void HandleDoorStateChanged_CurrentStateFalse()
        {
            var stringwriter = new StringWriter();
            Console.SetOut(stringwriter);

            _tempDoor.DoorStateChanged += Raise.EventWith(new DoorStateEventArg {State = false});
            Assert.AreEqual("Tilslut Telefon", stringwriter.ToString());
        }

        public void HandleDoorStateChanged_CurrentStateTrue()
        {
            var stringwriter = new StringWriter();
            Console.SetOut(stringwriter);

            _tempDoor.DoorStateChanged += Raise.EventWith(new DoorStateEventArg { State = true });
            Assert.AreEqual("Indlæs RFID", stringwriter.ToString());
        }

        public void HandleRfidReaderdetected_LadeSkabStateAvailable()
        {
            uut._state = LadeskabState.Available;
        }

        public void HandleRfidReaderdetected_LadeSkabStateLocked()
        {

        }

        public void HandleRfidReaderdetected_LadeSkabStateDoorOpen()
        {

        }
    }
}
