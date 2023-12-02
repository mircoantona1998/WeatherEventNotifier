namespace TelegramService
{
    class TelegramService
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                TimeZoneInfo fusoOrarioItalia = TimeZoneInfo.FindSystemTimeZoneById("Europe/Rome");
                DateTime oraadesso = TimeZoneInfo.ConvertTime(DateTime.UtcNow, fusoOrarioItalia);
                Console.WriteLine("start at the: " + oraadesso);
                Jobs jobs = new Jobs();
                try
                {
                    Utils utils = new Utils();
                    utils.initialize();
                    bool start = await jobs.ToWork();
                    if (start)
                    {
                        await jobs.start_job();
                        List<TelegramMessage> result = await utils.prepareTelegramFromDB();
                        bool x = false;
                        if (result != null)
                            x = await utils.SendTelegram(result);
                        else Console.WriteLine("No Telegram message to send!");
                        await jobs.refresh_status(false);

                        if (x == false) { await jobs.refresh_status(true); }
                    }
                    else
                    {
                        Console.WriteLine("Non devo lavorare");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    await jobs.refresh_status(false);
                }
                oraadesso = TimeZoneInfo.ConvertTime(DateTime.UtcNow, fusoOrarioItalia);
                Console.WriteLine("finish at the: " + oraadesso);
                Thread.Sleep(5000);
            }
        }

    }
}

