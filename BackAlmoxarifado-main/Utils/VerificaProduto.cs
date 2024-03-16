using AlmoxarifadoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CrawlerDados.Utils
{
    public class VerificaProduto
    {
        // Lista para armazenar produtos já verificados
        static List<GestaoProduto> produtosVerificados = new List<GestaoProduto>();

        public static void VerificarNovoProduto(GestaoProduto produto, string verificacao)
        {

            try
            {
                if (verificacao == "false")
                {  
                    if (produto != null)
                    {
                        if (!produtosVerificados.Exists(p => p.IdProduto == produto.IdProduto))
                        {
                            // Se é um novo produto, faça algo com ele
                            Console.WriteLine($"Novo produto encontrado: ID {produto.IdProduto}, Nome: {produto.Descricao}\n");
                            // Adicionar o produto à lista de produtos verificados
                            produtosVerificados.Add(produto);

                            LogManager.RegistrarLog("AO24", "AislanOliveira", DateTime.Now, "Consultar Dados - Verificação", "Sucesso", produto.IdProduto);

                            MercadoLivreScraper mercadoLivreScraper = new MercadoLivreScraper();
                            MagazineLuizaScraper magazineLuizaScraper = new MagazineLuizaScraper();

                            var precoMagazineLuiza = magazineLuizaScraper.ObterPreco(produto.Descricao, produto.IdProduto);
                            var precoMercadoLivre = mercadoLivreScraper.ObterPreco(produto.Descricao, produto.IdProduto);

                            Benchmarking.CompararValores(precoMagazineLuiza, precoMercadoLivre, produto.IdProduto, produto.Descricao);

                        }
                    }
                } else if (verificacao == "true")
                {
                    MercadoLivreScraper mercadoLivreScraper = new MercadoLivreScraper();
                    MagazineLuizaScraper magazineLuizaScraper = new MagazineLuizaScraper();

                    var precoMagazineLuiza = magazineLuizaScraper.ObterPreco(produto.Descricao, produto.IdProduto);
                    var precoMercadoLivre = mercadoLivreScraper.ObterPreco(produto.Descricao, produto.IdProduto);

                    BenchEmail.CompararValores(precoMagazineLuiza, precoMercadoLivre, produto.IdProduto, produto.Descricao);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao fazer a verificação do produto: {ex.Message}");
            }
        }
    }
}
