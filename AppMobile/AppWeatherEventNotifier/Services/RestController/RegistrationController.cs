
namespace AppWeatherEventNotifier.Services.RestController
{
     class RegistrationController
    {    
        public static async Task<bool> registration_request(string nome, string cognome, string usernameEntry, string passEntry, string cap,string city,string email, string telefono, string indirizzo)
        {
            string endPoint = "/Auth/Registration";
            var body = new
            {
                Nome=nome,
                Cognome=cognome,
                Username = usernameEntry,
                Password = passEntry,
                CAP=cap,
                City=city,
                Email=email,
                Phone=telefono,
                Address=indirizzo
            };
            bool res = await Helper.HttpHelper.HttpPostRequest<bool>(endPoint, body, false);
            if (res== true) { return true; }
            else return false;
        }
    }
}
