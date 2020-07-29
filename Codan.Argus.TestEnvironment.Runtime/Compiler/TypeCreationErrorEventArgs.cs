using System;

namespace Codan.Argus.TestEnvironment.Runtime.Compiler
{
    public class TypeCreationErrorEventArgs : EventArgs
    {
        private string _typeName = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeCreationErrorEventArgs"/> class.
        /// </summary>
        /// <param name="_typeName">Name of the s type.</param>
        public TypeCreationErrorEventArgs(string _typeName)
        {
            this._typeName = _typeName;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this._typeName;
        }

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        /// <value>
        /// The name of the type.
        /// </value>
        public string TypeName => this._typeName;
    }
}

