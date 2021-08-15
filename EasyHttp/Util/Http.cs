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
        public HttpClient HttpClient;
        public Http()
        {
            HttpClient = Create();
        }
        private CancellationTokenSource TokenSource { get; set; }
        private static HttpClient Create()
        {
            HttpClient HttpClient = new HttpClient()
            {
                Timeout = new TimeSpan(0, 3, 0)
            };
            HttpClient.DefaultRequestHeaders.Add("ContentType", "application/json");
            HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {MyApp.Instance.MyApp_User.Token}");
            return HttpClient;
        }
        public async Task<string> GetAsync(string url)
        {
            TokenSource = new CancellationTokenSource();
            try
            {
                var response = await HttpClient.GetAsync(url, TokenSource.Token);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var request = await response.Content.ReadAsStringAsync();
                    return request;
                }
                else return string.Empty;
            }
            catch (TaskCanceledException)
            {
                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public async Task<string> PostAsync(string url, HttpContent content)
        {
            TokenSource = new CancellationTokenSource();
            try
            {
                var response = await HttpClient.PostAsync(url, content, TokenSource.Token);
                var request = await response.Content.ReadAsStringAsync();
                return request;
            }
            catch (TaskCanceledException)
            {

                return "Task取消成功";
            }
            catch (Exception e)
            {
                return e.Message;
            }
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
        }

        ~Http()
        {
        }
    }
}
