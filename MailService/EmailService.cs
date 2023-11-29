using EmailService.Configuration;
using EmailService.Model;

namespace EmailService
{
    class EmailService
    {
        static async Task Main(string[] args)
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
                    List<Mail> result = await utils.prepareMailsFromDB();
                    bool x=false;
                    if (result != null )
                        x = utils.SendEmail(result);
                    else Console.WriteLine("No emails to send!");
                        await jobs.refresh_status(false);

                    if(x==false) { await jobs.refresh_status(true); }
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
        }    
    }
}

