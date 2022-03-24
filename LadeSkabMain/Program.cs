using LadeSkab;
using System;

namespace LadeSkabMain
{
    class Program
    {
         static void Main(string[] args)
        {
            // Assemble your system here from all the classes

            Door mydoor = new Door();
            Display mydisplay = new Display();
            RfidReaderSimulator myRfidReader = new RfidReaderSimulator();
            UsbChargerSimulator myUsbChargerSimulator = new UsbChargerSimulator();
            ChargeControl myChargeControl = new ChargeControl(myUsbChargerSimulator, mydisplay);
            StationControl myStationControl = new StationControl(mydoor, myRfidReader, myChargeControl, mydisplay);


            bool finish = false;
            do
            {
                string input;
                Console.WriteLine("Indtast \n E - end \n O - Open door \n C - close Door \n T - Tilslut telefon \n R - Rfid id: \n");

                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O': //Bruger åbner dør
                        mydoor.SimulateDoorOpen();
                        break;

                    case 'C': //Bruger lukker dør
                        mydoor.SimulateDoorClosed();
                        break;

                    case 'T': //Bruger tilslutter telefon
                        myUsbChargerSimulator.SimulateConnected(true);
                        break;

                    case 'R': //Bruger holder tag til læser 
                        Console.WriteLine("Indtast RFID id: ");
                        string idString = Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        myRfidReader.SimulateDetected(id);
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }

    }
}
