using System;

namespace TimeTracker
{
    /// <summary>
    /// Meant to be used in pace of EventArgs to pass along additional information with an event.
    /// </summary>
    public class CustomEventArgs : EventArgs
    {
        public object Value { get; }

        /// <summary>
        /// Used if no value needs to be sent along.
        /// </summary>
        public CustomEventArgs()
        {

        }

        /// <summary>
        /// Used if a value should be passed along with the event
        /// </summary>
        /// <param name="value">Can be any value that should be passed along with the event</param>
        public CustomEventArgs(object value)
        {
            Value = value;
        }
    }
}
