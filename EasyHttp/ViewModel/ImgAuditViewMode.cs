using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using EasyHttp.Util;
using EasyHttp.View.ImagePages;
using Newtonsoft.Json.Linq;
using PropertyChanged;

namespace EasyHttp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class ImgAuditViewMode
    {
        public int CountSize { get; set; }
        public Command PassCommand { get; set; }
        public Command BackCommand { get; set; }
        public Command RefreshCommand { get; set; }
        public Command LookImageCommand { get; set; }
        public ObservableCollection<ImgAuditModel> ImgAuditModels { get; set; }
        public ImgAuditViewMode()
        {
            PassCommand = new Command(Pass);
            BackCommand = new Command(Back);
            RefreshCommand = new Command(Refresh);
            LookImageCommand = new Command(LookImage);
            ImgAuditModels = new ObservableCollection<ImgAuditModel>();
        }

        private void LookImage(object obj)
        {
            var item = obj as ImgAuditModel;
            if (item == null) return;
            App.ImgNavigation.Navigation(new LookImagePage(item.ImageSource));
        }

        private void Refresh(object obj)
        {
            ImgAuditModels.Clear();
            GetImgAuditList();
        }

        private async void Back(object obj)
        {
            var item = obj as ImgAuditModel;
            if (item == null) return;
            item.Auditstate = -1;
            if (await SubmitAudit(item))
            {
                MessageBox.Show("审核完成");
                ImgAuditModels.Remove(item);
            }
            else MessageBox.Show("审核失败");
        }

        private async void Pass(object obj)
        {
            var item = obj as ImgAuditModel;
            if (item == null) return;
            item.Auditstate = 1;
            if (await SubmitAudit(item))
            {
                MessageBox.Show("审核完成");
                ImgAuditModels.Remove(item);
            }
            else MessageBox.Show("审核失败");
        }
        private async Task<bool> SubmitAudit(ImgAuditModel model)
        {
            Http http = new Http();
            MultipartFormDataContent formDataContent = new MultipartFormDataContent();
            formDataContent.Add(new StringContent(model.Auditid.ToString()), "auditid");
            formDataContent.Add(new StringContent(model.Auditstate.ToString()), "state");
            formDataContent.Add(new StringContent(model.Auditnote), "note");
            formDataContent.Add(new StringContent(model.Permissions.Tag.ToString()), "permissions");
            var result = await http.PostAsync(HttpUrl.Image.AuditImage(), formDataContent);
            if (string.IsNullOrEmpty(result)) return false;
            return true;
        }

        public async void GetImgAuditList()
        {
            Http http = new Http();
            var result = await http.GetAsync(HttpUrl.Image.GetAuditList(ImgAuditModels.Count, CountSize));
            foreach (var item in ImgAuditModel.GetImgAuditList(result))
            {
                ImgAuditModels.Add(item);
                await Task.Delay(10);
            }
        }
    }
    [AddINotifyPropertyChangedInterface]
    public class ImgAuditModel
    {
        public string ImageSource { get; set; }
        public string ImageThumbnail { get; set; }
        public string Audittime { get; set; }
        public string Auditnote { get; set; }
        public int Auditid { get; set; }
        public int Auditstate { get; set; }
        public string UserNickname { get; set; }
        public string UserAccount { get; set; }
        public ComboBoxItem Permissions { get; set; }
        public Visibility ShowLook { get; set; } = Visibility.Collapsed;
        public static List<ImgAuditModel> GetImgAuditList(string result)
        {
            List<ImgAuditModel> models = new List<ImgAuditModel>();
            if (!string.IsNullOrEmpty(result))
            {
                JObject jobject = JObject.Parse(result);
                foreach (var item in jobject.Value<JArray>("body"))
                {
                    models.Add(new ImgAuditModel()
                    {
                        ImageSource = $"{ HttpUrl.RootUrl }{item.Value<string>("auditsource")}",
                        ImageThumbnail = $"{ HttpUrl.RootUrl }{item.Value<string>("auditthumbnail")}",
                        Audittime = item.Value<string>("audittime"),
                        Auditnote = item.Value<string>("auditnote") == null ? string.Empty : item.Value<string>("auditnote"),
                        Auditid = item.Value<int>("auditid"),
                        Auditstate = item.Value<int>("auditstate"),
                        UserAccount = item.Value<string>("userAccount"),
                        UserNickname = item.Value<string>("userNickname")
                    });
                }
            }
            return models;
        }
    }
}
