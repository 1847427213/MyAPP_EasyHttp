using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyHttp.Util
{
    public static class HttpUrl
    {
        public static string RootUrl { get; } = "http://81.68.121.75";
        public static class User
        {
            public static string LoginUrl() => $"{RootUrl}/api/Authoize/Login";
        }
        public static class Image
        {
            public static string UploadToAudit() => $"{RootUrl}/api/Image/UploadToAudit";
            public static string GetAuditList(int pagesize, int countsize) => $"{RootUrl}​/api/Image/GetAuditList?pagesize={pagesize}&countsize={countsize}";
            public static string AuditImage() => $"{RootUrl}/api/Image​/AuditImage";
        }

    }
}
