using System;
using System.Reflection;

namespace Codan.Argus.TestEnvironment.Runtime.Compiler
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class CompilerSuccessEventArgs : EventArgs
    {
        private Assembly _assembly;

        private string _source;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompilerSuccessEventArgs"/> class.
        /// </summary>
        public CompilerSuccessEventArgs()
        {
            this._assembly = null;
            this._source = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompilerSuccessEventArgs"/> class.
        /// </summary>
        /// <param name="assembly">The compiler error collection.</param>
        public CompilerSuccessEventArgs(Assembly assembly)
        {
            this._assembly = null;
            this._source = string.Empty;
            this._assembly = assembly;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompilerSuccessEventArgs"/> class.
        /// </summary>
        /// <param name="_assembly">The compiler error collection.</param>
        /// <param name="source">The s source.</param>
        public CompilerSuccessEventArgs(Assembly _assembly,
                                        string source) : this(_assembly)
        {
            this._source = source;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this._assembly != null ? this._assembly.ImageRuntimeVersion : "";
        }

        /// <summary>
        /// Gets the compiler error collection.
        /// </summary>
        /// <value>
        /// The compiler error collection.
        /// </value>
        public Assembly Assembly => this._assembly;

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public string Source
        {
            get => this._source;
            set => this._source = value;
        }
    }
}