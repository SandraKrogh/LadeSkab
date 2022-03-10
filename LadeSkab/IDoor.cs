using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeSkab
{
    public class DoorEventArg : EventArgs
    {
        public double Current { set; get; }
    }

    public interface IDoor
    {
        // Event triggered on new current value
        event EventHandler<DoorEventArg> CurrentValueEvent;

        // Direct access to the current current value
        bool CurrentValue { get; }

        // Require connection status of the phone
        bool Connected { get; }

        // Start charging
        void StartCharge();
        // Stop charging
        void StopCharge();
    }
}
