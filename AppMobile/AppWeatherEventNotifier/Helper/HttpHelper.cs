using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using AppWeatherEventNotifier.Models;
using AppWeatherEventNotifier.Services.RestController;

namespace AppWeatherEventNotifier.Helper
{
    public class HttpHelper
    {
        public static async Task<T> HttpPostRequest<T>(string endPoint, object inputData, bool needAuth,string parameters=null) where T : new()
        {
         

            var connection = Connectivity.NetworkAccess;
            if (connection != NetworkAccess.Internet)
            {
               // if(typeof(T)!=typeof(PingResponse))
                   //  await Application.Current.MainPage.DisplayAlert("Attenzione", "Internet NON disponibile..", "Ok");
                return default;
            }
            T outputData = new T();
            string coding = "en-GB";
            HttpClient client = new HttpClient();
            using (client)
            {
                try
                {
                    string url = string.Concat(Globals.server, endPoint);

                    if (parameters != null)
                        url = string.Concat(url, "?", parameters);
                    var langStr = Preferences.Get("selectedLanguage", null);
                    if (langStr != null)
                    {
                        coding = JsonConvert.DeserializeObject<Language>(langStr).Coding;
                    }
                    using var request = new HttpRequestMessage(new HttpMethod("POST"), url);
                    request.Headers.TryAddWithoutValidation("accept", "application/octet-stream");
                    request.Headers.TryAddWithoutValidation("lang", coding);
                    if (TodoItemDatabase.Instance.Token != null && needAuth)
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", TodoItemDatabase.Instance.Token);
                    if (inputData != null)
                    {
                        string jsonString = JsonConvert.SerializeObject(inputData);
                        request.Content = new StringContent(jsonString);
                        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    }

                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var resString = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(resString);
                        if (typeof(T) == typeof(GenericResponse))
                        {
                            var tmpJson = new GenericResponse { Message = resString, Result = true };
                            resString = JsonConvert.SerializeObject(tmpJson);
                        }
                        outputData = JsonConvert.DeserializeObject<T>(resString);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest || response.StatusCode == System.Net.HttpStatusCode.InternalServerError || response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        //string msg = JsonConvert.DeserializeObject<ErrorMessage>(result).message;
                        if (typeof(T) == typeof(GenericResponse))
                        {
                            var tmpJson = new GenericResponse { Result = false, Message = result };
                            var tmpstr = JsonConvert.SerializeObject(tmpJson);
                            outputData = JsonConvert.DeserializeObject<T>(tmpstr);
                        }
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            try
                            {
                                if (string.IsNullOrEmpty(result))
                                    result = "Si è verificato un errore ricaricare la pagina";
                                Application.Current.MainPage.DisplayAlert("Attenzione", result, "Ok");
                            }
                            catch (Exception ex)
                            {
                                //Crashes.TrackError(ex);
                            }
                        });
                        return default;
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {

                        if (Globals.page_current != "Login")
                        {
                            Globals.Logout();
                            AppWeatherEventNotifier.App.Current.MainPage = new NavigationPage(new AppWeatherEventNotifier.Views.Login.LoginPage());
                            await Application.Current.MainPage.DisplayAlert("Attenzione", "Sessione scaduta", "Ok");
                        }
                        throw new Exception();
                    }
                    //lancio eccezione generica
                    else
                        throw new Exception();
                }
                catch (HttpRequestException ex)
                {
                    Dictionary<string, string> paramsEx = new Dictionary<string, string>();
                    if (ex.InnerException is WebException e && e.Status ==
                        WebExceptionStatus.TrustFailure)
                    {
                        paramsEx.Add("POTENTIAL ATTACK", "The server certificate validation failed: potential attack  ");
                    }
                    else
                    {
                        // Other network issues  
                    }
                //    Crashes.TrackError(ex, paramsEx);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                   // Crashes.TrackError(ex);
                }
            }
            return outputData;
        }
        public static async Task<T> HttpPatchRequest<T>(string endPoint, object inputData, bool needAuth, string parameters = null) where T : new()
        {


            var connection = Connectivity.NetworkAccess;
            if (connection != NetworkAccess.Internet)
            {
                // if(typeof(T)!=typeof(PingResponse))
                //  await Application.Current.MainPage.DisplayAlert("Attenzione", "Internet NON disponibile..", "Ok");
                return default;
            }
            T outputData = new T();
            string coding = "en-GB";
            HttpClient client = new HttpClient();
            using (client)
            {
                try
                {
                    string url = string.Concat(Globals.server, endPoint);

                    if (parameters != null)
                        url = string.Concat(url, "?", parameters);
                    var langStr = Preferences.Get("selectedLanguage", null);
                    if (langStr != null)
                    {
                        coding = JsonConvert.DeserializeObject<Language>(langStr).Coding;
                    }
                    using var request = new HttpRequestMessage(new HttpMethod("PATCH"), url);
                    request.Headers.TryAddWithoutValidation("accept", "application/octet-stream");
                    request.Headers.TryAddWithoutValidation("lang", coding);
                    if (TodoItemDatabase.Instance.Token != null && needAuth)
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", TodoItemDatabase.Instance.Token);
                    if (inputData != null)
                    {
                        string jsonString = JsonConvert.SerializeObject(inputData);
                        request.Content = new StringContent(jsonString);
                        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    }

                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var resString = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(resString);
                        if (typeof(T) == typeof(GenericResponse))
                        {
                            var tmpJson = new GenericResponse { Message = resString, Result = true };
                            resString = JsonConvert.SerializeObject(tmpJson);
                        }
                        outputData = JsonConvert.DeserializeObject<T>(resString);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest || response.StatusCode == System.Net.HttpStatusCode.InternalServerError || response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        //string msg = JsonConvert.DeserializeObject<ErrorMessage>(result).message;
                        if (typeof(T) == typeof(GenericResponse))
                        {
                            var tmpJson = new GenericResponse { Result = false, Message = result };
                            var tmpstr = JsonConvert.SerializeObject(tmpJson);
                            outputData = JsonConvert.DeserializeObject<T>(tmpstr);
                        }
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            try
                            {
                                if (string.IsNullOrEmpty(result))
                                    result = "Si è verificato un errore ricaricare la pagina";
                                Application.Current.MainPage.DisplayAlert("Attenzione", result, "Ok");
                            }
                            catch (Exception ex)
                            {
                                //Crashes.TrackError(ex);
                            }
                        });
                        return default;
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {

                        if (Globals.page_current != "Login")
                        {
                            Globals.Logout();
                            AppWeatherEventNotifier.App.Current.MainPage = new NavigationPage(new AppWeatherEventNotifier.Views.Login.LoginPage());
                            await Application.Current.MainPage.DisplayAlert("Attenzione", "Sessione scaduta", "Ok");
                        }
                        throw new Exception();
                    }
                    //lancio eccezione generica
                    else
                        throw new Exception();
                }
                catch (HttpRequestException ex)
                {
                    Dictionary<string, string> paramsEx = new Dictionary<string, string>();
                    if (ex.InnerException is WebException e && e.Status ==
                        WebExceptionStatus.TrustFailure)
                    {
                        paramsEx.Add("POTENTIAL ATTACK", "The server certificate validation failed: potential attack  ");
                    }
                    else
                    {
                        // Other network issues  
                    }
                    //    Crashes.TrackError(ex, paramsEx);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    // Crashes.TrackError(ex);
                }
            }
            return outputData;
        }
        public static async Task<T> HttpDeleteRequest<T>(string endPoint,  bool needAuth, string parameters = null) where T : new()
        {
            var connection = Connectivity.NetworkAccess;
            if (connection != NetworkAccess.Internet)
            {
                // Handle no internet access
                return default;
            }

            T outputData = new T();
            string coding = "en-GB";
            HttpClient client = new HttpClient();

            try
            {
                string url = string.Concat(Globals.server, endPoint);

                if (parameters != null)
                    url = string.Concat(url, "?", parameters);

                var langStr = Preferences.Get("selectedLanguage", null);
                if (langStr != null)
                {
                    coding = JsonConvert.DeserializeObject<Language>(langStr).Coding;
                }

                using var request = new HttpRequestMessage(HttpMethod.Delete, url);
                request.Headers.TryAddWithoutValidation("accept", "application/octet-stream");
                request.Headers.TryAddWithoutValidation("lang", coding);

                if (TodoItemDatabase.Instance.Token != null && needAuth)
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", TodoItemDatabase.Instance.Token);

             

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var resString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(resString);
                    if (typeof(T) == typeof(GenericResponse))
                    {
                        var tmpJson = new GenericResponse { Message = resString, Result = true };
                        resString = JsonConvert.SerializeObject(tmpJson);
                    }
                    outputData = JsonConvert.DeserializeObject<T>(resString);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest || response.StatusCode == System.Net.HttpStatusCode.InternalServerError || response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    if (typeof(T) == typeof(GenericResponse))
                    {
                        var tmpJson = new GenericResponse { Result = false, Message = result };
                        var tmpstr = JsonConvert.SerializeObject(tmpJson);
                        outputData = JsonConvert.DeserializeObject<T>(tmpstr);
                    }

                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        try
                        {
                            if (string.IsNullOrEmpty(result))
                                result = "Si è verificato un errore, ricaricare la pagina";
                            Application.Current.MainPage.DisplayAlert("Attenzione", result, "Ok");
                        }
                        catch (Exception ex)
                        {
                            // Handle UI-related exceptions
                        }
                    });
                    return default;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    if (Globals.page_current != "Login")
                    {
                        Globals.Logout();
                        AppWeatherEventNotifier.App.Current.MainPage = new NavigationPage(new AppWeatherEventNotifier.Views.Login.LoginPage());
                        await Application.Current.MainPage.DisplayAlert("Attenzione", "Sessione scaduta", "Ok");
                    }
                    throw new Exception();
                }
                // Handle other status codes as needed
                else
                {
                    throw new Exception();
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle specific exceptions (e.g., network-related issues)
            }
            catch (Exception ex)
            {
                // Handle other exceptions
            }
            finally
            {
                client.Dispose();
            }

            return outputData;
        }
        public static async Task<T> HttpGetRequest<T>(string endPoint,bool needAuth=false) where T : new()
        {

            var connection = Connectivity.NetworkAccess;
            if (connection != NetworkAccess.Internet)
            {
               // await Application.Current.MainPage.DisplayAlert("Attenzione", "Internet NON disponibile..", "Ok");
                return default;
            }
            T outputData = new T();
            HttpClient client= new HttpClient();
            string coding = "en-GB";
            using (client)
            {
                try
                {
                    client.DefaultRequestHeaders.Clear();
                    var langStr = Preferences.Get("selectedLanguage", null);
                    if (langStr != null)
                    {
                        coding = JsonConvert.DeserializeObject<Language>(langStr).Coding;
                    }
                    client.DefaultRequestHeaders.TryAddWithoutValidation("lang", coding);
                    if (TodoItemDatabase.Instance.Token != null && needAuth)
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TodoItemDatabase.Instance.Token);
                    var url = string.Concat(Globals.server, endPoint);
                    Console.WriteLine(url);
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var resString = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(resString);
                        if (typeof(T) == typeof(GenericResponse))
                        {
                            var tmpJson = new GenericResponse { Message = resString, Result = true };
                            resString = JsonConvert.SerializeObject(tmpJson);
                        }
                        outputData = JsonConvert.DeserializeObject<T>(resString);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {

                        if (Globals.page_current != "Login")
                        {
                            Globals.Logout();
                            AppWeatherEventNotifier.App.Current.MainPage = new NavigationPage(new AppWeatherEventNotifier.Views.Login.LoginPage());
                            await Application.Current.MainPage.DisplayAlert("Attenzione", "Sessione scaduta", "Ok");
                        }
                        throw new Exception(response.StatusCode.ToString());
                    }
                    //lancio eccezione generica
                    else
                        throw new Exception(response.StatusCode.ToString());
                }
                catch (HttpRequestException ex)
                {
                    Dictionary<string, string> paramsEx = new Dictionary<string, string>();
                    if (ex.InnerException is WebException e && e.Status ==
                        WebExceptionStatus.TrustFailure)
                    {
                        paramsEx.Add("POTENTIAL ATTACK", "The server certificate validation failed: potential attack  ");
                    }
                    else
                    {
                        // Other network issues  
                    }
                //    Crashes.TrackError(ex, paramsEx);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    return default;
                }
            }
            return outputData;
        }
        internal class ErrorMessage
        {
            public string message { get; set; }
        }
    }
}
