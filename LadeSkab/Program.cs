using LadeSkab;

namespace LadeSkab
{
    class Program
    {
        static void Main(string[] args)
        {
        // Assemble your system here from all the classes

        Door mydoor = new Door();
        RfidReaderSimulator myRfidReader = new RfidReaderSimulator();
        StationControl myStationControl = new StationControl(mydoor, myRfidReader);
   


            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E - end, O - Open door, C - close Door, R - Rfid id: ");
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
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

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
