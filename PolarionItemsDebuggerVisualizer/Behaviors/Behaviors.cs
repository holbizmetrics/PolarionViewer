using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TrackerService = net.seabay.polarion.Tracker;

namespace PolarionItemsDebuggerVisualizer.Behaviors
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Behavior : IBehavior
    {
        protected TrackerService.WorkItem _workItem = null;
        public Behavior(TrackerService.WorkItem workItem)
        {
            _workItem = workItem;
        }
        public abstract string Text { get; }
    }
}
