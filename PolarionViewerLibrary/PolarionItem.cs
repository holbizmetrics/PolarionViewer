using System.Collections.Generic;
using System.Text.RegularExpressions;
using Codan.Argus.TestEnvironment.PolarionAPI;
using PolarionItemsDebuggerVisualizer.Behaviors;
using TrackerService = net.seabay.polarion.Tracker;

namespace PolarionViewerLibrary
{
    /// <summary>
    /// 
    /// </summary>
    public class PolarionItem
    {
        private TrackerService.WorkItem _workItem = null;
        private List<PolarionItem> _childs = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public PolarionItem(string id = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                return;
            }

            _workItem = API.GetWorkItemById(id);
        }

        public string Title => _workItem.title;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workItem"></param>
        private PolarionItem(TrackerService.WorkItem workItem)
        {
            _workItem = workItem;
        }

        /// <summary>
        /// 
        /// </summary>
        public TrackerService.WorkItem WorkItem => _workItem;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static PolarionItem[] GetItemsFromQuery(string query, bool filterEmpty = true)
        {
            List<PolarionItem> polarionItems = null;

            TrackerService.WorkItem[] workItems = API.GetWorkItems(query);
            if (workItems == null || workItems.Length == 0) return null;

            polarionItems = new List<PolarionItem>();

            foreach (TrackerService.WorkItem workItem in workItems)
            {
                if (filterEmpty)
                {
                    if (string.IsNullOrEmpty(workItem.title))
                    {
                        continue;
                    }
                }
                polarionItems.Add(new PolarionItem(workItem));
            }
            return polarionItems.ToArray();
        }

        /// <summary>
        ///     returns the id.
        /// </summary>
        public string ID => _workItem.id;

        /// <summary>
        /// 
        /// </summary>
        public List<PolarionItem> Childs
        {
            get
            {
                if (_childs == null)
                {
                    _childs = new List<PolarionItem>();
                }

                _childs.Clear();

                if (_workItem.linkedWorkItems == null) return _childs;

                foreach (TrackerService.LinkedWorkItem workItem in _workItem.linkedWorkItems)
                {
                    Match match = Regex.Match(workItem.workItemURI, @"(\${WorkItem})([A-Z-]+\d+)");
                    string id = match.Groups[2].ToString();
                    _childs.Add(new PolarionItem(id));
                }

                return _childs;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public TrackerService.PlanningConstraint[] PlanningConstraints => _workItem.planningConstraints;

        /// <summary>
        /// 
        /// </summary>
        public TrackerService.LinkedWorkItem[] LinkedWorkItemsDerived => _workItem.linkedWorkItemsDerived;

        /// <summary>
        /// 
        /// </summary>
        public TrackerService.Revision[] LinkedRevisionsDerived => _workItem.linkedRevisionsDerived;

        /// <summary>
        /// 
        /// </summary>
        public TrackerService.Revision[] LinkedRevisions => _workItem.linkedRevisions;

        /// <summary>
        /// 
        /// </summary>
        public TrackerService.Category[] Categories => _workItem.categories;

        /// <summary>
        /// 
        /// </summary>
        public TrackerService.Attachment[] Attachments => _workItem.attachments;

        /// <summary>
        /// 
        /// </summary>
        public TrackerService.User[] User => _workItem.assignee;

        /// <summary>
        /// 
        /// </summary>
        public TrackerService.Comment[] Comments => _workItem.comments;

        /// <summary>
        /// 
        /// </summary>
        public TrackerService.Approval[] Approvals => _workItem.approvals;

        /// <summary>
        /// 
        /// </summary>
        public TrackerService.Hyperlink[] Hyperlinks => _workItem.hyperlinks;

        /// <summary>
        /// 
        /// </summary>
        public TrackerService.LinkedWorkItem[] LinkedWorkItems => this._workItem.linkedWorkItems;

        /// <summary>
        /// 
        /// </summary>
        public TrackerService.ExternallyLinkedWorkItem[] ExternallyLinkedWorkItems => this._workItem.externallyLinkedWorkItems;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{_workItem.id}-{_workItem.title}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public virtual string ToString(string format)
        {
            return string.Empty;
        }

        public virtual string TextualRepresentation(bool completeOrOnlyWorkItem)
        {
            if (completeOrOnlyWorkItem)
            {
                return ObjectDumper.Dump(this);
            }
            else
            {
                return ObjectDumper.Dump(_workItem);
            }
        }
    }
}
