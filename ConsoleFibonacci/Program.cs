namespace ConsoleFibonacci
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ConsoleFibunacci \n");

            int x = 0;
            int y = 1;
            int newResult = 0;


            for (int i = 0; i < 10; i++)
            {
                switch (i)
                {
                    case 0:
                        Console.Write(x);
                        break;
                    case 1:
                        Console.Write(", " + y);
                        break;
                    default:
                        newResult = x + y;
                        Console.Write(", " + newResult);

                        x = y;
                        y = newResult;
                        break;
                }
            }


            Console.ReadLine();
        }
    }
}
