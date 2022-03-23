using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LadeSkab.Interfaces;

namespace LadeSkab
{
    public class RfidReaderSimulator : IRfidReader
    { 
        public event EventHandler<RfidDetectedEventArgs> RfidDetected;

        public int CurrentId { get; set; }

        public void SimulateDetected(int id)
        {
            CurrentId = id;
            OnNewid();
        }

        private void OnNewid()
        {
            RfidDetected?.Invoke(this, new RfidDetectedEventArgs() { id = this.CurrentId });
        }
    }
}
