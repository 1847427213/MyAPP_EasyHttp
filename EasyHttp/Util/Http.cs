using EasyHttp.IServes;
using EasyHttp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasyHttp.Util
{
    public class Http : IHttp
    {
        public HttpClient HttpClient { get; private set; }
        public CancellationTokenSource TokenSource { get; private set; }
        public HttpResponseMessage Response { get; private set; }
        public string Request { get; private set; }
        public Http(Parm parm/* string Token, string MediaType = "application/json"*/)
        {
            HttpClient = new HttpClient();
            HttpClient.Timeout = new TimeSpan(0,3,0);
            HttpClient.DefaultRequestHeaders.Add("ContentType", parm.ContentType);
            HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {parm.Token}");
        }

        public async Task<string> GetAsync(string url)
        {
            TokenSource = new CancellationTokenSource();
            Response = await HttpClient.GetAsync(url, HttpCompletionOption.ResponseContentRead, TokenSource.Token);
            Request = await Response.Content.ReadAsStringAsync();
            return Request;
        }

        public async Task<string> PostAsync(string url, HttpContent content)
        {
            TokenSource = new CancellationTokenSource();
            Response = await HttpClient.PostAsync(url, content, TokenSource.Token);
            Request = await Response.Content.ReadAsStringAsync();
            return Request;
        }
        public bool Cancel()
        {
            TokenSource.Cancel();
            if (TokenSource.IsCancellationRequested)
                return true;
            return false;
        }
        private void Dispose()
        {
            HttpClient.Dispose();
            TokenSource.Dispose();
            Response.Dispose();
        }

        ~Http()
        {
            HttpClient.Dispose();
            TokenSource.Dispose();
            Response.Dispose();
        }
    }
}
