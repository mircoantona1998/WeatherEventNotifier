using Serilog;

namespace Userdata.Utils
{

    public  class Logger
    {
        private readonly Microsoft.Extensions.Logging.ILogger logger;

        public Logger(string logFilePath = "app.log")
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(logFilePath)
                .CreateLogger();

            var factory = LoggerFactory.Create(builder =>
            {
                builder.AddSerilog();
            });

            this.logger = factory.CreateLogger<Logger>();
        }

        public void LogAction(string actionDescription)
        {
            this.logger.LogInformation(actionDescription);
        }
    }
}
 
