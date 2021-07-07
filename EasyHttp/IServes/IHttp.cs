using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EasyHttp.IServes
{
    public interface IHttp
    {
        Task<string> GetAsync(string url);
        Task<string> PostAsync(string url, HttpContent content);
        bool Cancel();
    }
}
