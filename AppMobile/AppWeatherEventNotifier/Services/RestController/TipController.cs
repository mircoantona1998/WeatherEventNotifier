using AppWeatherEventNotifier.Helper;
using AppWeatherEventNotifier.Models;
using AppWeatherEventNotifier.Views.Login;

namespace AppWeatherEventNotifier.Services.RestController
{
     class TipController
     {    
        public static async Task<bool> getTipsUserLastHour()
        {
            string endPoint = "/getUserTips?lastHourOnly=true";
            var res = await Helper.HttpHelper.HttpGetRequest<List<Tip>>(endPoint, true);
            TodoItemDatabase.Instance.TipsUser = res;
            if(TodoItemDatabase.Instance.TipsUser != null )
            foreach (Tip x in TodoItemDatabase.Instance.TipsUser)
            {
                x.Message_descr = x.TipMessage;
                if (x.CodeType == "pvprodmin")
                {
                    x.icon = "pvprodmin" + ".png";
                    x.BackGround = Color.FromArgb("#FFA500");
                }
                else if (x.CodeType == "podconsmax")
                {
                    x.icon = "podconsmax" + ".png";
                    x.BackGround = Color.FromArgb("#FFA500");
                }
                else if (x.CodeType == "podconsmin")
                {
                    x.icon = "podconsmin" + ".png";
                    x.BackGround = Color.FromArgb("#FFA500");
                }
                else if (x.CodeType == "pvprodmax")
                {
                    x.icon = "pvprodmax" + ".png";
                    x.BackGround = Color.FromArgb("#73BF69");
                }
                else if (x.CodeType == "podconsgood")
                {
                    x.icon = "podconsgood" + ".PNG";
                    x.BackGround = Color.FromArgb("#73BF69");
                }
                else if (x.CodeType == "podnodata")
                {
                    x.icon = "podnodata" + ".png";
                    x.BackGround = Color.FromArgb("#FF7383");
                }
                else if (x.CodeType == "CEnodata")
                {
                    x.icon = "cenodata" + ".png";
                    x.BackGround = Color.FromArgb("#FF7383");
                }
                else if (x.CodeType == "pvprodzero")
                {
                    x.icon = "pvprodzero" + ".png";
                    x.BackGround = Color.FromArgb("#FF7383");
                }
                else if (x.CodeType == "podmax")
                {
                    x.icon = "podmax" + ".png";
                    x.BackGround = Color.FromArgb("#FF7383");
                }
                else if (x.CodeType == "washingMachineScheduler")
                {
                    x.icon = "lavatrice" + ".png";
                    x.BackGround = Color.FromArgb("#11A98E");
                }
                    //x.icon = "lavatrice" + ".png";
                    //x.Title = "Planning Washing Machine";
                    //x.Message = "New washing schedule is reccomended, new date Jul 25 2023 7:31 PM Energy need 400";
                    //x.Description = "";
                    //x.TipMessage = "Plan to use the washing machine at the most convenient time";
                    //x.BackGround = Color.FromArgb("#FFA500");
                    if (x.CodeType == "washingMachineScheduler")
                {
                    x.responseToDo = true;
                }
                else x.responseToDo = false;
            }
            return true;       
        }
     }
}
