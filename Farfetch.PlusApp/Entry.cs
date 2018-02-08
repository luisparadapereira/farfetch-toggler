using System;

namespace Farfetch.PlusApp
{
    public class Entry
    {
        public static void Main(string[] args)
        {
            Number number = new Number();
            while (true)
            {
                var key = Console.ReadKey();
                
                if (key.Key == ConsoleKey.Backspace)
                {
                    return;
                }

                if (key.Key == ConsoleKey.Enter)
                {
                    int value = number.CalcNumber(4);
                    Console.WriteLine("----> " + value);
                }
            }
        }
    }
}