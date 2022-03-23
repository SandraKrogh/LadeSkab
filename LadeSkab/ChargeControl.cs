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
        public IDisplay myDisplay_;

        public double CurrentCurrent 
        {
            get { return currentCurrent_; }
            set { currentCurrent_ = value; }
        }

        public ChargeControl(IUsbCharger usbCharger, IDisplay display)
        {
            myUsbCharger_ = usbCharger;
            myDisplay_ = display;

            usbCharger.CurrentValueEvent += HandleCurrentValueEvent;
        }
        
        // Handler for event fra UsbCharger
        private void HandleCurrentValueEvent(object sender, CurrentEventArgs e)
        {
            currentCurrent_ = e.Current;

            if (currentCurrent_ > 0 && currentCurrent_ <= 5)
                myDisplay_.WriteLine("Telefonen er fuldt opladet");
            else if (currentCurrent_ > 5 && currentCurrent_ <= 500)
                myDisplay_.WriteLine("Telefonen lader");
            else if (currentCurrent_ > 500)
            {
                StopCharge();
                myDisplay_.WriteLine("Fejl - Der er noget galt!");
            }
        }

        public void StartCharge()
        {
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
