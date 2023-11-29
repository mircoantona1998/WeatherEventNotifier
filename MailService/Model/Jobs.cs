using Microsoft.EntityFrameworkCore;

public class Jobs 
{
    public int Id { get; set; }
    public string? Job { get; set; }
    public bool? IsActive { get; set; }
    public int? HourToStart { get; set; }
    public int? MinuteToStart { get; set; }
    public DateTime? LastTimestampStart { get; set; }
    public DateTime? LastTimestampEnd { get; set; }
    public bool? Errors { get; set; }
    public async Task<bool> ToWork()
    {
        Jobs result = new Jobs();
        Console.WriteLine("Verify status job..");
        try
        {
            using (MyDbContext context = new MyDbContext())
            {
                result = await context.Jobs
                    .Where(ec => ec.Job == "Mail"
                    ).FirstAsync();
                if ((bool)result.IsActive)
                {
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
        return false;
    }
    public async Task start_job()
    {
        Jobs result = new Jobs();
        Console.WriteLine("Start job..");
        try
        {
            TimeZoneInfo fusoOrarioItalia = TimeZoneInfo.FindSystemTimeZoneById("Europe/Rome");
            DateTime oraadesso = TimeZoneInfo.ConvertTime(DateTime.UtcNow, fusoOrarioItalia);
            using (MyDbContext context = new MyDbContext())
            {
                result = await context.Jobs
                    .Where(ec => ec.Job == "Mail"
                    ).FirstAsync();
                if (result != null)
                {
                    result.LastTimestampStart = oraadesso;
                    await context.SaveChangesAsync();
                }
                else
                {
                    Console.WriteLine("Job not found in the database.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
        return;
    }
    public async Task refresh_status(bool errors)
    {
        Jobs result = new Jobs();
        Console.WriteLine("Refresh status job..");
        try
        {
            TimeZoneInfo fusoOrarioItalia = TimeZoneInfo.FindSystemTimeZoneById("Europe/Rome");
            DateTime oraadesso = TimeZoneInfo.ConvertTime(DateTime.UtcNow, fusoOrarioItalia);
            using (MyDbContext context = new MyDbContext())
            {
                result = await context.Jobs
                    .Where(ec => ec.Job == "Mail"
                    ).FirstAsync();
                if (result != null)
                {
                    result.LastTimestampEnd = oraadesso;
                    result.Errors = errors;
                    await context.SaveChangesAsync();
                }
                else
                {
                    Console.WriteLine("Job not found in the database.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
        return;
    }
}

