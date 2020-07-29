using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using net.seabay.polarion.Tracker;

using TrackerService = net.seabay.polarion.Tracker;

namespace Codan.Argus.TestEnvironment.PolarionAPI.Tests
{
    /// <summary>
    /// TestPlan to read about items.
    /// </summary>
    public class TestPlan : PolarionItem
    {
        private Dictionary<string, List<TestCase>> _testCases = null;

        private Dictionary<string, object> _fieldValues = null;

        private static TestCaseSequenceBehavior TestCaseSequenceBehavior { get; set; } = new TestCaseSequenceBehavior(true);

        /// <summary>
        ///     Gets the Test Cases
        /// </summary>
        public Dictionary<string, List<TestCase>> TestCases
        {
            get => this._testCases;
            private set => this._testCases = value;
        }

        /// <summary>
        ///     Gets the workItems of the contained Test Cases.
        /// </summary>
        public TrackerService.WorkItem[] WorkItems
        {
            get
            {
                List<TrackerService.WorkItem> workItems = new List<WorkItem>();

                foreach (KeyValuePair<string, List<TestCase>> testCase in this._testCases)
                {
                    foreach (TestCase currentTestCase in testCase.Value)
                    {
                        workItems.Add(currentTestCase);
                    }
                }

                return workItems.ToArray();
            }
        }

        /// <summary>
        ///     Gets the Test System Sequence if defined. If not defined this is null.
        /// </summary>
        public string TestSystemInstructions => API.GetTextSafe(this._fieldValues, "testSystemInstructions") as string;

        /// <summary>
        ///     Gets the Test System Trigger if defined. If not defined this is null.
        /// </summary>
        public string TestTrigger => API.GetTextSafe(this._fieldValues, "testSystemTrigger") as string;

        /// <summary>
        ///     Gets or sets the Test System Sequence. If defined not this is null.
        /// </summary>
        public string TestSystemSequence
        {
            get => API.GetTextSafe(this._fieldValues, "testSystemSequence");
            set => this._workItem.SetCustomFieldValue("testSystemSequence", value);
        }

        /// <summary>
        ///     Use the id of the Test Plan and if a query should be use
        /// </summary>
        /// <param name="id"></param>
        /// <param name="useQuery"></param>
        public TestPlan(string id, bool useQuery = false)
        {
            this._id = id;

            //Get test plan sequenceField content
            List<string> testPlanFields = API.DefaultFields;
            testPlanFields.Add("customFields.testSystemSequence");
            testPlanFields.Add("customFields.testSystemTrigger");
            testPlanFields.Add("customFields.testSystemInstructions");

            WorkItem testPlanWorkItem = API.GetWorkItemById(id, testPlanFields.ToArray());
            this._workItem = testPlanWorkItem;

            if ( testPlanWorkItem == null)
            {
                return;
            }

            //read the testSequence from the custom field in the testplan.
            TrackerService.Custom[] customFields = API.GetCustomFields(testPlanWorkItem);
            this._fieldValues = API.GetCustomFieldValues(testPlanWorkItem);

            Custom polarionCustomContent = customFields[0];

            //this should be working for the following cases:
            //a) only polarion type link ids are in the text field.
            //b) only SW-DIGITS type links are in the text field.
            //c) mixture of a) and b).
            string testSequence = HtmlHelper.GetTextFromHtmlTextField(polarionCustomContent, false);
            List<string> polarionTypeLinkIDs = HtmlHelper.GetPolarionTypeLinkIDs(testSequence);

            testSequence = HtmlHelper.GetTextFromHtmlTextField(polarionCustomContent, true);
            List<string> ids = new List<string>();
            this.GetIDs(testSequence, ref ids);
            ids.AddRange(polarionTypeLinkIDs);

            string createdQuery = string.Empty;

            if (useQuery)
            {
                createdQuery = string.Join(" OR ", ids);
            }

            List<string> testCaseFields = API.DefaultFields;
            testCaseFields.Add("customFields.testProcedure");
            testCaseFields.Add("customFields.testAssemblies");
            testCaseFields.Add("customFields.testUsings");
            //testCaseFields.Add("customFields.testFQN");

            //And finally add items to the test plan.
            this.AddWorkItems(useQuery, createdQuery, testCaseFields, ids);
        }

        /// <summary>
        /// Adds the WorkItems.
        /// </summary>
        /// <param name="useQuery"></param>
        /// <param name="createdQuery"></param>
        /// <param name="testCaseFields"></param>
        /// <param name="ids"></param>
        private void AddWorkItems (bool useQuery, string createdQuery, List<string> testCaseFields, List<string> ids)
        {
            TrackerService.WorkItem[] workItems = null;

            if (useQuery)
            {
                workItems = API.GetWorkItems(createdQuery, "index", testCaseFields.ToArray());
            }
            else
            {
                workItems = API.GetEachWorkItemByID(testCaseFields, ids.ToArray());
            }

            this._testCases = new Dictionary<string, List<TestCase>>();

            TestPlan.TestCaseSequenceBehavior.TestCases = this.TestCases;

            foreach (TrackerService.WorkItem workItem in workItems)
            {
                TestCase testCase = new TestCase(workItem);

                TestPlan.TestCaseSequenceBehavior.AddTestCases(testCase);
            }
        }

        /// <summary>
        ///     Gets the ids.
        /// </summary>
        /// <param name="testSequence"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        private string GetIDs (string testSequence, ref List<string> ids)
        {
            //build query from content
            //regex expression of the form:
            //<SOMEALPHANUMERICTEXT-NUMBER> - <ITEM TITLE>
            string pattern = @"(\w+-\d+) - ([\w \d()_]+)";
            MatchCollection collection = Regex.Matches(testSequence, pattern);

            foreach (Match match in collection)
            {
                string queryId = match.Groups[1].ToString();
                ids.Add($"{queryId}");
            }

            string createdQuery = string.Join(" OR ", ids.ToArray());
            return createdQuery;
        }

        /// <summary>
        ///     Gets the TestCases
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public TestCase[] this[string ID] => this._testCases[ID].ToArray();
    }
}
