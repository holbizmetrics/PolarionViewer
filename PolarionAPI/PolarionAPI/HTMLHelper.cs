using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

using TrackerService = net.seabay.polarion.Tracker;

namespace Codan.Argus.TestEnvironment.PolarionAPI
{
    /// <summary>
    /// 
    /// </summary>
    public static class HtmlHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlText"></param>
        /// <param name="decode"></param>
        /// <returns></returns>
        public static string StripHtml(string htmlText, bool decode = true)
        {
            Regex regex = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            string stripped = regex.Replace(htmlText, "");
            return decode ? HttpUtility.HtmlDecode(stripped) : stripped;
        }

        /// <summary>
        ///     Used to extract Polarion Type Links.
        /// </summary>
        /// <param name="xmlOrHtml"></param>
        /// <remarks>This type of links only has html attributes. But no inner text or inner html.
        /// Therefore the attributes need to be read.</remarks>
        /// <returns></returns>
        public static List<string> GetPolarionTypeLinkIDs(string xmlOrHtml)
        {
            List<string> linkIDs = new List<string>();
            MatchCollection matches = Regex.Matches(xmlOrHtml, @"(class=""polarion-rte-link"")[ ]+(data-type=""[a-z]+"")[ \t]+(id=""fake"")[ ]+(data-item-id=""SW-\d+"") data-option-id=""long""", RegexOptions.IgnoreCase);

            foreach (Match match in matches)
            {
                string idPartOfLink = match.Groups[4].Value;
                Match idMatch = Regex.Match(idPartOfLink, @"(data-item-id)=(""SW-\d+"")");
                string linkId = idMatch.Groups[2].Value;
                linkId = linkId.Replace(@"""", @""); //strip the quotes
                linkIDs.Add(linkId);
            }

            return linkIDs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetTextFromHtmlText(string text)
        {
            text = HtmlHelper.StripHtml(text);
            return text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customField"></param>
        /// <returns></returns>
        public static string GetTextFromHtmlTextField(TrackerService.Custom customField, bool stripHtml = true)
        {
            string text = ((TrackerService.Text)(customField.value)).content;

            if (stripHtml)
            {
                text = HtmlHelper.StripHtml(text);
            }

            return text;
        }
    }
}
