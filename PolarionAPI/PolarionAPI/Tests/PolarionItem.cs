using System.Collections.Generic;

using TrackerService = net.seabay.polarion.Tracker;

namespace Codan.Argus.TestEnvironment.PolarionAPI.Tests
{
    public abstract class PolarionItem : IPolarionItem
    {
        protected string _id = null;

        protected TrackerService.WorkItem _workItem = null;

        protected Dictionary<string, object> _fieldValues = null;

        /// <inheritdoc />
        public override string ToString ()
        {
            if (this._workItem != null)
            {
                return $"{this._workItem.id}-{this._workItem.title}";
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the work Item ID.
        /// </summary>
        public string ID => this._workItem.id;

        /// <summary>
        /// Gets the work item title
        /// </summary>
        public string Title => this._workItem.title;

        /// <summary>
        /// Returns the underlying Polarion WorkItem
        /// </summary>
        public TrackerService.WorkItem WorkItem => this._workItem;
    }
}
