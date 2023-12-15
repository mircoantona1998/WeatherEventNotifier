using Confluent.Kafka;
using ExposeAPI.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using ExposeAPI.DB;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

config.confdb = configuration.GetConnectionString("Userdata");
Kafka.producer = new KafkaProducer(configuration["topic_to_configuration"]);
Kafka.consumer = new KafkaConsumer(configuration["bootstrapServers"], configuration["groupID_response"], configuration["topic_to_userdata"]);
Kafka.consumerConfig = new ConsumerConfig
{
    BootstrapServers = configuration["bootstrapServers"],
    GroupId = configuration["groupID_response"],
    AutoOffsetReset = AutoOffsetReset.Earliest,
    EnableAutoCommit=true
};
Kafka.producerConfig = new ProducerConfig {BootstrapServers= configuration["bootstrapServers"] };
Kafka.topic_to_configuration = configuration["topic_to_configuration"];
Kafka.topic_to_userdata = configuration["topic_to_userdata"];

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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),
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
app.UseHttpsRedirection();
app.UseResponseCaching();
app.MapControllers();
app.Run();