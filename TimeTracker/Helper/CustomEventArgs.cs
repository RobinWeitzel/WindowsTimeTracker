using System;

namespace TimeTracker
{
    /// <summary>
    /// Meant to be used in pace of EventArgs to pass along additional information with an event.
    /// </summary>
    public class CustomEventArgs : EventArgs
    {
        private readonly object value;

        /// <summary>
        /// Gets the value that was passed along
        /// </summary>
        public object Value
        {
            get { return this.value; }
        }

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
            this.value = value;
        }
    }
}
