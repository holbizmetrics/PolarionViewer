using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace net.seabay.polarion.Connector
{
    /// <summary>
    ///     SessionIdInspector
    /// </summary>
    public class SessionIdInspector : IClientMessageInspector, IDispatchMessageInspector
    {
        private string _httpWsPolarionCommunicationSession = "http://ws.polarion.com/session";

        private long SessionId { get; set; }


        #region IDispatchMessageInspector Members

        /// <summary>
        ///     AfterReceiveRequest
        /// </summary>
        /// <param name="request"></param>
        /// <param name="channel"></param>
        /// <param name="instanceContext"></param>
        /// <returns></returns>
        object IDispatchMessageInspector.AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            return null;
        }

        /// <summary>
        ///     BeforeSendReply
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="correlationState"></param>
        /// <returns></returns>
        void IDispatchMessageInspector.BeforeSendReply(ref Message reply, object correlationState)
        {
        }

        #endregion


        #region IClientMessageInspector Members

        /// <summary>
        ///     AfterReceive Reply
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="correlationState"></param>
        /// <returns></returns>
        void IClientMessageInspector.AfterReceiveReply(ref Message reply, object correlationState)
        {
            this._httpWsPolarionCommunicationSession = "http://ws.polarion.com/session";
            int index= reply.Headers.FindHeader("sessionID", this._httpWsPolarionCommunicationSession);

            if (index != -1)
            {
                XmlDictionaryReader reader = reply.Headers.GetReaderAtHeader(0);
                string sessionId = reader.ReadInnerXml();
                this.SessionId = Convert.ToInt64(sessionId);
            }
        }

        /// <summary>
        ///     BeforeSendRequest
        /// </summary>
        /// <param name="request"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        object IClientMessageInspector.BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            if (this.SessionId != 0)
            {
                request.Headers.Add(MessageHeader.CreateHeader("sessionID", this._httpWsPolarionCommunicationSession, this.SessionId));
            }

            return null;
        }

        #endregion
    }
}
