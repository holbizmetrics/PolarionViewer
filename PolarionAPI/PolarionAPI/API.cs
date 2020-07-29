using net.seabay.polarion.Builder;
using net.seabay.polarion.Tracker;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

using TrackerService = net.seabay.polarion.Tracker;

namespace Codan.Argus.TestEnvironment.PolarionAPI
{
    /// <summary>
    /// Polarion API
    /// </summary>
    public static class API
    {
        /// <summary>
        ///     Gets or sets the connection.
        /// </summary>
        public static Connection Connection
        {
            get;
            set;
        }

        private static readonly string polarionProject = "SW";

        /// <summary>
        ///     The default usable fields.
        ///     The fields that are commented out did not work yet.
        /// </summary>
        public static readonly List<string> DefaultFields = new List<string>
        {
            "approvals",
            "assignee",
            "attachments",
            /*"backlinkedWorkItems",*/
            "categories",
            "comments",
            "created",
            "description",
            "dueDate",
            "externallyLinkedWorkItems",
            "hyperlinks",
            "id",
            "initialEstimate",
            "linkedRevisions",
            "linkedRevisionsDerived",
            "linkedWorkItems",
            "location",
            "outlineNumber",
            /*"parentWorkItem",*/
            "plannedIn",
            "plannedEnd",
            "plannedStart",
            "planningConstraints",
            "priority",
            "project",
            "remainingEstimate",
            "resolution",
            "resolvedOn",
            "severity",
            "status",
            /*"suspect",*/
            "timePoint",
            "timeSpent",
            "title",
            "type",
            "updated",
            /*"watchedBy",*/
            "workRecords",
            "customFields.targetVersion"
        };

        /// <summary>
        ///     Any Initialization needed goes here.
        /// </summary>
        static API()
        {
        }

        /// <summary>
        ///     Returns if a connection to Polarion is established.
        /// </summary>
        public static bool IsConnected => Connection?.IsLoggedIn ?? false;

        /// <summary>
        /// Gets the attachment of the WorkItem as a plain object.
        /// </summary>
        /// <param name="workItem">THe WorkItem to read the attachment from.</param>
        /// <returns></returns>
        public static TrackerService.Attachment[] ExtractAttachment(TrackerService.WorkItem workItem)
        {
            return workItem.attachments;
        }

        /// <summary>
        ///     Creates an attachment.
        /// </summary>
        /// <param name="workItem">The WorkItem to upload the attachment to.</param>
        /// <param name="fileName"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        public static void CreateAttachment (TrackerService.WorkItem workItem, string fileName, string title, string content)
        {
            Connection.Tracker.createAttachment(workItem.uri, fileName, title, Encoding.ASCII.GetBytes(content));
        }

        /// <summary>
        ///     Creates an attachment.
        /// </summary>
        /// <param name="workItem">The WorkItem to upload the attachment to.</param>
        /// <param name="title">The title for the attachment.</param>
        /// <param name="filePath">Full qualified filepath of the file to upload.</param>
        public static void CreateAttachment(TrackerService.WorkItem workItem, string title, string filePath)
        {
            TrackerService.Attachment newWorkItem = new TrackerService.Attachment();
            byte[] content = File.ReadAllBytes(filePath);
            Connection.Tracker.createAttachment(workItem.uri, filePath, title, content);
        }

        /// <summary>
        ///     Creates an attachment.
        /// </summary>
        /// <param name="workItem">WorkItem to upload the attachment to.</param>
        /// <param name="fileName">The FileName to upload.</param>
        /// <param name="title">How the item should be called.</param>
        /// <param name="content">The content to upload.</param>
        public static void CreateAttachment (TrackerService.WorkItem workItem, string fileName, string title, byte[] content)
        {
            Connection.Tracker.createAttachment(workItem.uri, fileName, title, content);
        }

        /// <summary>
        ///     Gets WorkItems by query. Sorted by id.
        /// </summary>
        /// <param name="query">The given query.</param>
        /// <param name="sort">Column to be sorted by.</param>
        /// <returns></returns>
        public static TrackerService.WorkItem[] GetWorkItems (string query, string sort = "id")
        {
            return GetWorkItems(query, sort, DefaultFields.ToArray());
        }

        /// <summary>
        ///     Gets all the custom fields as a plain object.
        /// </summary>
        /// <param name="workItem"></param>
        /// <returns></returns>
        public static TrackerService.Custom[] GetCustomFields (TrackerService.WorkItem workItem)
        {
            return workItem.customFields;
        }

