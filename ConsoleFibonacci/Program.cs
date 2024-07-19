namespace ConsoleFibonacci
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            Console.WriteLine("ConsoleFibunacci \n");

            Console.Write("Wieviele Fibonacci-Zahlen sollen berechnet werden? ");
            int count = 0;
            while (count < 1)
            {
                try
                {
                    count = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\nUngültige Eingabe: Nur positive Ganzzahlen > 0 sind erlaubt\n");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("Wieviele Fibonacci-Zahlen sollen berechnet werden? ");
                }
            }

            int consoleWidth = Console.WindowWidth;


            ShowFibonacciNumbers(count);

            Console.WriteLine("\n\nProgramm beenden (e)?");
            Console.ReadLine();
        }

        private static void ShowFibonacciNumbers(int count)
        {
            long x = 0;
            long y = 1;
            long newResult = 0;


            for (int i = 0; i < count; i++)
            {
                switch (i)
                {
                    case 0:
                        Console.Write("\n" + x);
                        break;
                    case 1:
                        Console.Write(", " + y);
                        break;
                    default:
                        newResult = x + y;
                        Console.Write(", " + newResult.ToString("N0"));

                        x = y;
                        y = newResult;
                        break;
                }
            }
        }
    }
}
