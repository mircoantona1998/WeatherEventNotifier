using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using EmailService.Configuration;
using Newtonsoft.Json;
using EmailService.Model;
using Microsoft.EntityFrameworkCore;


public class Utils
{
    public Utils() { }
    public void initialize()
    {
        string json = File.ReadAllText("Configuration.json");
        Globals.config = JsonConvert.DeserializeObject<Configuration>(json);
    }
    public  async Task<List<Mail>> prepareMailsFromDB()
    {
        List<Mail> result = new List<Mail>();
        string mailFrom = "";
        string fromName = "";
        string password = "";
        try
        {
            Console.WriteLine("Query to set config mail from");
            using (MyDbContext context = new MyDbContext())
            {
                List<MailConfiguration> entities = await context.MailConfiguration
                    .ToListAsync();
                foreach (MailConfiguration entity in entities)
                {
                    mailFrom = entity.mail;
                    fromName = entity.name;
                    password = entity.password;
                    break;
                }
            }
               
            using (MyDbContext context = new MyDbContext())
            {
                List<Mail> entities = await context.Mail
                    .Where(mail=> mail.WasSent==false)
                    .ToListAsync();

                foreach (Mail entity in entities)
                {
                    if (entity.Destinatario != null) 
                    { 
                        string[] substrings = entity.Destinatario.Replace(" ","").Split(',');
                        foreach (string destinatario in substrings)
                        {
                            try { 
                            if (destinatario != null && destinatario.Replace(" ", "") != "" && destinatario.Contains("@")
                                && mailFrom != "" && password != "" && fromName != "")
                            {
                                MailComplete mail = new MailComplete();
                                mail.Oggetto = entity.Oggetto;
                                mail.Testo = entity.Testo;
                                mail.Destinatario = destinatario.Replace(" ","");
                                mail.mail = mailFrom;
                                mail.password = password;
                                mail.name = fromName;
                                mail.Id = entity.Id;
                                result.Add(mail);
                            }
                            else if(destinatario != null && destinatario == "") { }
                            else
                            {
                                Console.WriteLine("Saltata: " +
                                "destinatario:" + destinatario +
                                "mailFrom:" + mailFrom +
                                "fromName:" + destinatario);
                                updateMailSent(entity.Id,  
                                    "Error "+ "destinatario:" + destinatario +
                                    "mailFrom:" + mailFrom +
                                    "fromName:" + destinatario);
                            }
                            }catch (Exception ex) {
                                updateMailSent(entity.Id, 
                                    "Error " + "destinatario:" + destinatario +
                                    "mailFrom:" + mailFrom +
                                    "fromName:" + destinatario);
                            }
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
    public bool SendEmail(List<Mail> mails)
    {
        foreach (MailComplete mail in mails)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(mail.name, mail.mail));
                message.To.Add(new MailboxAddress(mail.Destinatario.Split("@")[0], mail.Destinatario));
                message.Subject = mail.Oggetto;
                message.Body = new TextPart("plain")
                {
                    Text = mail.Testo
                };
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    client.Authenticate(mail.mail, mail.password);
                    client.Send(message);
                    client.Disconnect(true);
                }
                Console.WriteLine("Email sent successfully to: " + mail.Destinatario + "!");
                    bool v=updateMailSent(mail.Id,"Was sent to:"+mail.Destinatario);
                if(v==false) { return false; }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Email NOT sent to: " + mail.Destinatario + " ERROR: " + ex.Message);
                    updateMailSent(mail.Id,
                        "Error " + "destinatario:" + mail.Destinatario +
                        "mailFrom:" + mail.mail +
                        "fromName:" + mail.name);
                        return false;
            }
        }
        if (mails.Count <= 0)
            Console.WriteLine("No emails to send!");
        return true;
    }
    public  bool updateMailSent(int idMail,string result)
    {
        try
        {
            TimeZoneInfo fusoOrarioItalia = TimeZoneInfo.FindSystemTimeZoneById("Europe/Rome");
            DateTime oraadesso = TimeZoneInfo.ConvertTime(DateTime.UtcNow, fusoOrarioItalia);
            using (MyDbContext context = new MyDbContext())
            {
                var entityToUpdate =  context.Mail
                .Find(idMail);
                    
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

