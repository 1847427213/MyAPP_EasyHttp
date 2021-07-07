using EasyHttp.IServes;
using EasyHttp.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace EasyHttp.Util
{
    public class Http : IHttp
    {
        public HttpClient HttpClient { get; private set; }
        public CancellationTokenSource TokenSource { get; private set; }
        public HttpResponseMessage Response { get; private set; }
        public string Request { get; private set; }
        public string Url { get; private set; }

        public Http(string url) : this(url, null, null) { }
        public Http(string url, IEnumerable<KeyValue> headers) : this(url, headers, null) { }
        public Http(string url, IEnumerable<KeyValue> headers, IEnumerable<KeyValue> parameters)
        {
            HttpClient = new HttpClient()
            {
                Timeout = new TimeSpan(0, 3, 0)
            };
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    if (string.IsNullOrEmpty(item.Key) || string.IsNullOrEmpty(item.Value))
                        continue;
                    HttpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }
            StringBuilder paramsStr = new StringBuilder("?");
            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    if (string.IsNullOrEmpty(item.Key))
                        continue;
                    paramsStr.Append($"{item.Key}={item.Value}");
                }
            }
            string str = paramsStr.Length > 1 ? paramsStr.ToString() : string.Empty;
            Url = $"{url}{str}";
        }
        public async Task<string> GetAsync()
        {
            if (string.IsNullOrEmpty(Url))
            {
                return "Url为空";
            }
            TokenSource = new CancellationTokenSource();
            try
            {
                Response = await HttpClient.GetAsync(Url, HttpCompletionOption.ResponseContentRead, TokenSource.Token);
                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Request = await Response.Content.ReadAsStringAsync();
                }
                else Request = Response.ToString();
                return Request;
            }
            catch (Exception e)
            {
                return e.Message;
            }
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
            HttpClient?.Dispose();
            TokenSource?.Dispose();
            Response?.Dispose();
        }

        ~Http()
        {
            HttpClient?.Dispose();
            TokenSource?.Dispose();
            Response?.Dispose();
        }
    }
}
