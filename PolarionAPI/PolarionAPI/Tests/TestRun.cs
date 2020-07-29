using System.Collections.Generic;

namespace Codan.Argus.TestEnvironment.PolarionAPI.Tests
{
    public class TestRun : PolarionItem
    {
        public List<TestCase> TestCases => this.TestCases;

        public TestRun ()
        {

        }

        //public List<TestResult> TestResults => TestResults;
        /// <inheritdoc />
        public override string ToString ()
        {
            return $"{this._id}-{this.Title}";
        }
    }
}
