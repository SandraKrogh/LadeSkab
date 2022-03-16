using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LadeSkab.Interfaces;

namespace LadeSkab
{
    public class RfidReader : IRfdReader
    { 
        public event EventHandler<RfidDetectedEventArgs> RfidDetected;

        public int CurrentId { get; set; }

        public void SimulateDetected(int id)
        {
            CurrentId = id;
            OnNewCurrent();
        }

        private void OnNewCurrent()
        {
            RfidDetected?.Invoke(this, new RfidDetectedEventArgs() { id = this.CurrentId });
        }
    }
}
