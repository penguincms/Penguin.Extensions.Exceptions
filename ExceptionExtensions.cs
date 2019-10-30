using System;
using System.Diagnostics.Contracts;

namespace Penguin.Extensions.Exceptions
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public static class ExceptionExtensions
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        /// <summary>
        /// Recurses through the inner exceptions of an exception to concat the message into a single string for logging
        /// </summary>
        /// <param name="ex">The message to recurse through</param>
        /// <returns>A concat of all the inner (and outer) messages</returns>
        public static string RecursiveMessage(this Exception ex)
        {
            Contract.Requires(ex != null);
            string output = string.Empty;

            do
            {
                output += ex.Message + "\r\n\r\n";

                ex = ex.InnerException;
            }
            while (ex != null);

            return output;
        }

        public static bool TryFind<T>(this Exception ex, out T found) where T : Exception
        {
            Exception toCheck = ex;
            while (toCheck != null)
            {
                if (toCheck is T me)
                {
                    found = me;
                    return true;
                }

                toCheck = toCheck.InnerException;
            }
            found = null;
            return false;
        }

        /// <summary>
        /// Recurses through the inner exceptions of an exception to concat the stack trace into a single string for logging
        /// </summary>
        /// <param name="ex">The message to recurse through</param>
        /// <returns>A concat of all the inner (and outer) stack traces</returns>
        public static string RecursiveStackTrace(this Exception ex)
        {
            Contract.Requires(ex != null);

            string output = string.Empty;

            do
            {
                output += ex.StackTrace + "\r\n\r\n";

                ex = ex.InnerException;
            }
            while (ex != null);

            return output;
        }
    }
}