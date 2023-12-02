
//using Plugin.XamarinFormsSaveOpenPDFPackage;
using AppWeatherEventNotifier.Models;
using AppWeatherEventNotifier.Services.Events;
using AppWeatherEventNotifier.Services.Notifications;
using AppWeatherEventNotifier.ViewModels;
using AppWeatherEventNotifier.Views.Login;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;


namespace AppWeatherEventNotifier.Helper
{
    public static class Globals
    {
        public static DateTime lastMqttRequest=DateTime.Now;
        public static DateTime lastRefreshTokenRequest = DateTime.Now;
        public static bool canHttp = true;
        public static bool first=false;
        public static bool IsConnected = false;
        public static bool MqttStatus = false;
        public static string server=Constants.SERVER_COLLAUDO;
        public static string page_current = "";

        public static HomeViewModel homeViewModel;

        public static void send_notification(string title, string message)
        {
            object o = new object();
            EventArgs e = new EventArgs();
            EventMessage.RunEvents(o,e);
            INotificationManager notificationManager;
            notificationManager = DependencyService.Get<INotificationManager>();
            notificationManager.SendNotification(title, message);
        }

        public static Task save_photo(string nameFile,string newDirectory, byte[] bytes)
        {
            DirectoryInfo d = new DirectoryInfo(DelegateFunction.funcGetStoreCameraDir());        
            var imagePath1 = Path.Combine(d.FullName, newDirectory);
            if (!System.IO.Directory.Exists(imagePath1))
            {
                Directory.CreateDirectory(imagePath1);
            }
            var imagePath = Path.Combine(imagePath1, nameFile+ ".png");
            System.IO.File.WriteAllBytes(imagePath, bytes);
            return Task.CompletedTask;
        }
        public static byte[] streamToByteArray(Stream input)
        {
            MemoryStream ms = new MemoryStream();
            input.CopyTo(ms);
            return ms.ToArray();
        }
        public static async Task  pdf(Stream stream, string filename)
        {
            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
            }
        }
       
        public static void Logout()
        {
            TodoItemDatabase.Instance.IsLoggedIn = false;
            TodoItemDatabase.Instance.Token = null;
            TodoItemDatabase.Instance.UserId = null;
            TodoItemDatabase.Instance.Username = string.Empty;
            TodoItemDatabase.Instance.UsernameEntry= string.Empty;
            TodoItemDatabase.Instance.PasswordEntry = string.Empty;
        }

    }
}
