using AlmoxarifadoAPI.Models;
using CrawlerDados.Models;
using HtmlAgilityPack;

public class MercadoLivreScraper
{
    public ProdutoScraper ObterPreco(string descricaoProduto, int idProduto)
    {
        // URL da pesquisa no Mercado Livre com base na descrição do produto
        string url = $"https://lista.mercadolivre.com.br/{descricaoProduto}";
        try
        {
            // Inicializa o HtmlWeb do HtmlAgilityPack
            HtmlWeb web = new HtmlWeb();

            // Carrega a página de pesquisa do Mercado Livre
            HtmlDocument document = web.Load(url);

            // Encontra o elemento que contém o preço do primeiro produto            
            HtmlNode firstProductPriceNode = document.DocumentNode.SelectSingleNode("//span[@class='andes-money-amount__fraction']");
            HtmlNode firstProductTitleNode = document.DocumentNode.SelectSingleNode("//h2[@class='ui-search-item__title']");
            HtmlNode firstProductUrlNode = document.DocumentNode.SelectSingleNode("//a[contains(@class, 'ui-search-link__title-card')]");

            // Verifica se o elemento foi encontrado
            if (firstProductPriceNode != null)
            {
                // Obtém o preço do primeiro produto
                string firstProductPrice = firstProductPriceNode.InnerText.Trim();
                string firstProductTitle = firstProductTitleNode.InnerText.Trim();
                string firstProductUrl = firstProductUrlNode.GetAttributeValue("href", "");

                ProdutoScraper produto = new ProdutoScraper();

                produto.Preco = firstProductPrice;
                produto.Titulo = firstProductTitle;
                produto.Url = firstProductUrl;

                // Registra o log com o ID do produto
                RegistrarLog("AO24", "AislanOliveira", DateTime.Now, "Consultar Dados - Mercado Livre", "Sucesso", idProduto);


                // Retorna o preço
                return produto;
            }

            else
            {
                Console.WriteLine("Preço não encontrado.");

                // Registra o log com o ID do produto
                RegistrarLog("AO24", "AislanOliveira", DateTime.Now, "Consultar Dados - Mercado Livre", "Preço não encontrado", idProduto);

                return null;
            }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao acessar a página: {ex.Message}");

            // Registra o log com o ID do produto
            RegistrarLog("AO24", "AislanOliveira", DateTime.Now, "Consultar Dados - Mercado Livre", $"Erro: {ex.Message}", idProduto);

            return null;
        }

    }

    private void RegistrarLog(string codRob, string usuRob, DateTime dateLog, string processo, string infLog, int idProd)
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