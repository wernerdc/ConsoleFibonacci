namespace ConsoleFibonacci {
    
    internal class Program {

        private static readonly string[,] dataTypes = { 
            { "byte",      "8 Bit", "0 bis                         255" },
            { "ushort",   "16 Bit", "0 bis                      65.535" }, 
            { "uint",     "32 Bit", "0 bis               4.294.967.295" },
            { "ulong",    "64 Bit", "0 bis  18.446.744.073.709.551.615" }, 
            { "string", "n*16 Bit", "           like an array of chars" }, 
            { "all",            "", "                   show all types" } 
        };

        static void Main(string[] args) {

            bool appRunning = true;

            while (appRunning) {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();
                Console.WriteLine("ConsoleFibunacci \n");

                // get user input from console. Convert it to int and handle any exeptions.
                int type = GetDataTypeFromInput();
                int count = GetFibonacciCountFromInput();

                string selectedOption = dataTypes[type, 0];
                switch (selectedOption) {
                    case "byte":
                    case "ushort":
                    case "uint":
                    case "ulong":
                        ShowFibonacciNumsByDataType(selectedOption, count);
                        break;

                    case "string":
                        ShowFibonacciNumsByStringAlgorithm(count);
                        break;

                    case "all":
                        ShowFibonacciNumsByDataType("byte", count);
                        ShowFibonacciNumsByDataType("ushort", count);
                        ShowFibonacciNumsByDataType("uint", count);
                        ShowFibonacciNumsByDataType("ulong", count);
                        ShowFibonacciNumsByStringAlgorithm(count);
                        break;

                    default:
                        break;
                }

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
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" {0} ", i);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("= {0,-7} {1,10}    {2}", dataTypes[i, 0], dataTypes[i, 1], dataTypes[i, 2]);
            }
            Console.WriteLine();

            int typeIndex = -1;
            while (typeIndex < 0) {
                try {
                    // try to convert string from console to integer 
                    Console.Write("Welcher Datentyp soll verwendet werden? ");
                    typeIndex = Convert.ToInt32(Console.ReadLine());
                    if (typeIndex < 0 || typeIndex >= dataTypes.GetLength(0)) {
                        typeIndex = -1;
                        ShowTypeErrorMessage();
                    }
                } catch {
                    // catch errors so the app doesn't crash if one ocours
                    ShowTypeErrorMessage();
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
                        ShowInputErrorMessage();
                    }
                } catch {
                    // catch errors so the app doesn't crash if one ocours
                    ShowInputErrorMessage();
                }
            }

            return count;
        }

        private static void ShowTypeErrorMessage() {
            Console.SetCursorPosition(0, Console.GetCursorPosition().Top -2);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\nUngültige Eingabe: Nur Ganzzahlen von 0 bis {0} sind erlaubt", dataTypes.GetLength(0) -1);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        
        private static void ShowInputErrorMessage() {
            Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 2);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\nUngültige Eingabe: Nur positive Ganzzahlen > 0 sind erlaubt");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void ShowFibonacciNumsByDataType(string type, int count) {
            ulong f1 = 0;
            ulong f2 = 1;
            ulong newResult = 0;
            bool lastResultOK = true;

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n{0}: ", type);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Nur die grünen Ergebnisse wurden mit Typ {0} korrekt berechnet.\n", type);
            Console.ForegroundColor = ConsoleColor.Green;

            for (int i = 0; i < count; i++) {
                // exceptions for the first two numbers which are allready preset in f1 and f2, then begin calculations
                if (i <= 1) {
                    Console.Write(i);
                    WriteCommaSeparatorToConsole();
                    continue;
                }
                
                newResult = CalcNewResultByDataType(type, f1, f2);

                // insert a line break if window width would be reached by next number
                //            cursors y position     + formatted result length   + (", ")  >  console window width in chars
                if (Console.GetCursorPosition().Left + newResult.ToString("N0").Length + 2 >= Console.WindowWidth) {
                    Console.Write("\n");
                }

                // mark incorrect results by changeing color
                if (lastResultOK) {
                    if (newResult.ToString() != CalcNewResultString(f1.ToString(), f2.ToString())) {
                        lastResultOK = false;
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    }
                }
                Console.Write(newResult.ToString("N0"));           
                WriteCommaSeparatorToConsole();
                Console.ForegroundColor = lastResultOK ? ConsoleColor.Green : ConsoleColor.DarkMagenta;

                f1 = f2;
                f2 = newResult;
                
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
        }

        private static ulong CalcNewResultByDataType(string type, ulong f1, ulong f2) {
            ulong newResult = 0;
            // explicit cast of calculation (f1 + f2) to the selectet data type
            switch (type) {
                case "byte":
                    return newResult = (byte) (f1 + f2);
                case "ushort":
                    return newResult = (ushort) (f1 + f2);
                case "uint":
                    return newResult = (uint) (f1 + f2);
                case "ulong":
                    return newResult = (ulong) (f1 + f2);
                default:
                    return newResult;
            }
        }

        private static void ShowFibonacciNumsByStringAlgorithm(int count) {
            string f1 = "0";
            string f2 = "1";

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nstring: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Mit dem Additions-Algorithmus können wesentlich größere Zahlen berechnet und als string dargestellt werden.\n");
            Console.ForegroundColor = ConsoleColor.Green;

            for (int i = 0; i < count; i++) {
                // exception: first + second Fibbonacci nums (f1/f2) are allready set
                if (i <= 1) {           
                    Console.Write(i);
                    WriteCommaSeparatorToConsole();
                    continue;
                }

                string newResult = CalcNewResultString(f1, f2);
                
                // add dot separator (1.000.000.000.000....)
                string formattedResult = "";
                for (int j = 0; j < newResult.Length; j++) {
                    if ((newResult.Length - j) % 3 == 0 && j != 0) {
                        formattedResult += ".";
                    }
                    formattedResult += newResult[j].ToString();
                }

                // insert a line break if window width would be reached by next number
                //   cursors y position           + formattedResult+space length >  console window width (in chars)
                if (Console.GetCursorPosition().Left + formattedResult.Length +2 >= Console.WindowWidth) {
                    Console.Write("\n");
                }
                Console.Write(formattedResult);
                WriteCommaSeparatorToConsole();

                // update f1 f2 for the next loop 
                f1 = f2;
                f2 = newResult;
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }


        private static string CalcNewResultString(string f1, string f2) {
            // equalize f1/f2 string length for the next loop by adding a leading 0 
            if (f2.Length > f1.Length) {
                f1 = 0 + f1;
            }
            string newResult = "";
            int carryOver = 0;

            // loop through the f1/f2 strings starting from the right (last index) 
            for (int j = f2.Length - 1; j >= 0; j--) {

                // sum numbers at index position
                int subSum = int.Parse(f1[j].ToString()) + int.Parse(f2[j].ToString()) + carryOver;
                
                // add subSums first digit to newResult string (add to the left)
                newResult = (subSum % 10) + newResult;
                
                // strore the carry over (second digit) for the next loop 
                carryOver = subSum / 10;

                // add carry over to actual result if the last loop (index 0) is reached
                if (j == 0 && carryOver > 0) {
                    newResult = carryOver + newResult;
                }
            }

            return newResult;
        }

        private static void WriteCommaSeparatorToConsole() {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(", ");
            Console.ForegroundColor = ConsoleColor.Green;
        }
    }
}
