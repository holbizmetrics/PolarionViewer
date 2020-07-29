using Microsoft.CSharp;

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Linq;

namespace Codan.Argus.TestEnvironment.Runtime.Compiler
{
    /// <summary>
    ///     Used to evaluate a function that is runtime compiled.
    /// </summary>
    public class ReflectiveEvaluator : BaseEvaluator
    {
        /// <summary>
        /// Provide default compiler parameters
        /// </summary>
        private static readonly Dictionary<string, string> _compilerParameters = new Dictionary<string, string>
        {
            {
                "CompilerVersion", "v4.0" //do not omit the "v" in front of the version number. It's needed.
            }
        };

        private string _outputAssemblyFileName = "SaturnTestRuns.dll";

        private CompilerParameters m_CompilerParameters;

        private CodeDomProvider _CSharpCodeProvider;

        private CompilerResults _results;

        private Assembly _lastBuiltAssembly;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event CompilerErrorEventHandler CompilerError;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event EventHandler CompilerSuccess;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event TypeCreationErrorEventHandler TypeCreationError;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReflectiveEvaluator"/> class.
        /// </summary>
        /// <param name="assemblyName">Name of the s assembly.</param>
        public ReflectiveEvaluator(string assemblyName = null)
            : this(new CSharpCodeProvider(), assemblyName)
        {
        }

        /// <summary>
        ///     Used to be able to change the runtime compiler version
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="compilerParameters"></param>
        public ReflectiveEvaluator(string assemblyName, Dictionary<string, string> compilerParameters)
            : this(new CSharpCodeProvider(compilerParameters), assemblyName)
        {
        }

        /// <summary>
        ///     Creates a new compiler with given parameters.
        /// </summary>
        /// <returns></returns>
        private CodeDomProvider CreateCompiler (Dictionary<string, string> compilerParameters = null)
        {
            if (compilerParameters == null)
            {
                compilerParameters = _compilerParameters;
            }

            return new CSharpCodeProvider(compilerParameters);
        }

