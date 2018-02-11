using Farfetch.APIHandler.API_Toggler.Internal;
using Farfetch.APIHandler.Common;
using Farfetch.Common;
using Farfetch.Messaging;

namespace Farfetch.PlusApp
{
    /// <summary>
    /// Example service application where a toggle is used
    /// </summary>
    public class Number: ApiAssembly,  IApplication
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
        /// Message Subscriber
        /// </summary>
        private readonly Subscriber _subscriber;

        /// <summary>
        /// Event  for changes in plus values
        /// </summary>
        public event FarfetchDelegate Toggle1ChangedEvent;

        /// <summary>
        /// Event  for changes in plus values
        /// </summary>
        public event FarfetchDelegate Toggle2ChangedEvent;

        /// <summary>
        /// Default constructor gathers application information and the initializes
        /// the toggle API communication
        /// </summary>
        public Number()
        {
            GetApplicationInformation(typeof(Number));
            GetApiKey(CallingAssemblyName, CallingAssemblyVersion);
            _togglerApiInternal = new TogglerApiInternal();
            _subscriber = new Subscriber();
        }

        /// <summary>
        /// Registers all the application toggles
        /// </summary>
        public void RegisterToggles()
        {
            _subscriber.ReceiveMessage("toggle1", Toggle1ChangedEvent);
            _subscriber.ReceiveMessage("toggle2", Toggle2ChangedEvent);
        }

        /// <summary>
        /// Given a number x will return number y.
        /// Depending if the togle is active or not:
        /// y = x -> toggle not active
        /// y = x + VALUE_TO_ADD -> toggle active
        /// </summary>
        /// <param name="number">The number to calculate</param>
        /// <returns>the input number with a possible operation on it</returns>
        public int AddNumber(int number)
        {
            int valueToReturn = number;
            if (_togglerApiInternal != null && _togglerApiInternal.CheckToggle("toggle1", true, CallingAssemblyName, CallingAssemblyVersion, Key.Key))
            {
                valueToReturn = number + VALUE_TO_ADD;
            }

            return valueToReturn;
        }

        /// <summary>
        /// Given a number x will return number y.
        /// Depending if the togle is active or not:
        /// y = x -> toggle not active
        /// y = x * VALUE_TO_ADD -> toggle active
        /// </summary>
        /// <param name="number">The number to calculate</param>
        /// <returns>the input number with a possible operation on it</returns>
        public int MultNumber(int number)
        {
            int valueToReturn = number;
            if (_togglerApiInternal != null && _togglerApiInternal.CheckToggle("toggle2", true, CallingAssemblyName, CallingAssemblyVersion, Key.Key))
            {
                valueToReturn = number * VALUE_TO_ADD;
            }

            return valueToReturn;
        }


        /// <inheritdoc cref="IApplication" />
        public string GetAssemblyName()
        {
            return CallingAssemblyName;
        }

        /// <inheritdoc cref="IApplication" />
        public string GetAssemblyVersion()
        {
            return CallingAssemblyVersion;
        }
    }
}
