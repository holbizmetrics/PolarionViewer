using System.Collections.Generic;

using TrackerService = net.seabay.polarion.Tracker;

namespace Codan.Argus.TestEnvironment.PolarionAPI.Tests
{
    /// <summary>
    ///     Test Case
    /// </summary>
    public class TestCase : PolarionItem
    {
        /// <summary>
        /// Uses workitem and reads the custom field values using the dictionary method from the Polarion API.
        /// </summary>
        /// <param name="workItem"></param>
        public TestCase(TrackerService.WorkItem workItem)
        {
            this._workItem = workItem;
            this._fieldValues = API.GetCustomFieldValues(workItem);
        }

        //public static explicit operator TrackerService.WorkItem(TestCase testCase) => testCase._workItem;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testCase"></param>
        public static implicit operator TrackerService.WorkItem(TestCase testCase) => testCase._workItem;

        /// <summary>
        /// Gets the usings if defined. If not this is null.
        /// </summary>
        public string Usings => API.GetTextSafe(this._fieldValues, "testUsings") as string;

        /// <summary>
        /// Gets the assemblies if defined. If not this is null.
        /// </summary>
        public string Assemblies => API.GetTextSafe(this._fieldValues, "testAssemblies") as string;

        /// <summary>
        /// Gets the test procedure if defined. If not this is null.
        /// </summary>
        public string TestProcedure => API.GetTextSafe(this._fieldValues, "testProcedure") as string;

        public string TestFullyQualifiedName => API.GetTextSafe(this._fieldValues, "testFQN") as string;
    }
}
