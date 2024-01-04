using Confluent.Kafka;
using ExposeAPI.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using ExposeAPI.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Confluent.Kafka.Admin;

config.configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();


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
            Console.WriteLine($"Topic '{Kafka.topic_to_userdata}' creato con successo.");
            topicCreated = true;
        }
        catch (CreateTopicsException e)
        {
            foreach (var topicError in e.Results)
            {
                if (topicError.Error.Code == ErrorCode.TopicAlreadyExists)
                {
                    Console.WriteLine($"Il topic '{Kafka.topic_to_userdata}' esiste già.");
                    topicCreated = true;
                }
                else
                {
                    Console.WriteLine($"Errore durante la creazione del topic: {topicError.Error.Reason}");
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
app.UseCors("CorsPolicy");
app.UseSwagger();
app.UseSwaggerUI();
//app.UseHttpsRedirection();
app.UseResponseCaching();
app.MapControllers();
app.Run();