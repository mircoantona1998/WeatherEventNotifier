using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserdataService.Model;

public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IProducer<Null, string> _kafkaProducer;
    private readonly IConfiguration _configuration;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IProducer<Null, string> kafkaProducer,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _kafkaProducer = kafkaProducer;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        // Implementa la registrazione dell'utente

        // Invia un messaggio a Kafka quando la registrazione ha successo
        await _kafkaProducer.ProduceAsync("registration-topic", new Message<Null, string> { Value = "User registered: " + model.Username });

        return Ok(new { Message = "Registration successful" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        // Implementa il login dell'utente
        var user = await _userManager.FindByNameAsync(model.Username);

        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            // Credenziali valide
            var token = GenerateJwtToken(user);

            // Invia un messaggio a Kafka quando il login ha successo
            await _kafkaProducer.ProduceAsync("login-topic", new Message<Null, string> { Value = "User logged in: " + model.Username });

            return Ok(new { Token = token });
        }

        return Unauthorized(new { Message = "Invalid username or password" });
    }

    private string GenerateJwtToken(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName)
            // Aggiungi altre rivendicazioni utente se necessario
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(15), // Specifica la scadenza del token
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
