using System.Xml.Serialization;
using EduBestServiceStub.Lib.NoarkTypes;

namespace EduBestServiceStub.Service
{
    public interface INoarkExchange
    {
        /// <remarks/>
        GetCanReceiveMessageResponseType GetCanReceiveMessage([XmlElement(Namespace="http://www.arkivverket.no/Noark/Exchange/types")] GetCanReceiveMessageRequestType GetCanReceiveMessageRequest);

        /// <remarks/>
        PutMessageResponseType PutMessage([XmlElement(Namespace="http://www.arkivverket.no/Noark/Exchange/types")] PutMessageRequestType PutMessageRequest);

        string Url { get; set; }
    }
}