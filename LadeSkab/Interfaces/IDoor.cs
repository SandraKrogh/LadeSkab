using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeSkab.Interfaces
{
    public class DoorStateEventArg : EventArgs
    {
        public bool State { set; get; }
    }

    public interface IDoor
    {
        // Event triggered on new current value
        event EventHandler<DoorStateEventArg> DoorStateChanged;

        // Direct access to the current value
        bool CurrentState { get; set; }

        public bool locked { get; set; }

        //Open door
        void DoorOpen();

        //Close door
        void DoorClosed();

        void LockDoor();

        void UnlockDoor();

    }
}
