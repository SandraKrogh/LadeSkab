using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LadeSkab.Interfaces;

namespace LadeSkab
{
    public class Door : IDoor
    {

        public event EventHandler<DoorStateEventArg> DoorStateChanged;

        public bool CurrentState { get; set; }

        public bool locked { get; set; }

        public Door()
        {
            //Åben fra start
            CurrentState = false;
            locked = false;
        }

        //Dør åbnes
        public void DoorOpen()
        {
            CurrentState = false;
            OnNewState();
        }

        //Dør lukkes
        public void DoorClosed()
        {

            CurrentState = true;
            OnNewState();
        }

        //Lås Dør
        public void LockDoor()
        {
            locked = true;
        }

        //Åben dør 
        public void UnlockDoor()
        {
            locked = false;
        }

        private void OnNewState()
        {
            DoorStateChanged?.Invoke(this, new DoorStateEventArg() { State = this.CurrentState });
        }
    }
}
