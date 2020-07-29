using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TrackerService = net.seabay.polarion.Tracker;

namespace PolarionItemsDebuggerVisualizer.Behaviors
{
    public class ObjectDumperBehavior : Behavior
    {
        public ObjectDumperBehavior(TrackerService.WorkItem workItem)
            : base(workItem)
        { 
        }
        public override string Text
        {
            get { return ObjectDumper.Dump(_workItem); }
        }
    }
}
