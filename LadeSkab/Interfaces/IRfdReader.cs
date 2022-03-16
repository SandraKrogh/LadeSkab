﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeSkab.Interfaces
{
    public class RfidDetectedEventArgs : EventArgs
    {
        public int id { set; get; }
    }

    public interface IRfdReader
    {
        event EventHandler<RfidDetectedEventArgs> RfidDetected;

        int CurrentId { get; set; }

        void SimpulateDetected(int id);


    }
}