using LadeSkab;
using System;

namespace LadeSkab
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
        ChargeControl myChargeControl = new ChargeControl(myUsbChargerSimulator,mydisplay);
        StationControl myStationControl = new StationControl(mydoor, myRfidReader,myChargeControl,mydisplay);


            bool finish = false;
            do
            {
                string input;
                Console.WriteLine("Indtast E - end, O - Open door, C - close Door, R - Rfid id: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        mydoor.SimulateDoorOpen();
                        break;

                    case 'C':
                        mydoor.SimulateDoorClosed();
                        break;

                    case 'R':
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
