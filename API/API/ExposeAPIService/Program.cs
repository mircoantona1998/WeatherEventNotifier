using Confluent.Kafka;
using ExposeAPI.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using ExposeAPI.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Confluent.Kafka.Admin;
using ExposeAPI.Utils;
using Prometheus;

config.configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();
Logger log= new();

string sqlServerHost = Environment.GetEnvironmentVariable("SQL_SERVER_HOST") ?? "localhost";
string sqlServerPort = Environment.GetEnvironmentVariable("SQL_SERVER_PORT") ?? "1433";
string sqlServerDatabase = Environment.GetEnvironmentVariable("SQL_SERVER_DATABASE") ?? "Userdata";
string sqlServerUser = Environment.GetEnvironmentVariable("SQL_SERVER_USER") ?? "sa";
string sqlServerPassword = Environment.GetEnvironmentVariable("SQL_SERVER_PASSWORD") ?? "RootRoot.1";
string masterConnectionString = Environment.GetEnvironmentVariable("ConnectionStringsMaster") ?? $"Data Source={sqlServerHost},{sqlServerPort};User ID={sqlServerUser};Password={sqlServerPassword};";
string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings") ?? config.configuration["ConnectionStrings:Userdata"];
string sqlServerScriptPath = "init-sqlServer.sql";
string sqlServerDump = File.ReadAllText(sqlServerScriptPath);
connectionString = connectionString+"TrustServerCertificate = true";
masterConnectionString = masterConnectionString + "TrustServerCertificate = true";
while (!DB.IsSqlServerAvailable(masterConnectionString))
{
    Console.WriteLine("Attendo la disponibilità di SQL Server... connection string master="+ masterConnectionString);
    log.LogAction("Attendo la disponibilità di SQL Server... connection string master=" + masterConnectionString);
    Thread.Sleep(5000);
}
foreach (string command in DB.GetSqlCommands(sqlServerDump))
{
    if(command.Contains("CREATE DATABASE")) {
        bool firtsCompose= DB.ExecuteSqlCommand(masterConnectionString, command);
        if (firtsCompose == false) break;
    }
    else DB.ExecuteSqlCommand(connectionString, command);
}
config.confdb = Environment.GetEnvironmentVariable("ConnectionStrings") ?? config.configuration["ConnectionStrings:Userdata"];
Kafka.producer = new KafkaProducer();
Kafka.consumer = new KafkaConsumer(Environment.GetEnvironmentVariable("topic_to_userdata") ?? config.configuration["topic_to_userdata"]);
Kafka.consumerConfig = new ConsumerConfig
{
    BootstrapServers = Environment.GetEnvironmentVariable("bootstrapServers") ?? config.configuration["bootstrapServers"] ,
    GroupId = Environment.GetEnvironmentVariable("groupID") ?? config.configuration["groupID"]  ,
    AutoOffsetReset = AutoOffsetReset.Earliest,
    EnableAutoCommit=true
};
Kafka.producerConfig = new ProducerConfig {
    BootstrapServers = Environment.GetEnvironmentVariable("bootstrapServers") ?? config.configuration["bootstrapServers"]   ,
    ClientId= Environment.GetEnvironmentVariable("groupID") ?? config.configuration["groupID"]
};
Kafka.topic_to_configuration = Environment.GetEnvironmentVariable("topic_to_configuration") ?? config.configuration["topic_to_configuration"];
Kafka.topic_to_userdata = Environment.GetEnvironmentVariable("topic_to_userdata") ?? config.configuration["topic_to_userdata"] ;
var adminClientConfig = new AdminClientConfig
{
    BootstrapServers = Environment.GetEnvironmentVariable("bootstrapServers") ?? config.configuration["bootstrapServers"]
};
using (var adminClient = new AdminClientBuilder(adminClientConfig).Build())
{
    bool topicCreated = false;

    while (!topicCreated)
    {
        try
        {
            var topicSpecification = new TopicSpecification
            {
                Name = Kafka.topic_to_userdata,
                NumPartitions = 1,
                ReplicationFactor = 1
            };

            adminClient.CreateTopicsAsync(new List<TopicSpecification> { topicSpecification }).Wait();
            Console.WriteLine($"Topic '{Kafka.topic_to_userdata}' created successfully.");
            log.LogAction($"Topic '{Kafka.topic_to_userdata}' created successfully.");
            topicCreated = true;
        }
        catch (AggregateException ae)
        {
            foreach (var innerException in ae.InnerExceptions)
            {
                if (innerException is CreateTopicsException createTopicsException)
                {
                    foreach (var topicError in createTopicsException.Results)
                    {
                        if (topicError.Error.Code == ErrorCode.TopicAlreadyExists)
                        {
                            Console.WriteLine($"The topic '{Kafka.topic_to_userdata}' already exists.");
                            log.LogAction($"The topic '{Kafka.topic_to_userdata}' already exists.");
                            topicCreated = true;
                        }
                        else
                        {
                            Console.WriteLine($"Error creating topic: {topicError.Error.Reason}");
                            log.LogAction($"Error creating topic: {topicError.Error.Reason}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Unexpected exception: {innerException.Message}");
                    log.LogAction($"Unexpected exception: {innerException.Message}");
                }
            }
        }
    }
}
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("Jwt") ?? config.configuration["Jwt:SecretKey"] )),
            ClockSkew = TimeSpan.Zero
        };
    }
    );
builder.Services.AddMvc().AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver());
builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ExposeAPI WeatherEventNotifier", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header utilizza lo schema Bearer.
                      Digita 'Bearer' [spazio] ed incolla il token che puoi ottenere dalla login.
                      Esempio: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    );
});
var app = builder.Build();
//if (env.IsDevelopment())
//{
    app.UseDeveloperExceptionPage();
//}
//else
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseHttpMetrics();
app.UseSwagger();
app.UseSwaggerUI();
app.UseResponseCaching();
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseDeveloperExceptionPage();
app.MapControllers();
app.MapMetrics();
app.Run();