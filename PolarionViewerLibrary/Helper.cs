using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace PolarionViewerLibrary
{
    /// <summary>
    /// 
    /// </summary>
    public static class Helper
    {
        public static  string test =
@"<span class=""polarion-rte-link"" data-type=""workItem"" id=""fake"" data-item-id=""SW-1104"" data-option-id=""long""></span><br/>
 <span class=""polarion-rte-link"" data-type=""workItem"" id=""fake"" data-item-id=""SW-1784"" data-option-id=""long""></span><br/>
 <span class=""polarion-rte-link"" data-type=""workItem"" id=""fake"" data-item-id=""SW-1783"" data-option-id=""long""></span><br/>
 <span class=""polarion-rte-link"" data-type=""workItem"" id=""fake"" data-item-id=""SW-1785"" data-option-id=""long""></span> <br/>
 <span class=""polarion-rte-link"" data-type=""workItem"" id=""fake"" data-item-id=""SW-1786"" data-option-id=""long""></span> <br/>
<span class=""polarion-rte-link"" data-type=""workItem"" id=""fake"" data-item-id=""SW-1838"" data-option-id=""long""></span><br/>";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlOrHtml"></param>
        /// <returns></returns>
        public static List<string> GetPolarionTypeLinkIDs(string xmlOrHtml)
        {
            List<string> linkIDs = new List<string>();
            MatchCollection matches = Regex.Matches(xmlOrHtml, @"(class=""polarion-rte-link"")[ ]+(data-type=""[a-z]+"")[ \t]+(id=""fake"")[ ]+(data-item-id=""SW-\d+"") data-option-id=""long""", RegexOptions.IgnoreCase);
            foreach (Match match in matches)
            {
                string idPartOfLink = match.Groups[4].Value;
                Match idMatch = Regex.Match(xmlOrHtml, @"(data-item-id)=(""SW-\d+"")");
                string linkId = idMatch.Groups[2].Value;
                linkIDs.Add(linkId);
            }

            return linkIDs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="internalPolarionURI"></param>
        /// <returns></returns>
        public static string GetURI(string internalPolarionURI)
        {
            // Pattern: https://polarion.codan.de/polarion/#/project/SW/workitem?id=SW-1785
            Uri uri = new Uri(internalPolarionURI);

            Match match = Regex.Match(uri.ToString(), @"SW-\d+");
            if (match.Success)
            {
                return $"https://polarion.codan.de/polarion/#/project/SW/workitem?id={match.ToString()}";
            }
            return string.Empty;
        }
    }
}