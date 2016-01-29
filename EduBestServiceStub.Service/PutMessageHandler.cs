using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using EduBestServiceStub.Lib.NoarkTypes;

namespace EduBestServiceStub.Service
{
    public class PutMessageHandler
    {
        private readonly PutMessageRequestType putMessageRequest;

        public PutMessageHandler(PutMessageRequestType putMessageRequest)
        {
            this.putMessageRequest = putMessageRequest;
        }

        public PutMessageResponseType GetResponse()
        {
            if (string.IsNullOrEmpty(putMessageRequest.Payload))
            {
                return GetErrorResponse();
            }

           var decoded = HttpUtility.HtmlDecode(putMessageRequest.Payload);
            XDocument doc = XDocument.Parse(decoded);
            
            var journalpost = doc.Descendants("journpost").FirstOrDefault();
            var sak = doc.Descendants("").FirstOrDefault();



            //var melding = GetMelding(putMessageRequest.Payload);


            return GetOkResponse();
        }

       public Stream StringToStream(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }


        private PutMessageResponseType GetOkResponse()
        {
            var response = new PutMessageResponseType
            {
                result = new AppReceiptType
                {
                    message = GetStatuMessage("1", "True"),
                    type = AppReceiptTypeType.OK
                }
            };
            return response;
        }

        private PutMessageResponseType GetErrorResponse()
        {
            var response = new PutMessageResponseType
            {
                result = new AppReceiptType
                {
                    message = GetStatuMessage("0", "Payload missing"),
                    type = AppReceiptTypeType.ERROR
                }
            };
            return response;
        }

        private StatusMessageType[] GetStatuMessage(string code, string text)
        {
            return new[]
            {
                new StatusMessageType
                {
                    code = code,
                    text = text
                }
            };
        }
    }
}