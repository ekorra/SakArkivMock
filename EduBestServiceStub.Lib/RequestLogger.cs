using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EduBestServiceStub.Lib.NoarkTypes;

namespace EduBestServiceStub.Lib
{
    public class RequestLogger : IRequestLogger
    {
        private string logdir;

        public RequestLogger()
        {
            logdir = Directory.GetCurrentDirectory() + @"\RequestLogs";
            if (!Directory.Exists(logdir))
            {
                Directory.CreateDirectory(logdir);
            }
        }

        public void Log(PutMessageRequestType request)
        {
            var logname = $"{request.envelope.sender.orgnr}_{DateTime.Now.GetTimestamp()}";
            string path = $@"{logdir}\{logname}.log";

            File.WriteAllText(path, request.DumpToString());
        }
    }
}
