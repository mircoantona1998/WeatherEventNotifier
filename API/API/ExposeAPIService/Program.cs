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
using Userdata.Models;


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
    Console.WriteLine("Attendo la disponibilit� di SQL Server... connection string master="+ masterConnectionString);
    log.LogAction("Attendo la disponibilit� di SQL Server... connection string master=" + masterConnectionString);
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
string clusters = Environment.GetEnvironmentVariable("HowManyCluster") ?? "2";
string partitions = Environment.GetEnvironmentVariable("HowManyPartition") ?? "2";
UserRepository userRepo;
userRepo = new UserRepository(connectionString);
await userRepo.DistributeClustersPartitions(Convert.ToInt32(clusters),Convert.ToInt32(partitions));

var app = builder.Build();
app.UseDeveloperExceptionPage();
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

