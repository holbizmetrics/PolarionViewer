using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerService = net.seabay.polarion.Tracker;

namespace PolarionItemsDebuggerVisualizer.Behaviors
{
    public class DefaultBehavior : Behavior
    {
        public DefaultBehavior(TrackerService.WorkItem workItem)
            : base(workItem)
        {

        }
        public override string Text { get { 
                
string textualRepresentation =
$@"title:       \t{_workItem.title}
assignee:       \t{_workItem.assignee}
author:         \t{_workItem.author}
created:        \t{_workItem.created}
description:    \t{_workItem.description}
due date:       \t{_workItem.dueDate}
id:             \t{_workItem.id}
initial est:    \t{_workItem.initialEstimate}
location:       \t{_workItem.location}
module URI:     \t{_workItem.moduleURI}
outline num:    \t{_workItem.outlineNumber}
planned end:    \t{_workItem.plannedEnd}
planned start:  \t{_workItem.plannedStart}
priority:       \t{_workItem.priority}
remaining est:  \t{_workItem.remainingEstimate}
resolved on:    \t{_workItem.resolvedOn}
time spent:     \t{_workItem.timeSpent}
type:           \t{_workItem.type.ToString()}
updated:        \t{_workItem.updated}
uri:            \t{_workItem.uri}
";
                return textualRepresentation;
            }
        }
    }
}
