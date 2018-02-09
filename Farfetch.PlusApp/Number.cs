using Farfetch.APIHandler.API_Toggler.Internal;
using Farfetch.APIHandler.Common;
using Ferfetch.Messaging;

namespace Farfetch.PlusApp
{
    /// <summary>
    /// Example service application where a toggle is used
    /// </summary>
    public class Number: ApiService
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
        /// Event Delegate
        /// </summary>
        public event FarfetchDelegate toggleChangedEvent;

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
            RegisterToggle("toggle1", toggleChangedEvent);
            RegisterToggle("toggle2", toggleChangedEvent);
        }

        /// <summary>
        /// Registers each toggle with the subscriber module
        /// </summary>
        /// <param name="toggleName">The toggle name</param>
        /// <param name="delegateHandler">The event handler</param>
        private void RegisterToggle(string toggleName, FarfetchDelegate delegateHandler)
        {
            _subscriber.ReceiveMessage(toggleName, delegateHandler);
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
    }
}
