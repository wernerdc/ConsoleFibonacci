namespace ConsoleFibonacci {
    internal class Program {
        static void Main(string[] args) {
            
            bool appRunning = true;

            while (appRunning) {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Clear();
                Console.WriteLine("ConsoleFibunacci \n");

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
