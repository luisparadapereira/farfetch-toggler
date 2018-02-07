using Farfetch.AppCommon;
using Farfetch.APIHandler.TogglerAPI;

namespace Farfetch.PlusApp
{
    /// <summary>
    /// Example service application where a toggle is used
    /// </summary>
    public class Number: ApplicationInformation
    {
        /// <summary>
        /// The value to add to the input number
        /// </summary>
        private const int VALUE_TO_ADD = 2;

        /// <summary>
        /// The Toggler API Communication
        /// </summary>
        private readonly TogglerApiInternal _togglerApiInternal;

        /// <summary>
        /// Default constructor gathers application information and the initializes
        /// the toggle API communication
        /// </summary>
        public Number()
        {
            GetApplicationInformation(typeof(Number));
            _togglerApiInternal = new TogglerApiInternal();
        }

        /// <summary>
        /// Given a number x will return number y.
        /// Depending if the togle is active or not:
        /// y = x -> toggle not active
        /// y = x + VALUE_TO_ADD -> toggle active
        /// </summary>
        /// <param name="number">The number to calculate</param>
        /// <returns>the input number with a possible operation on it</returns>
        public int CalcNumber(int number)
        {
            int valueToReturn = number;

            if (_togglerApiInternal != null && _togglerApiInternal.CheckToggle("toggle1", true, CallingAssemblyName, CallingAssemblyVersion))
            {
                valueToReturn = number + VALUE_TO_ADD;
            }

            return valueToReturn;
        }
    }
}