        /// <summary>
        ///     Gets WorkItems by query. Sorted by id.
        /// </summary>
        /// <param name="query">The given query.</param>
        /// <param name="fields">The fields</param>
        /// <returns></returns>
        public static TrackerService.WorkItem[] GetWorkItems (string query, params string[] fields)
        {
            return GetWorkItems(query, "id", fields);
        }

        /// <summary>
        ///     Gets the WorkItem by the given id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="project">The project.</param>
        /// <returns></returns>
        public static TrackerService.WorkItem GetWorkItemById (string id, string project = "SW")
        {
            return Connection.Tracker.getWorkItemByIdsWithFields(project, id, DefaultFields.ToArray());
        }

        /// <summary>
        ///     Get WorkItem by id or query
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="fields">The fields and custon fields</param>
        /// <param name="project">given project</param>
        /// <returns></returns>
        public static TrackerService.WorkItem GetWorkItemById (string id, string[] fields, string project = "SW")
        {
            return Connection.Tracker.getWorkItemByIdsWithFields(project, id, fields);
        }

        /// <summary>
        ///     Get all work items by id using the given fields.
        /// </summary>
        /// <param name="fields">Fields and custom fields</param>
        /// <param name="ids">The WorkItem ids.</param>
        /// <returns></returns>
        public static TrackerService.WorkItem[] GetEachWorkItemByID (List<string> fields, params string[] ids)
        {
            List<TrackerService.WorkItem> workItems = new List<TrackerService.WorkItem>();

            foreach (string id in ids)
            {
                TrackerService.WorkItem workItem = API.GetWorkItemById(id, fields.ToArray());
                if (workItem == null) continue;
                workItems.Add(workItem);
            }

            return workItems.ToArray();
        }

        /// <summary>
        ///     Gets each work item by the given id
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static TrackerService.WorkItem[] GetWorkItemsByID (params string[] ids)
        {
            return GetEachWorkItemByID(DefaultFields, ids);
        }

        /// <summary>
        ///     Gets workitems by query, sorted by sort with all the fields given.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="sort"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static TrackerService.WorkItem[] GetWorkItems (string query, string sort = "id", params string[] fields)
        {
            TrackerService.WorkItem[] workItemList = Connection.Tracker.queryWorkItems(query, sort, fields);
            return workItemList;
        }

        /// <summary>
        ///     Creates a new Work Item
        /// </summary>
        /// <returns></returns>
        public static TrackerService.WorkItem CreateWorkItem()
        {
            TrackerService.WorkItem newWorkItem = new TrackerService.WorkItem();
            Connection.Tracker.createWorkItem(newWorkItem);
            return newWorkItem;
        }

        /// <summary>
        ///     Connect to Polarion
        /// </summary>
        /// <param name="URI">The URI.</param>
        /// <param name="user">The User.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static bool Connect (string URI, string user, string password)
        {
            Connection = new Connection(new Uri(URI));
            Connection.Login(user, password);
            return Connection.IsLoggedIn;
        }

        /// <summary>
        ///     Get the custom fields of a WorkItem as Dictionary.
        ///     Eeasier to access that way.
        /// </summary>
        /// <param name="workItem"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetCustomFieldValues (TrackerService.WorkItem workItem)
        {
            Dictionary<string, object> fieldValueAssociation = new Dictionary<string, object>();
            TrackerService.Custom[] customFields = workItem.customFields;

            if (customFields == null)
            {
                return fieldValueAssociation;
            }

            foreach (TrackerService.Custom customField in customFields)
            {
                fieldValueAssociation.Add(customField.key, customField.value);
            }

            return fieldValueAssociation;
        }

        /// <summary>
        ///     Sets the custom field values.
        /// </summary>
        /// <param name="workItem"></param>
        /// <param name="fieldValueAssociation"></param>
        public static void SetCustomFieldValues(TrackerService.WorkItem workItem, Dictionary<string, object> fieldValueAssociation)
        {
            foreach(KeyValuePair<string, object> keyValuePair in fieldValueAssociation)
            {
                TrackerService.Custom custom = workItem.customFields.Where(x => x.key == keyValuePair.Key).FirstOrDefault();
                if (custom != null)
                {
                    custom.value = keyValuePair.Value;
                }
            }
        }

