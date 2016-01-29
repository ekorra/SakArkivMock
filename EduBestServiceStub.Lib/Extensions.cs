using System;
using System.IO;
using Newtonsoft.Json;

namespace EduBestServiceStub.Lib
{
    public static class Dumper
    {
        public static string DumpToString(this object value)
        {
            return JsonConvert.SerializeObject(value, Formatting.Indented);
        }
    }

}