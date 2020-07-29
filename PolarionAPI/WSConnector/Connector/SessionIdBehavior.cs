using System.ServiceModel.Description;

namespace net.seabay.polarion.Connector
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionIdBehavior : IEndpointBehavior
    {
        private SessionIdInspector Inspector { get; set; }


        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public SessionIdBehavior()
        {
            this.Inspector = new SessionIdInspector();
        }

        #endregion Constructor


        #region public Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="bindingParameters"></param>
        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="clientRuntime"></param>
        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(this.Inspector);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="endpointDispatcher"></param>
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(this.Inspector);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        public void Validate(ServiceEndpoint endpoint)
        {
        }

        #endregion public Members
    }
}
