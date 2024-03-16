using AlmoxarifadoAPI.Models;
using Newtonsoft.Json;

namespace CrawlerDados.Utils
{
    class ProdutoManager
    {
        // Método para processar os dados da resposta e obter produtos
        public static List<GestaoProduto> ObterNovosProdutos(string responseData)
        {
            // Desserializar os dados da resposta para uma lista de produtos
            List<GestaoProduto> produtos = JsonConvert.DeserializeObject<List<GestaoProduto>>(responseData);
            return produtos;
        }

        // Método para verificar se o produto já foi registrado no banco de dados
        public static bool ProdutoJaRegistrado(int idProduto)
        {
            using (var context = new AlmoxarifadoAPIContext())
            {
                return context.LOGROBO.Any(log => log.IdProdutoAPI == idProduto && log.CodigoRobo == "AO24");
            }
        }
    }
}
