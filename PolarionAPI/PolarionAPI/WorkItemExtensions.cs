using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using TrackerService = net.seabay.polarion.Tracker;

namespace Codan.Argus.TestEnvironment.PolarionAPI
{
    /// <summary>
    /// 
    /// </summary>
    public static class WorkItemExtensions
    {
        /// <summary>
        /// Test if workItem is really valid.
        /// </summary>
        /// <param name="workItem"></param>
        /// <returns></returns>
        public static bool IsValidItem(this TrackerService.WorkItem workItem)
        {
            return ( workItem.id != null) && ( workItem.title != null );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workItem"></param>
        /// <param name="fileName"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        public static void CreateAttachment(this TrackerService.WorkItem workItem, string fileName, string title, string content)
        {
            API.CreateAttachment(workItem, fileName, title, Encoding.ASCII.GetBytes(content));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workItem"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetCustomFieldValue(this TrackerService.WorkItem workItem, string key, string value)
        {
            API.SetCustomFieldValue(workItem, key, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workItem"></param>
        /// <param name="customFields"></param>
        public static void SetCustomFieldValues(this TrackerService.WorkItem workItem, Dictionary<string, object> customFields)
        {
            API.SetCustomFieldValues(workItem, customFields);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workItem"></param>
        /// <param name="title"></param>
        /// <param name="filePath"></param>
        public static void CreateAttachment(this TrackerService.WorkItem workItem, string title, string filePath)
        {
            API.CreateAttachment(workItem, title, filePath);
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="workItem"></param>
        /// <param name="fileName"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        public static void CreateAttachment(this TrackerService.WorkItem workItem, string fileName, string title, byte[] content)
        {
            API.CreateAttachment(workItem, fileName, title, content);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workItem"></param>
        /// <returns></returns>
        public static TrackerService.Attachment[] ExtractAttachment(this TrackerService.WorkItem workItem)
        {
            return API.ExtractAttachment(workItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workItem"></param>
        /// <param name="index"></param>
        /// <param name="downloadedFileName"></param>
        /// <returns></returns>
        public static FileInfo DownloadAttachment(this TrackerService.WorkItem workItem, int index, string password, string downloadedFileName)
        {
            TrackerService.Attachment attachment = API.ExtractAttachment(workItem)[index];
            WebClient webClient = new WebClient();
            webClient.Credentials = new NetworkCredential(API.Connection.UserName, password);
            webClient.DownloadFile(attachment.url, downloadedFileName);
            return new FileInfo(downloadedFileName);

        }
    }
}