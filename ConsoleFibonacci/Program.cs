namespace ConsoleFibonacci {
    internal class Program {
        static void Main(string[] args) {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            Console.WriteLine("ConsoleFibunacci \n");

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

            ShowFibonacciNumbers(count);

            Console.WriteLine("\n\nProgramm beenden (e)?");
            Console.ReadLine();
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
                switch (i) {
                    case 0:
                        Console.Write("\n" + x + ", ");
                        break;
                    case 1:
                        Console.Write(y + ", ");
                        break;
                    default:
                        newResult = x + y;

                        // break line if window width would be reached by next number
                        if (Console.GetCursorPosition().Left + newResult.ToString("N0").Length + 2 >= Console.WindowWidth) {
                            Console.Write("\n");
                        }
                        //newResult.ToString().Length;
                        //Console.GetCursorPosition().Left;

                        Console.Write(newResult.ToString("N0") + ", ");

                        x = y;
                        y = newResult;
                        break;
                }
            }
        }
    }
}
