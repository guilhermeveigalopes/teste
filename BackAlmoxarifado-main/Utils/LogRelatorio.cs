using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CrawlerDados.Utils
{
    public class LogRelatorio
    {
        public static async Task EnviarRelatorioBenchmarkingAsync(object relatorioBenchmarking)
        {
            var json = JsonConvert.SerializeObject(relatorioBenchmarking);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync("http://gestaomargi-001-site8.gtempurl.com/api/Logs", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("POST bem-sucedido!");
                }
                else
                {
                    Console.WriteLine($"Erro: {response.StatusCode}");
                }
            }
        }

        public static async Task RelatorioAsync(string NomeProduto, decimal precoMagalu, decimal precoMercado, decimal economiaTotal)
        {
            var relatorioBenchmarking = new
            {
                codigorobo = 1824,
                nomedev = "AOC4",
                nomeproduto = NomeProduto,
                valor1 = precoMagalu, 
                valor2 = precoMercado, 
                economia = economiaTotal
            };

            await EnviarRelatorioBenchmarkingAsync(relatorioBenchmarking);
        }
    }
}
