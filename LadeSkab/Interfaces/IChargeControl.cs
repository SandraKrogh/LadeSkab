﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeSkab.Interfaces
{
    public interface IChargeControl
    {
        void StartCharge();
        void StopCharge();
        void IsConnected();
    }
}
