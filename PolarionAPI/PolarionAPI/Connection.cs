
using System;
using Codan.Argus.Polarion;
using net.seabay.polarion.Tracker;
using net.seabay.polarion.Builder;
using net.seabay.polarion.Project;
using net.seabay.polarion.Planning;
using net.seabay.polarion.Security;
using net.seabay.polarion.Session;
using System.Diagnostics;
using System.ServiceModel;

namespace Codan.Argus.TestEnvironment.PolarionAPI
{
    /// <summary>
    /// 
    /// </summary>
    public class Connection
    {
        #region private Properties

        private WebServiceConnector Factory { get; set; }

        private net.seabay.polarion.Project.User PolarionUser { get; set; }

        private string Password { get; set; }

        #endregion private Properties


        #region public Properties

        public string Protocol { get; private set; }

        public string Server { get; private set; }

        public string UserID => PolarionUser.id;

        public string UserName => PolarionUser.name;

        public bool IsLoggedIn { get; internal set; }

        public Uri Uri { get; set; }

        #endregion public Properties


        //  WebServices
        public BuilderWebServiceClient Builder => Factory.WebServices[WebServiceFactory.Builder];

        public ProjectWebServiceClient Project => Factory.WebServices[WebServiceFactory.Project];

        public PlanningWebServiceClient Planning => Factory.WebServices[WebServiceFactory.Planning];

        public SecurityWebServiceClient Security => Factory.WebServices[WebServiceFactory.Security];

        public SessionWebServiceClient Session => Factory.WebServices[WebServiceFactory.Session];

        //public TestManagementWebServiceClient TestManagement => Factory.WebServices[WebServiceFactory.TestManagement];
        public TrackerWebServiceClient Tracker => Factory.WebServices[WebServiceFactory.Tracker];


        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseUri"></param>
        public Connection(Uri baseUri)
        {
            Uri = baseUri;

            Connect(baseUri);
        }

        private void Connect(Uri baseUri)
        {
            Factory = new WebServiceConnector(baseUri);
            Factory.Connect();
            IsLoggedIn = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="protocol"></param>
        /// <param name="server"></param>
        public Connection(string protocol, string server)
        {
            Protocol = protocol;
            Server = server;

            Factory = new WebServiceConnector(protocol, server);
            Factory.Connect();
            IsLoggedIn = false;
        }

        #endregion Constructor


        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        public void Login (string user, string password)
        {
            try
            {
                Session.LogIn(user, password);
                PolarionUser = Project.getUser(user);
                IsLoggedIn = true;
            }
            catch (ProtocolException ex)
            {
                //  user could not log in
                IsLoggedIn = false;
                Debug.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                //  user could not log in
                IsLoggedIn = false;
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
