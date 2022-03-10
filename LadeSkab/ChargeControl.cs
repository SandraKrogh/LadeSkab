using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LadeSkab.Interfaces;

namespace LadeSkab
{
    public class ChargeControl : IChargeControl
    {
        IUsbCharger myUsbCharger_;
        public double currentCurrent_; // Nuværende strøm

        ChargeControl(IUsbCharger usbCharger)
        {
            myUsbCharger_ = usbCharger;

            usbCharger.CurrentValueEvent += HandleCurrentValueEvent;
        }
        
        // Handler for event fra UsbCharger
        private void HandleCurrentValueEvent(object sender, CurrentEventArgs e)
        {
            currentCurrent_ = e.Current;

            Console.WriteLine(@"Strømmen har aendret sig til: {currentCurrent_}");
        }

        public void StartCharge()
        {
            if (IsConnected())
                myUsbCharger_.StartCharge();
        }
        public void StopCharge()
        {
            myUsbCharger_.StopCharge();
        }
        public bool IsConnected()
        {
            return myUsbCharger_.Connected;
        }
    }
}
