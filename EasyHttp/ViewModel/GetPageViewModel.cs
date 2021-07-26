using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EasyHttp.Util;
using EasyHttp.View;
using Newtonsoft.Json;
using PropertyChanged;

namespace EasyHttp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class GetPageViewModel
    {
        public Command AddHeaderCommand { get; set; }
        public Command RemoveHeaderCommand { get; set; }
        public Command AddParmCommand { get; set; }
        public Command RemoveParmCommand { get; set; }
        public Command RequestCommand { get; set; }
        public Command CancelCommand { get; set; }
        //请求头
        public ObservableCollection<string> a { get; set; }
        public ObservableCollection<KeyValue> Headers { get; set; }
        //请求参数
        public ObservableCollection<KeyValue> Parameters { get; set; }
        //请求地址
        public string Url { get; set; } = string.Empty;
        public Visibility ShowPro { get; set; } = Visibility.Collapsed;
        private Http _http { get; set; }

        public GetPageViewModel()
        {
            AddHeaderCommand = new Command(AddHeader);
            RemoveHeaderCommand = new Command(RemoveHeader);
            AddParmCommand = new Command(AddParm);
            RemoveParmCommand = new Command(RemoveParm);
            RequestCommand = new Command(Request);
            CancelCommand = new Command(Cancel);
            Headers = new ObservableCollection<KeyValue>()
            {
                new KeyValue() {Key="ContentType",Value="application/json"},
                new KeyValue() {Key="Authorization",Value="Bearer "}
            };
            Parameters = new ObservableCollection<KeyValue>() { new KeyValue() };
            a = new ObservableCollection<string>() { "1", "2", "3" };
        }
        /// <summary>
        /// 取消请求
        /// </summary>
        /// <param name="obj"></param>
        private void Cancel(object obj)
        {
            if (_http.Cancel())
            {
                ShowPro = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("取消失败，请稍后");
            }
        }

        /// <summary>
        /// 移除头
        /// </summary>
        /// <param name="obj"></param>
        private void RemoveHeader(object obj)
        {
            Headers.Remove((KeyValue)obj);
        }
        /// <summary>
        /// 添加头
        /// </summary>
        /// <param name="obj"></param>
        private void AddHeader(object obj)
        {
            Headers.Add(new KeyValue());
        }
        /// <summary>
        /// 删除参数
        /// </summary>
        /// <param name="obj"></param>
        private void RemoveParm(object obj)
        {
            Parameters.Remove((KeyValue)obj);
        }
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="obj"></param>
        private void AddParm(object obj)
        {
            Parameters.Add(new KeyValue());
        }
        /// <summary>
        /// 请求
        /// </summary>
        /// <param name="obj"></param>
        private async void Request(object obj)
        {
            ShowPro = Visibility.Visible;
            _http = new Http(Url, Headers.Count > 0 ? Headers : null, Parameters.Count > 0 ? Parameters : null);
            var result = await _http.GetAsync();
            ResponseWindow responseWindow = new ResponseWindow(_http.Response);
            responseWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            responseWindow.ShowDialog();
            //MessageBox.Show(result);
            ShowPro = Visibility.Collapsed;
        }
    }
    [AddINotifyPropertyChangedInterface]
    public class KeyValue
    {
        public KeyValue()
        { 
        
        }
        public KeyValue(string Key, string Value)
        {
            this.Key = Key;
            this.Value = Value;
        }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
