using TelegramService.Configuration;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;

public class Utils
{
    public Utils() { }
    public void initialize()
    {
        string json = File.ReadAllText("Configuration.json");
        Globals.config = JsonConvert.DeserializeObject<Configuration>(json);
    }
    public  async Task<List<TelegramMessage>> prepareTelegramFromDB()
    {
        List<TelegramMessage> result = new List<TelegramMessage>();
        string bot = "";
        string token = "";
        try
        {
            Console.WriteLine("Query to set config telegram from");
            using (MyDbContext context = new MyDbContext())
            {
                List<TelegramConfiguration> entities = await context.TelegramConfiguration
                    .ToListAsync();
                foreach (TelegramConfiguration entity in entities)
                {
                    bot = entity.bot;
                    token= entity.token;
                    break;
                }
            }         
            using (MyDbContext context = new MyDbContext())
            {
                List<TelegramMessage> entities = await context.TelegramMessages
                    .Where(telegram=> telegram.WasSent==false)
                    .ToListAsync();
                foreach (TelegramMessage entity in entities)
                {
                    if (entity.IdChat != null) 
                    {     
                            try 
                            {        
                                     result.Add(entity);
                            }
                                  
                            catch (Exception ex) {
                                updateTelegramSent(entity.Id, 
                                    "Error " +entity.Id + " destinatario:" + entity.IdChat );
                            }
                        }
                    }
                }
            }
        
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
        return result;
    }
    public async Task<bool> SendTelegram(List<TelegramMessage> Telegram)
    {
        foreach (TelegramMessage telegram in Telegram)
        {
            try
            {
                var botClient = new TelegramBotClient(Globals.config.BotToken);
                try
                {
                    await botClient.SendTextMessageAsync(telegram.IdChat, telegram.Testo);
                    Console.WriteLine("Messaggio inviato con successo!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Errore durante l'invio del messaggio: {ex.Message}");
                }
                Console.WriteLine("Telegram sent successfully to: " + telegram.IdChat + "!");
                    bool v=updateTelegramSent(telegram.Id,"Was sent to:"+telegram.IdChat);
                if(v==false) { return false; }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Telegram NOT sent to: " + telegram.IdChat + " ERROR: " + ex.Message);
                    updateTelegramSent(telegram.Id,
                        "Error " + "destinatario:" + telegram.IdChat );
                        return false;
            }       
        }
        if (Telegram.Count <= 0)
            Console.WriteLine("No Telegrams messages to send!");
        return true;
    }
    public  bool updateTelegramSent(int idTelegram,string result)
    {
        try
        {
            TimeZoneInfo fusoOrarioItalia = TimeZoneInfo.FindSystemTimeZoneById("Europe/Rome");
            DateTime oraadesso = TimeZoneInfo.ConvertTime(DateTime.UtcNow, fusoOrarioItalia);
            using (MyDbContext context = new MyDbContext())
            {
                var entityToUpdate =  context.TelegramMessages
                .Find(idTelegram);
                    
                if (entityToUpdate != null)
                {
                entityToUpdate.WasSent = true;
                if(entityToUpdate.Result!=null && entityToUpdate.Result!= string.Empty)
                     entityToUpdate.Result = entityToUpdate.Result+","+result;
                else entityToUpdate.Result = result;
                    entityToUpdate.dateSent = oraadesso;
                    context.SaveChangesAsync();
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
        return false;
    }
}

