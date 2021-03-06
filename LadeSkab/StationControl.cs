using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LadeSkab.Interfaces;

namespace LadeSkab
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        private LadeskabState _state;
        private IChargeControl _chargeControl;
        public int _oldId;
        public IDoor _door;
        public IRfidReader _rfidReader;
        public IDisplay _myDisplay;

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        // Her mangler constructor
        public StationControl(IDoor door, IRfidReader reader, IChargeControl chargeControl, IDisplay display)
        {
            _myDisplay = display;
            _chargeControl = chargeControl;
            _door = door;
            _rfidReader = reader;
            _door.DoorStateChanged += HandleDoorChangedEvent;
            reader.RfidDetected += HandleRfidDetected;
            _state = LadeskabState.Available;
        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void HandleRfidDetected(object sender, RfidDetectedEventArgs e)
        {
            int id = e.id;

            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_chargeControl.IsConnected())
                    {
                        _door.LockDoor();
                        _chargeControl.StartCharge();
                        _oldId = id;

                        /*
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }*/

                        _myDisplay.WriteLine("Ladeskab optaget");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        _myDisplay.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _chargeControl.StopCharge();
                        _door.UnlockDoor();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        }

                        _myDisplay.WriteLine("Fjern telefon");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _myDisplay.WriteLine("Forkert RFID tag");
                    }

                    break;
            }
        }

        //Door state changed
        private void HandleDoorChangedEvent(object sender, DoorStateEventArg e)
        {
            //Bruger åbner dør
            if(e.State == false)
            {
                _myDisplay.WriteLine("Tilslut Telefon");
                _state = LadeskabState.DoorOpen;
            }
            //Døren lukker dør 
            else if (e.State == true)
            {
                _myDisplay.WriteLine("Indlæs RFID");
                _state = LadeskabState.Available;

            }
        }

    }
}
