using System;
using System.Diagnostics;
using System.Reflection;
using Farfetch.Toggler.AppManager;

namespace Farfetch.PlusApp
{
    public class Number
    {
        private int valueToAdd = 2;

        private bool Check(string toggleName, bool toggleValue)
        {
            if (string.IsNullOrEmpty(toggleName)) throw new ArgumentNullException(nameof(toggleName));
            string executingAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;

            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;

            return CheckToggle.ShouldExec(toggleName, toggleValue, "service1", "1.0.0");

            // Call via http to localhost blah

        }

        public int CalcNumber(int number)
        {
            int valueToReturn = number;

            if (Check("toggle55", true))
            {
                valueToReturn = number + valueToAdd;
            }

            return valueToReturn;
        }
    }
}
