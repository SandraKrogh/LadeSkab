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
        private IChargeControl _charger;
        private int _oldId;
        private IDoor _door;
        private IRfidReader _rfidReader;
        private IDisplay _myDisplay = new Display();

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        // Her mangler constructor
        public StationControl(IDoor door, IRfidReader reader)
        {
            door.DoorStateChanged += HandleDoorChangedEvent;
            reader.RfidDetected += HandleRidDetected;
            _state = LadeskabState.Available;


        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.IsConnected())
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = id;

                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }

                        _myDisplay.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        }

                        Console.WriteLine("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        Console.WriteLine("Forkert RFID tag");
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
            else if (e.State == false)
            {
                _myDisplay.WriteLine("Indlæs RFID");
                _state = LadeskabState.Available;

            }
        }


        //Rfid detected
        private void HandleRidDetected(object sender, RfidDetectedEventArgs e)
        {
            RfidDetected(e.id);
        }
    }
}
