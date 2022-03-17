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
    public class TestDoor
    {
        private Door _uut;
        private DoorStateEventArg _receivedEventArgs;

        [SetUp]
        public void Setup()
        {

            _receivedEventArgs = null;

            _uut = new Door();

            // Set up event listener
            _uut.DoorStateChanged += (obj, args) =>
            {
                _receivedEventArgs = args;
            };
        }

        [Test]
        public void SetCurrentId_StateSetToFalse_EventFired()
        {
            _uut.DoorOpen();
            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        [Test]
        public void SetCurrentId_StateSetToFalse_CorrectNewIdReceived()
        {
            _uut.DoorOpen();
            Assert.That(_receivedEventArgs.State, Is.EqualTo(false));
        }

        [Test]
        public void SetCurrentId_StateSetToTrue_EventFired()
        {
            _uut.DoorClosed();
            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        [Test]
        public void SetCurrentId_StateSetToTrue_CorrectNewIdReceived()
        {
            _uut.DoorClosed();
            Assert.That(_receivedEventArgs.State, Is.EqualTo(true));
        }
    }
}
