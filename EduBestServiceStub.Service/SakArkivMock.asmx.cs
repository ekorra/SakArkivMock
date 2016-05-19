﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Services;
using System.Web.Services.Protocols;
using EduBestServiceStub.Lib;
using EduBestServiceStub.Lib.NoarkTypes;
using EduBestServiceStub.Slack;
using log4net;
using log4net.Config;

namespace EduBestServiceStub.Service
{


    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "noarkExchangeBinding", Namespace = "http://www.arkivverket.no/Noark/Exchange")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(PutJournpostResponseType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(PutJournpostRequestType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(GetJournpostResponseType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(GetJournpostRequestType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(PutNoarksakResponseType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(PutNoarksakRequestType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(GetNoarksakResponseType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(GetNoarksakRequestType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(MeldingType))]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [WebService(Namespace = "http://www.arkivverket.no/Noark/Exchange")]
    [SoapDocumentService(RoutingStyle = SoapServiceRoutingStyle.RequestElement)]
    public class NoarkExchange : WebService
    {
        private ILog log;
        private readonly IRequestLogger requestLogger;
        private readonly bool logRequest;
        private string slackUrl;
        private string slackChennel;

        public NoarkExchange()
        {
            XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(NoarkExchange));
            logRequest = bool.Parse(WebConfigurationManager.AppSettings["LogRequest"]);
            slackUrl = WebConfigurationManager.AppSettings["SlackUrl"];
            slackChennel = WebConfigurationManager.AppSettings["SlackChannel"];
            log.Info($"LogRequests : {logRequest}");

            if (logRequest)
            {
                try
                {
                    requestLogger = new RequestLogger();
                }
                catch (Exception e)
                {
                    log.Error("Failed to initiate requestlogger", e);
                }
                
            }
            
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("GetCanReceiveMessageResponse", Namespace = "http://www.arkivverket.no/Noark/Exchange/types")]
        [WebMethod(MessageName = "GetCanReceiveMessageResponse")]
        public GetCanReceiveMessageResponseType GetCanReceiveMessage(
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.arkivverket.no/Noark/Exchange/types")] GetCanReceiveMessageRequestType GetCanReceiveMessageRequest)
        {
            log.Info(GetCanReceiveMessageRequest.DumpToString());
            return new GetCanRecieveMessageHandler(GetCanReceiveMessageRequest).GetResponse();
        }


        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("PutMessageResponse", Namespace = "http://www.arkivverket.no/Noark/Exchange/types")]
        [WebMethod(MessageName = "PutMessageResponse")]

        public PutMessageResponseType PutMessage(
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.arkivverket.no/Noark/Exchange/types")] PutMessageRequestType PutMessageRequest)
        {
            log.Info("Got request");
            requestLogger?.Log(PutMessageRequest);
            var noarkExchangeClient = new noarkExchange { Url = Resource.IntegrasjonspunktUrl };
            var slackNotifier = new SlackNotifier(slackUrl, slackChennel);
            var result = new PutMessageHandler(noarkExchangeClient, slackNotifier).HandleRequest(PutMessageRequest);
            log.Info("returning respone");
            return result;
        }

    }
}