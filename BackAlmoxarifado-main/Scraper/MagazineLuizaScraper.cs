using AlmoxarifadoAPI.Models;
using CrawlerDados.Models;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Security.Policy;

public class MagazineLuizaScraper
{
    public ProdutoScraper ObterPreco(string descricaoProduto, int idProduto)
    {
        try
        {
            
            // Inicializa o ChromeDriver
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync($"https://www.magazineluiza.com.br/busca/{descricaoProduto}").Result;

                string content = response.Content.ReadAsStringAsync().Result;

                var docHtml = new HtmlDocument();

                docHtml.LoadHtml(content);

                var produtos = docHtml.DocumentNode.SelectNodes("//a");
                ProdutoScraper produtoRetorno = new ProdutoScraper();
                foreach (var item in produtos)
                {
                    if (item.OuterHtml.Contains("data-testid=\"product-card-container\""))
                    {

                        var card = item;
                        var linkproduto = card.Attributes["href"].Value;
                        var elePrecoValue = card.SelectSingleNode(".//p[@data-testid=\"price-value\"]");
                        var firstProductTitle = card.SelectSingleNode(".//h2[@data-testid=\"product-title\"]");
                        var tdd = "sda";

                       

                        produtoRetorno.Preco = elePrecoValue.InnerText;
                        produtoRetorno.Titulo = firstProductTitle.InnerText;
                        produtoRetorno.Url = linkproduto;



                    }
                    
                }

                    // Registra o log com o ID do produto
                    RegistrarLog("AO24", "AislanOliveira", DateTime.Now, "Consultar Dados - Magazine Luiza", "Sucesso", idProduto);
                return produtoRetorno;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao acessar a página: {ex.Message}");

            // Registra o log com o ID do produto
            RegistrarLog("AO24", "AislanOliveira", DateTime.Now, "Consultar Dados - Magazine Luiza", $"Erro: {ex.Message}", idProduto);

            return null;
        }
    }

    private static void RegistrarLog(string codRob, string usuRob, DateTime dateLog, string processo, string infLog, int idProd)
    {

        using (var context = new AlmoxarifadoAPIContext())
        {
            var log = new Logrobo
            {
                CodigoRobo = codRob,
                UsuarioRobo = usuRob,
                DateLog = dateLog,
                Etapa = processo,
                InformacaoLog = infLog,
                IdProdutoAPI = idProd
            };
            context.LOGROBO.Add(log);
            context.SaveChanges();
        }

    }
}

