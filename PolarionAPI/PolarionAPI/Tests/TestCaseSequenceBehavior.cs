using System.Collections.Generic;

namespace Codan.Argus.TestEnvironment.PolarionAPI.Tests
{
    /// <summary>
    ///     TestCase Sequence Behavior.
    /// </summary>
    public class TestCaseSequenceBehavior
    {
        private Dictionary<string, List<TestCase>> _testCases = null;

        private bool _allowDoubleTestCases = false;

        /// <summary>
        ///     Gets or sets the TestCases
        /// </summary>
        public Dictionary<string, List<TestCase>> TestCases
        {
            get => this._testCases;
            set => this._testCases = value;
        }

        /// <summary>
        ///     Initialize if it is allowed to generate duplicate test entries in polarion or not.
        /// </summary>
        /// <param name="allowDoubleTestCases"></param>
        public TestCaseSequenceBehavior(bool allowDoubleTestCases = true)
        {
            this._allowDoubleTestCases = allowDoubleTestCases;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testCase"></param>
        public void AddTestCases(TestCase testCase)
        {
            if (!this._testCases.ContainsKey(testCase.ID))
            {
                this._testCases.Add(testCase.ID, new List<TestCase>()
                {
                    testCase
                });
            }
            else if (this._testCases.ContainsKey(testCase.ID) && this._allowDoubleTestCases)
            {
                this.TestCases[testCase.ID].Add(testCase);
            }

            if (this._testCases.ContainsKey(testCase.ID) && !_allowDoubleTestCases)
            {
                throw new TestSequenceException(testCase.ID);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool AllowDoubleTestCases
        {
            get => this._allowDoubleTestCases;
            set => this._allowDoubleTestCases = value;
        }
    }
}
