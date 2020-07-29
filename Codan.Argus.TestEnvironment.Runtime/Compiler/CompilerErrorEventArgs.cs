namespace Codan.Argus.TestEnvironment.Runtime.Compiler
{
    using System;
    using System.CodeDom.Compiler;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class CompilerErrorEventArgs : EventArgs
    {
        private System.CodeDom.Compiler.CompilerErrorCollection _compilerErrorCollection;

        private string _source;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompilerErrorEventArgs"/> class.
        /// </summary>
        public CompilerErrorEventArgs()
        {
            this._compilerErrorCollection = null;
            this._source = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompilerErrorEventArgs"/> class.
        /// </summary>
        /// <param name="compilerErrorCollection">The compiler error collection.</param>
        public CompilerErrorEventArgs(System.CodeDom.Compiler.CompilerErrorCollection compilerErrorCollection)
        {
            this._compilerErrorCollection = null;
            this._source = string.Empty;
            this._compilerErrorCollection = compilerErrorCollection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompilerErrorEventArgs"/> class.
        /// </summary>
        /// <param name="_compilerErrorCollection">The compiler error collection.</param>
        /// <param name="source">The s source.</param>
        public CompilerErrorEventArgs(System.CodeDom.Compiler.CompilerErrorCollection _compilerErrorCollection,
                                      string source)
            : this(_compilerErrorCollection)
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
            string stringRepresentation = string.Empty;

            foreach (CompilerError error in this._compilerErrorCollection)
            {
                stringRepresentation = stringRepresentation + error.ToString();
            }

            return stringRepresentation;
        }

        /// <summary>
        /// Gets the compiler error collection.
        /// </summary>
        /// <value>
        /// The compiler error collection.
        /// </value>
        public CompilerErrorCollection CompilerErrorCollection => this._compilerErrorCollection;

        /// <summary>
        /// Gets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public string Source => this._source;
    }
}