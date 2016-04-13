using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using EduBestServiceStub.Lib.NoarkTypes;
using log4net;

namespace EduBestServiceStub.Lib
{
    public class RequestLogger : IRequestLogger
    {
        private string logdir;
        private ILog log;

        public RequestLogger()
        {
            log = LogManager.GetLogger(typeof(RequestLogger));
            EnsureLogDirectroy();
        }

        private void EnsureLogDirectroy()
        {
            logdir = WebConfigurationManager.AppSettings["RequestLogPath"];

            if (!Directory.Exists(logdir))
            {
                Directory.CreateDirectory(logdir);
            }

            log.Info($"Request loger initialized to path: {logdir}");
        }

        public void Log(PutMessageRequestType request)
        {
            var logname = $"{request.envelope.sender.orgnr}_{DateTime.Now.GetTimestamp()}";
            string path = $@"{logdir}\{logname}.log";

            File.WriteAllText(path, request.DumpToString());
        }
    }
}
