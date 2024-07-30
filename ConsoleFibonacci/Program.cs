namespace ConsoleFibonacci {
    
    internal class Program {

        public static string[,] dataTypes = { 
            { "byte",    "8 Bit" },
            { "ushort", "16 Bit" }, 
            { "uint",   "32 Bit" },
            { "ulong",  "64 Bit" }, 
            { "string",  "x Bit" } 
        };

        static void Main(string[] args) {

            bool appRunning = true;

            while (appRunning) {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Clear();
                Console.WriteLine("ConsoleFibunacci \n");

                int type = GetDataTypeFromInput();
                // get input from console + convert to int. handle any exeptions.
                int count = GetFibonacciCountFromInput();

                // calculate the fibonacci numbers and output them to console window. Also do not split
                // any number at the right window border.
                ShowFibonacciNumbers(count);

                // exit or repeat the app
                Console.Write("\n\nProgramm beenden (e)? ");
                try {
                    string exitApp = Console.ReadLine();
                    if (exitApp.ToUpper() == "E") {
                        appRunning = false;
                    }
                } catch {
                    // no error message, just keep going and repeat the app
                }
            }
        }

        private static int GetDataTypeFromInput() {

            for (int i = 0; i < dataTypes.GetLength(0); i++) {
                Console.WriteLine("{0} = {1,-7} {2,6}", i, dataTypes[i, 0], dataTypes[i, 1]);
            }
            Console.Write("Welcher Datentyp soll verwendet werden? ");

            int typeIndex = -1;
            while (typeIndex < 0) {
                try {
                    // try to convert string from console to integer 
                    typeIndex = Convert.ToInt32(Console.ReadLine());
                    if (typeIndex < 0 || typeIndex >= dataTypes.GetLength(0)) {
                        typeIndex = -1;
                        ShowErrorTypeMessage();
                    }
                } catch {
                    // catch errors so the app doesn't crash if one ocours
                    // then show an error message
                    ShowErrorTypeMessage();
                }
            }

            return typeIndex;
        }

        private static int GetFibonacciCountFromInput() {
            int count = 0;
            while (count < 1) {
                try {
                    // try to convert string from console to integer 
                    Console.Write("Wieviele Fibonacci-Zahlen sollen berechnet werden? ");
                    count = Convert.ToInt32(Console.ReadLine());
                    if (count <= 0) {
                        ShowErrorInputMessage();
                    }
                } catch {
                    // catch errors so the app doesn't crash if one ocours
                    // then show an error message
                    ShowErrorInputMessage();
                }
            }

            return count;
        }

        private static void ShowErrorTypeMessage() {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\nUngültige Eingabe: Nur positive Ganzzahlen von 0 bis {0} sind erlaubt\n", dataTypes.GetLength(0) -1);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        
        private static void ShowErrorInputMessage() {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\nUngültige Eingabe: Nur positive Ganzzahlen > 0 sind erlaubt\n");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void ShowFibonacciNumbers(int count) {
            ulong x = 0;
            ulong y = 1;
            ulong newResult = 0;


            for (int i = 0; i < count; i++) {
                // exceptions for the first two numbers which are allready preset in x and y, then begin calculations (x + y)
                switch (i) {
                    case 0:
                        Console.Write("\n" + x + ", ");
                        break;
                    case 1:
                        Console.Write(y + ", ");
                        break;
                    default:
                        newResult = x + y; 

                        // insert a line break if window width would be reached by next number
                        //            cursor y position      + formatted newResult lengt + (", ")  >  console window width in chars
                        if (Console.GetCursorPosition().Left + newResult.ToString("N0").Length + 2 >= Console.WindowWidth) {
                            Console.Write("\n");
                        }
                        Console.Write(newResult.ToString("N0") + ", ");

                        x = y;
                        y = newResult;
                        break;
                }
            }
        }
    }
}
