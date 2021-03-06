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
            _uut.SimulateDoorOpen();
            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        [Test]
        public void SetCurrentId_StateSetToFalse_CorrectNewIdReceived()
        {
            _uut.SimulateDoorOpen();
            Assert.That(_receivedEventArgs.State, Is.EqualTo(false));
        }

        [Test]
        public void SetCurrentId_StateSetToTrue_EventFired()
        {
            _uut.SimulateDoorClosed();
            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        [Test]
        public void SetCurrentId_StateSetToTrue_CorrectNewIdReceived()
        {
            _uut.SimulateDoorClosed();
            Assert.That(_receivedEventArgs.State, Is.EqualTo(true));
        }

        [Test]
        public void ctor_CurrentState()
        {
            Assert.That(_uut.CurrentState, Is.False);
        }

        [Test]
        public void ctor_Locked()
        {
            Assert.That(_uut.locked, Is.False);
        }

        [Test]
        public void lockDoor_LockStateChanges()
        {
            _uut.LockDoor();
            Assert.That(_uut.locked, Is.True);
        }

        [Test]
        public void unlockDoor_LockStateChanges()
        {
            _uut.UnlockDoor();
            Assert.That(_uut.locked, Is.False);
        }
}
}