        /// <summary>
        ///     Sets a custom field value.
        /// </summary>
        /// <param name="workItem"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetCustomFieldValue(TrackerService.WorkItem workItem, string key, object value)
        {
            CustomField customField = new CustomField();
            customField.key = key;
            customField.value = value;
            Connection.Tracker.setCustomField(customField);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetTextSafe (TrackerService.Text text)
        {
            if (text != null)
            {
                return text.content;
            }

            return string.Empty;
        }

        /// <summary>
        ///     Returns text only if fieldValues Contains the key keyname.
        /// </summary>
        /// <param name="fieldValues"></param>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static string GetTextSafe (Dictionary<string, object> fieldValues, string keyName)
        {
            if (!fieldValues.ContainsKey(keyName))
            {
                return string.Empty;
            }

            return GetTextSafe(fieldValues[keyName] as TrackerService.Text);
        }

        /// <summary>
        ///     Gets the field content.
        /// </summary>
        /// <param name="customField"></param>
        /// <returns></returns>
        public static string GetFieldContent (TrackerService.Custom customField)
        {
            return ((TrackerService.Text)(customField.value)).content;
        }

        /// <summary>
        ///     Disconnect
        /// </summary>
        /// <returns></returns>
        public static bool Disconnect()
        {
            try
            {
                if (( API.Connection != null ) && ( API.Connection.Session != null ))
                {
                    Connection.Session.endSession();
                }

                Connection.IsLoggedIn = false;
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Demystify());
                return false;
            }
        }

        /// <summary>
        ///     Use this to use the predefined work item types.
        /// </summary>
        /// <param name="workItemType"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static TrackerService.WorkItem CreateWorkItem (WorkItemType workItemType, string title)
        {
            return API.CreateWorkItem(workItemType.ToString(), title);
        }

        /// <summary>
        ///     Creates a work Item
        /// </summary>
        /// <param name="workItemType"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static TrackerService.WorkItem CreateWorkItem (string workItemType, string title = "Test Run", Dictionary<string, object> customFields = null)
        {
            //  create a new WorkItem instance
            TrackerService.WorkItem newWorkItem = new TrackerService.WorkItem();

            //  set project
            newWorkItem.project = new TrackerService.Project();
            newWorkItem.project.uri = Connection.Project.getProject(polarionProject).uri;

            //  set the work item type
            TrackerService.EnumOptionId enumId = new TrackerService.EnumOptionId();
            enumId.id = workItemType;
            newWorkItem.type = enumId;

            //  set the title
            newWorkItem.title = title;

            //  create the work item
            string newWorkItemUri = Connection.Tracker.createWorkItem(newWorkItem);
            newWorkItem.uri = newWorkItemUri;

            //  set the custom fields

            if (customFields != null)
            {
                foreach (KeyValuePair<string, object> currentCustomField in customFields)
                {
                    TrackerService.CustomField customField = new TrackerService.CustomField();
                    customField.key = "targetVersion";
                    customField.parentItemURI = newWorkItemUri;

                    //  set the value, it is of type EnumOptionId[]
                    enumId = new TrackerService.EnumOptionId();
                    enumId.id = "Version_1_0";
                    customField.value = new TrackerService.EnumOptionId[] { enumId };

                    Connection.Tracker.setCustomField(customField);
                }
            }

            //the regex expression matches the term: SW-<multipledigits>
            Match idMatch = Regex.Match(newWorkItemUri, @"SW-\d+");

            //Already set id, so it is not needed to reread the id beforehand.
            if (idMatch.Success)
            {
                newWorkItem.id = idMatch.ToString();
            }

            return newWorkItem;
        }

        /// <summary>
        ///     Reads a Polarion HTML Table.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static List<List<string>> ReadPolarionTable(string text)
        {
            List<List<string>> polarionTable = new List<List<string>>();
            string rawText = text.Replace("<tr>", "$£$<tr>");
            rawText = Regex.Replace(rawText, "\n|\r|  ", "");
            rawText = rawText.Replace("</tr>", "</tr>$£$");
            rawText = rawText.Replace("�", "");
            rawText = rawText.Replace("</th>", "</th>$R$");
            rawText = rawText.Replace("</td>", "</td>$R$");
            //Eliminate HTML code
            rawText = HtmlHelper.StripHtml(rawText);
            rawText = HttpUtility.HtmlDecode(rawText);
            rawText = rawText.Replace("$R$", "\t");
            rawText = rawText.Replace("$£$", "\n");
            List<string> rows = rawText.Split('\n').ToList();

            foreach (string row in rows)
            {
                List<string> columns = row.Split('\t').ToList();

                // Filtered entries at beginning, keep this filter in mind
                if ((columns.FindIndex(x => x.Length > 0) >= 0)
                    && (columns.Count > 1)
                )
                {
                    polarionTable.Add(columns);
                }
            }

            return polarionTable;
        }
    }
}
