using System;
using System.Runtime.Serialization;

namespace Codan.Argus.TestEnvironment.PolarionAPI
{
    /// <summary>
    ///     TestSequenceException: This occurs if any element is double in TestSequence field of TestPlan.
    /// </summary>
    [Serializable]
    public class TestSequenceException : Exception
    {
        private object _id;

        private static readonly string _errorMessage = "An element with this ID had already been added: {0}";

        /// <summary>
        ///     Gets or sets the ID.
        /// </summary>
        public object ID
        {
            get => _id;
            set => _id = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public TestSequenceException(object id)
            : base(string.Format(TestSequenceException._errorMessage, id))
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public TestSequenceException(string message)
            : base(string.Format(TestSequenceException._errorMessage, message))
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public TestSequenceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected TestSequenceException (SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}