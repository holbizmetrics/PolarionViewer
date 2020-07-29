using Codan.Argus.TestEnvironment.PolarionAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TrackerService = net.seabay.polarion.Tracker;

namespace PolarionViewerLibrary
{
    public class PolarionViewerAPI
    {
        public static PolarionItem[] CreateItems(string type, string text)
        {
            string[] lines = Regex.Split(text, "\n");

            List<PolarionItem> polarionItems = new List<PolarionItem>();
            foreach(string line in lines)
            {
                string changedLine = line.Replace("/", "").TrimStart(' ');
                TrackerService.WorkItem workItem = API.CreateWorkItem("type", changedLine);
                PolarionItem polarionItem = new PolarionItem(workItem.id);
                polarionItems.Add(polarionItem);
            }
            return polarionItems.ToArray();
        }
    }
}