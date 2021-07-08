using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PropertyChanged;

namespace EasyHttp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class ResponseWindowViewModel
    {
        public string Response { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Request { get; set; } = string.Empty;
        public ResponseWindowViewModel(HttpResponseMessage response)
        {
            if (response != null)
            {
                ParseResponse(response);
            }
        }
        private async void ParseResponse(HttpResponseMessage response)
        {
            Response = StrFormat(response.ToString());
            Content = JsonFormat(await response.Content.ReadAsStringAsync());
            Request = StrFormat(response.RequestMessage.ToString());
        }
        private string StrFormat(string str)
        {
            var returnStr = string.Empty;
            if (!string.IsNullOrEmpty(str))
            {
                var para = str.Split(',');
                foreach (var item in para)
                {
                    returnStr += item.Trim() + "\r\n";
                }
            }
            return returnStr;
        }
        private string JsonFormat(string json)
        {
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(json);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return json;
            }
        }
    }
}
