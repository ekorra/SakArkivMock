using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using EduBestServiceStub.Lib.NoarkTypes;

namespace EduBestServiceStub.Service
{
    public class GetCanRecieveMessageHandler
    {
        private readonly GetCanReceiveMessageRequestType getCanReceiveMessageRequest;
        private string OrgNr { get; set; }

        public GetCanRecieveMessageHandler(GetCanReceiveMessageRequestType getCanReceiveMessageRequest)
        {
            this.getCanReceiveMessageRequest = getCanReceiveMessageRequest;
            OrgNr = Regex.Replace(getCanReceiveMessageRequest.receiver.orgnr, @"\s+", "");
        }

        public GetCanReceiveMessageResponseType GetResponse()
        {
            var result = new GetCanReceiveMessageResponseType { result = IsOrgNr() || IsPersonnummer() };
            return result;
        }

        private bool IsOrgNr()
        {
            return IsOrgNrNumeric() && OrgNr.Length == 9;
        }

        private bool IsPersonnummer()
        {
            return IsOrgNrNumeric() && OrgNr.Length == 11;
        }

        private bool IsOrgNrNumeric()
        {
            return Regex.IsMatch(OrgNr, @"^\d+$");
        }
    }
}