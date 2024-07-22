namespace ConsoleFibonacci {
    internal class Program {
        static void Main(string[] args) {
            
            bool appRunning = true;

            while (appRunning) {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Clear();
                Console.WriteLine("ConsoleFibunacci \n");

                int count = GetFibonacciCountFromInput();
                ShowFibonacciNumbers(count);

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
            Console.Write("Wieviele Fibonacci-Zahlen sollen berechnet werden? ");
            int count = 0;
            while (count < 1) {
                try {
                    count = Convert.ToInt32(Console.ReadLine());
                    if (count <= 0) {
                        ShowErrorInputMessage();
                    }
                } catch {
                    ShowErrorInputMessage();
                }
            }

            return count;
        }

        private static void ShowErrorInputMessage() {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\nUngültige Eingabe: Nur positive Ganzzahlen > 0 sind erlaubt\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Wieviele Fibonacci-Zahlen sollen berechnet werden? ");
        }

        private static void ShowFibonacciNumbers(int count) {
            long x = 0;
            long y = 1;
            long newResult = 0;


            for (int i = 0; i < count; i++) {
                // exceptions for the first two numbers which are preset in x and y, then begin calculations (x + y)
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
