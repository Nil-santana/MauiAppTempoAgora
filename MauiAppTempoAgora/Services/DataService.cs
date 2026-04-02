using MauiAppTempoAgora.Models;
using Newtonsoft.Json.Linq;

namespace MauiAppTempoAgora.Services
{
    public class DataService
    {

        public static async Task<Tempo?> Gatprevisao(string cidade)
        {

            Tempo? t = null;


            String chave = "91cd4293f0f85930fafe13a45a37a142";

            String url = $"https://api.openweathermap.org/data/2.5/weather?" +
                $"q={cidade}&units=metric&appid={chave}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resp = await client.GetAsync(url);
                // Verifica o StatusCode para tratar erros específicos
                if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null; // Cidade não encontrada → retorna null
                }

                if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Chave de API inválida ou não autorizada.");
                }
                if (resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();
                    
                    var rascunho = JObject.Parse(json);

                    DateTime time = new();
                    DateTime sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                    DateTime sunset = time.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime();

                    t = new()
                    {
                        lat = (double)rascunho["coord"]["lat"],
                        lon = (double)rascunho["coord"]["lon"],
                        description = (string)rascunho["weather"][0]["description"],
                        main = (string)rascunho["weather"][0]["main"],
                        temp_min = (double)rascunho["main"]["temp_min"],
                        temp_max = (double)rascunho["main"]["temp_max"],
                        speed = (double)rascunho["wind"]["speed"],
                        visibility = (int)rascunho["visibility"],
                        sunrise = sunrise.ToString("HH:mm:ss"),
                        sunset = sunset.ToString("HH:mm:ss"),
                    };// Fecha objeto do tempo.
                }// Fecha if de resposta do servidor.
            } // Fecha using do HttpClient.
            return t;
        }
    }
}
