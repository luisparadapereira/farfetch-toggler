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

            _number.ToggleChangedEvent += UpdateStatus;
            _number.RegisterToggles();

            while (true)
            {
                if (_plusNumber != _plusNumberTmp)
                {
                    _plusNumber = _plusNumberTmp;
                    Console.WriteLine("--------------------------");
                    Console.WriteLine("PlusNumber: " + _plusNumber);
                    Console.WriteLine("--------------------------");
                }

                if (_multNumber != _multNumberTmp)
                {
                    _multNumber = _multNumberTmp;
                    Console.WriteLine("--------------------------");
                    Console.WriteLine("MultNumber: " + _multNumber);
                    Console.WriteLine("--------------------------");
                }
            }
        }

        private static void UpdateStatus(string str)
        {
            _plusNumberTmp = _number.AddNumber(INPUT_VALUE);
            _multNumberTmp = _number.MultNumber(INPUT_VALUE);
        }
    }
}