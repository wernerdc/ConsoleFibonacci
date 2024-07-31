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

                // get input from console + convert to int. handle any exeptions.
                int type = GetDataTypeFromInput();
                int count = GetFibonacciCountFromInput();

                // calculate the fibonacci numbers and output them to console window. 
                ShowFibonacciNumbers(count);
                ShowFibonacciNumsByStringAlgorithm(count);


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
            Console.WriteLine();

            int typeIndex = -1;
            while (typeIndex < 0) {
                try {
                    // try to convert string from console to integer 
                    Console.Write("Welcher Datentyp soll verwendet werden? ");
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
            Console.SetCursorPosition(0, Console.GetCursorPosition().Top -2);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\nUngültige Eingabe: Nur Ganzzahlen von 0 bis {0} sind erlaubt", dataTypes.GetLength(0) -1);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        
        private static void ShowErrorInputMessage() {
            Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 2);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\nUngültige Eingabe: Nur positive Ganzzahlen > 0 sind erlaubt");
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

        private static void ShowFibonacciNumsByStringAlgorithm(int count) {
            string f1 = "0";
            string f2 = "1";

            for (int i = 0; i < count; i++) {

                switch (i) {
                    case 0:
                        Console.Write("\n\n" + f1 + ", ");
                        break;
                    
                    case 1:
                        Console.Write(f2+ ", ");
                        break;
                    
                    default:
                        string newResult = "";
                        int carryOver = 0;
                        
                        // loop through the f1/f2 strings from right/last to the left/first digit (char)
                        for (int j = f2.Length -1; j >= 0; j--) {
                            // sum numbers at index position
                            int subSum = int.Parse(f1[j].ToString()) + int.Parse(f2[j].ToString()) + carryOver;
                            // add subSums first digit to newResult string (add to the left)
                            newResult = (subSum % 10) + newResult;
                            // strore the carry over (second digit) for the next loop 
                            carryOver = subSum / 10;
                            
                            // add carry over to new result if the last index/loop (0) is reached
                            if (j == 0 && carryOver > 0) {
                                newResult = carryOver + newResult;
                            }
                        }

                        // add dot separator (1.000.000.000.000....)
                        string formattedResult = "";
                        for (int j = 0; j < newResult.Length; j++) {
                            if ((newResult.Length - j) % 3 == 0 && j != 0) {
                                formattedResult += ".";
                            }
                            formattedResult += newResult[j].ToString();
                        }
                        formattedResult += ", ";

                        // insert a line break if window width would be reached by next number
                        //   cursors y position              + formattedResult length >  console window width (in chars)
                        if (Console.GetCursorPosition().Left + formattedResult.Length >= Console.WindowWidth) {
                            Console.Write("\n");
                        }
                        Console.Write(formattedResult);

                        // equalize string length for the next loop by adding a leading 0 
                        if (newResult.Length > f2.Length) {
                            f2 = 0 + f2;
                        }
                        f1 = f2;
                        f2 = newResult;
                        break;
                }
            }

        }
    }
}
