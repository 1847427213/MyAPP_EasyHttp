using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EasyHttp.Model;
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
        public bool IsFile { get; set; }
        public Visibility ShowFile { get; set; } = Visibility.Collapsed;
        public Visibility ShowJson { get; set; } = Visibility.Visible;
        public ResponseModel responseModel { get; set; }
        public ImageSource ImageSource { get; set; }
        public ResponseWindowViewModel(HttpResponseMessage response)
        {
            if (response != null)
            {
                responseModel = new ResponseModel();
                responseModel.IsFile = !response.Content.Headers.ContentType.MediaType.Equals("application/json");
                if (responseModel.IsFile)
                {
                    ShowFile = Visibility.Visible;
                    ShowJson = Visibility.Collapsed;
                }
                else
                {
                    ShowFile = Visibility.Collapsed;
                    ShowJson = Visibility.Visible;
                }
                responseModel.Etag = response.Headers.ETag?.Tag;
                responseModel.FileName = response.Content.Headers.ContentDisposition?.FileName;
                ParseResponse(response);
            }
        }
        private async void ParseResponse(HttpResponseMessage response)
        {
            Response = StrFormat(response.ToString());
            if (responseModel.IsFile)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                var fileType = FileHeader.GetStreamType(stream);
                if (fileType.Equals("unknow"))
                {

                }
                else if (fileType.Equals("jpg"))
                {
                    stream.Position = 0;
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                    ImageSource = bitmapImage;
                }
                else if (fileType.Equals("zip"))
                {

                }
            }
            else
            {
                Content = JsonFormat(await response.Content.ReadAsStringAsync());
            }

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
            try
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
            catch (Exception)
            {
                return json;
            }
        }
    }
    [AddINotifyPropertyChangedInterface]
    public class ResponseModel
    {
        public bool IsFile { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string Etag { get; set; }

    }
}
