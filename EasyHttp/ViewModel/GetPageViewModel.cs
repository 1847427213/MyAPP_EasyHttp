using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EasyHttp.Util;
using Newtonsoft.Json;
using PropertyChanged;

namespace EasyHttp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class GetPageViewModel
    {
        public Command AddParmCommand { get; set; }
        public Command RequestCommand { get; set; }
        public Command RemoveParmCommand { get; set; }
        public ObservableCollection<KeyValue> Headers { get; set; }
        private ObservableCollection<KeyValue> parameters;
        public ObservableCollection<KeyValue> Parameters
        {
            get => parameters;
            set
            {
                parameters = value;
                var parmstr = Url.Split('?');
                if (parmstr.Length <= 0) Url= "?";
                
                foreach (var item in value)
                {
                    if (string.IsNullOrEmpty(item.KeyParm)) continue;
                    if (parmstr.Length == 1)
                        Url= $"{Url}{item.KeyParm}={item.ValueParm}";
                    else Url = $"{Url}&{item.KeyParm}={item.ValueParm}";
                }
            }
        }
        private string url=string.Empty;
        public string Url
        {
            get => url;
            set
            {
                url = value;
                var parmstr = value.Split('?');
                if (parmstr.Length <= 1) return;
                var parms = parmstr[1].Split('&');
                Parameters = new ObservableCollection<KeyValue>();
                foreach (var item in parms)
                {
                    if (string.IsNullOrEmpty(item)) continue;
                    if (item.Split('=').Length < 2) continue;
                    Parameters.Add(new KeyValue() { KeyParm = item.Split('=')[0], ValueParm = item.Split('=')[1] });
                }
            }
        }
        public GetPageViewModel()
        {
            AddParmCommand = new Command(AddParm);
            RemoveParmCommand = new Command(RemoveParm);
            RequestCommand = new Command(Request);
            Headers = new ObservableCollection<KeyValue>() { new KeyValue() };
            Parameters = new ObservableCollection<KeyValue>() { new KeyValue() };
        }

        private async void Request(object obj)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var item in Headers)
            {
                dic.Add(item.KeyParm, item.ValueParm);
            }
            var jsonStr = JsonConvert.SerializeObject(dic);
            Parm parm = default;
            try
            {
                parm = JsonConvert.DeserializeObject<Parm>(jsonStr);
            }
            catch (Exception)
            {
                MessageBox.Show("Key错误!");
            }

            Http http = new Http(parm);
            var result = await http.GetAsync(Url);
        }

        private void RemoveParm(object obj)
        {
            Headers.Remove((KeyValue)obj);
        }

        private void AddParm(object obj)
        {
            Headers.Add(new KeyValue());
        }
    }
    [AddINotifyPropertyChangedInterface]
    public class KeyValue
    {
        public string KeyParm { get; set; }
        public string ValueParm { get; set; }
    }
    [AddINotifyPropertyChangedInterface]
    public class Parm
    {
        public string ContentType { get; set; } = "application/json";
        public string Token { get; set; }

    }
}
