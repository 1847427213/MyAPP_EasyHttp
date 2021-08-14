using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyHttp.Util
{
    public static class HttpUrl
    {
        public static string RootUrl { get; } = "http://81.68.121.75/api/";
        public static class User
        {
            public static string LoginUrl { get; } = $"{RootUrl}Authoize/Login";
        }

    }
}
