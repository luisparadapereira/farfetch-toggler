using System;

namespace Farfetch.PlusApp
{
    public class Entry
    {
        private static Number _number;

        private static int _plusNumber;
        private static int _plusNumberTmp;
        private static int _multNumber;
        private static int _multNumberTmp;

        private const int INPUT_VALUE = 4;

        public static void Main(string[] args)
        {
            _number = new Number();
            _plusNumber = _number.AddNumber(INPUT_VALUE);
            _multNumber = _number.MultNumber(INPUT_VALUE);
            _plusNumberTmp = _plusNumber;
            _multNumberTmp = _multNumber;

            Console.WriteLine("PlusNumber: " + _plusNumber);
            Console.WriteLine("MultNumber: " + _multNumber);

            _number.Toggle1ChangedEvent += UpdatePlusValue;
            _number.Toggle2ChangedEvent += UpdateMultValue;
            _number.RegisterToggles();
        }

        private static void UpdateMultValue(string str)
        {
            Console.WriteLine("Requesting new mult value.");
            _multNumberTmp = _number.MultNumber(INPUT_VALUE);

            if (_multNumber != _multNumberTmp)
            {
                _multNumber = _multNumberTmp;
                Console.WriteLine("--------------------------");
                Console.WriteLine("MultNumber: " + _multNumber);
                Console.WriteLine("--------------------------");
            }
        }

        private static void UpdatePlusValue(string str)
        {
            Console.WriteLine("Requesting new plus value.");
            _plusNumberTmp = _number.AddNumber(INPUT_VALUE);

            if (_plusNumber != _plusNumberTmp)
            {
                _plusNumber = _plusNumberTmp;
                Console.WriteLine("--------------------------");
                Console.WriteLine("PlusNumber: " + _plusNumber);
                Console.WriteLine("--------------------------");
            }
        }
    }
}