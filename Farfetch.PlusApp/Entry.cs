using System;
using Ferfetch.Messaging;

namespace Farfetch.PlusApp
{
    public class Entry
    {
        private static Number number;

        private static int plusNumber;
        private static int plusNumberTmp;
        private static int multNumber;
        private static int multNumberTmp;

        private const int INPUT_VALUE = 4;

        public static void Main(string[] args)
        {
            number = new Number();
            plusNumber = number.AddNumber(INPUT_VALUE);
            multNumber = number.MultNumber(INPUT_VALUE);
            plusNumberTmp = plusNumber;
            multNumberTmp = multNumber;

            Console.WriteLine("PlusNumber: " + plusNumber);
            Console.WriteLine("MultNumber: " + multNumber);

            number.toggleChangedEvent += UpdateStatus;
            number.RegisterToggles();

            while (true)
            {
                if (plusNumber != plusNumberTmp)
                {
                    plusNumber = plusNumberTmp;
                    Console.WriteLine("--------------------------");
                    Console.WriteLine("PlusNumber: " + plusNumber);
                    Console.WriteLine("--------------------------");
                }

                if (multNumber != multNumberTmp)
                {
                    multNumber = multNumberTmp;
                    Console.WriteLine("--------------------------");
                    Console.WriteLine("MultNumber: " + multNumber);
                    Console.WriteLine("--------------------------");
                }
            }
        }

        private static void UpdateStatus(string str)
        {
            plusNumberTmp = number.AddNumber(INPUT_VALUE);
            multNumberTmp = number.MultNumber(INPUT_VALUE);
        }
    }
}