        /// <summary>
        ///     Creates a new compiler.
        /// </summary>
        /// <returns></returns>
        private CodeDomProvider CreateCompiler()
        {
            return new CSharpCodeProvider();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReflectiveEvaluator"/> class.
        /// </summary>
        /// <param name="codeDomProvider">The code DOM provider.</param>
        /// <param name="assemblyName">Name of the s assembly.</param>
        public ReflectiveEvaluator(CodeDomProvider codeDomProvider, string assemblyName = null, bool initializeDefaultDLLs = true)
        {
            this._results = null;
            this.m_CompilerParameters = null;
            this._CSharpCodeProvider = CreateCompiler();
            this.m_CompilerParameters = new CompilerParameters();
            this.m_CompilerParameters.GenerateInMemory = false;
            m_CompilerParameters.OutputAssembly = this._outputAssemblyFileName;

            if (!string.IsNullOrEmpty(assemblyName))
            {
                this.m_CompilerParameters.OutputAssembly = assemblyName;
            }

            this.m_CompilerParameters.GenerateExecutable = false;

            if (initializeDefaultDLLs)
            {
                this.AddDefaultUsings();
            }
        }

        /// <summary>
        /// Gets the default compiler parameters.
        /// </summary>
        public Dictionary<string, string> DefaultCompilerParameters => _compilerParameters;

        /// <summary>
        /// Adds the default usings.
        /// </summary>
        private void AddDefaultUsings()
        {
            AddUsing("system.core.dll");
            AddUsing("System.dll");
            AddUsing("mscorlib.dll");
        }

        /// <summary>
        /// Adds the using.
        /// </summary>
        /// <param name="referenceNames">The sa reference names.</param>
        public void AddUsing(params object[] referenceNames)
        {
            foreach (string referenceName in referenceNames)
            {
                this.m_CompilerParameters.ReferencedAssemblies.Add(referenceName);
            }
        }

        /// <summary>
        ///     Only allow to get default back. Completely empty is not possible.
        /// </summary>
        public void ResetUsings()
        {
            this.m_CompilerParameters.ReferencedAssemblies.Clear();
            AddDefaultUsings();
        }

        /// <summary>
        ///     Evaluates the specified source.
        /// </summary>
        /// <param name="source">The s source.</param>
        /// <param name="type">Type of the s.</param>
        /// <param name="method">The s method.</param>
        /// <returns></returns>
        public object Eval(string source, string type, string method, object[] constructorParameters = null)
        {
            return this.Eval(source, type, method, true, true, constructorParameters);
        }

        //TODO: separate compilation and evaluation.
        /// <summary>
        ///     Evaluatess the specified s source.
        /// </summary>
        /// <param name="source">The s source.</param>
        /// <param name="evaluationType">Type of the s.</param>
        /// <param name="method">The s method.</param>
        /// <param name="recompileNeeded">if set to <c>true</c> [b recompile needed].</param>
        /// <param name="executeWhenCompileFailed">if set to <c>true</c> [b execute when compile failed].</param>
        /// <param name="parameters">a parameter.</param>
        /// <returns></returns>
        public object Eval(string source, string evaluationType, string method, bool recompileNeeded = true, bool executeWhenCompileFailed = true, object[] constructorParameters = null, params object[] parameters)
        {
            bool bHasErrors = false;

            if (recompileNeeded || (this._results == null))
            {
                this._results = this.RuntimeCompileClass(source);

                if (this._results.Errors.HasErrors)
                {
                    bHasErrors = this._results.Errors.HasErrors;
                    this.OnCompilerError(new CompilerErrorEventArgs(this._results.Errors, source));
                    return typeof(void);
                }

                this._lastBuiltAssembly = this._results.CompiledAssembly;
                this.OnCompilerSuccess(new CompilerSuccessEventArgs(this._lastBuiltAssembly));
            }

            if (!executeWhenCompileFailed) return typeof(void);

            Assembly compiledAssembly = this._results.CompiledAssembly;

            if (compiledAssembly == null)
            {
                this.OnCompilerError(new CompilerErrorEventArgs(this._results.Errors, source));
            }

            Type type = compiledAssembly.GetType(evaluationType);

            if (type != null)
            {
                object objectCreatedFromClass = null;

                if (constructorParameters == null)
                {
                    objectCreatedFromClass = Activator.CreateInstance(type);
                }
                else
                {
                    objectCreatedFromClass = Activator.CreateInstance(type, constructorParameters);
                }

                return objectCreatedFromClass.GetType().GetMethod(method).Invoke(objectCreatedFromClass, parameters);
            }

            this.OnTypeCreationError(new TypeCreationErrorEventArgs(evaluationType));
            return null;
        }

        /// <summary>
        ///     Gets the extension methods.
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="extendedType"></param>
        /// <returns></returns>
        public static IEnumerable<MethodInfo> GetExtensionMethods(Assembly assembly, Type extendedType)
        {
            return (
                       from type in assembly.GetTypes()
                       where ( type.IsSealed && !type.IsGenericType ) && !type.IsNested
                       from method in type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
                       where method.IsDefined(typeof(ExtensionAttribute), false)
                       where method.GetParameters()[0].ParameterType == extendedType
                       select method );
        }

        /// <summary>
        /// Raises the <see cref="E:CompilerError" /> event.
        /// </summary>
        /// <param name="e">The <see cref="CompilerErrorEventArgs"/> instance containing the event data.</param>
        protected virtual void OnCompilerError(CompilerErrorEventArgs e)
        {
            CompilerErrorEventHandler compilerError = this.CompilerError;

            if (this.CompilerError != null)
            {
                this.CompilerError(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:CompilerSuccess" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnCompilerSuccess(CompilerSuccessEventArgs e)
        {
            EventHandler compilerSuccess = this.CompilerSuccess;

            if (compilerSuccess != null)
            {
                compilerSuccess(this, e);
            }
        }

        /// <summary>
        ///     Raises the <see cref="E:TypeCreationError" /> event.
        /// </summary>
        /// <param name="e">The <see cref="TypeCreationErrorEventArgs"/> instance containing the event data.</param>
        protected virtual void OnTypeCreationError(TypeCreationErrorEventArgs e)
        {
            if (this.TypeCreationError != null)
            {
                this.TypeCreationError(this, e);
            }
        }

        /// <summary>
        ///     Runtimes the compile class.
        /// </summary>
        /// <param name="source">The s source.</param>
        /// <returns></returns>
        private CompilerResults RuntimeCompileClass(string source)
        {
            return this._CSharpCodeProvider.CompileAssemblyFromSource(this.m_CompilerParameters, source);
        }

        /// <summary>
        ///     Gets the last assembly.
        /// </summary>
        public Assembly LastBuiltAssembly => this._lastBuiltAssembly;

        /// <summary>
        ///     Gets or sets the output assembly file name.
        /// </summary>
        public string OutputAssemblyFileName
        {
            get => this._outputAssemblyFileName;
            set => this._outputAssemblyFileName = value;
        }
    }
}

