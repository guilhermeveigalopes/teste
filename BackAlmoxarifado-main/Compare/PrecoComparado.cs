using System;
using System.Linq;
using AlmoxarifadoAPI.Models;

namespace AlmoxarifadoAPI.Compare
{
    public class PrecoComparado
    {
        public static void AtualizarPrecoPorId(int idProduto, decimal novoPreco)
        {
            // Aqui você deve implementar a lógica para atualizar o preço do produto com o ID fornecido
            // Exemplo de como você pode atualizar o preço na base de dados usando Entity Framework Core:

            using (var dbContext = new AlmoxarifadoAPIContext()) // Substitua SeuDbContext pelo seu contexto de banco de dados real
            {
                var produto = dbContext.GestaoProdutos.FirstOrDefault(p => p.IdProduto == idProduto);

                if (produto != null)
                {
                    produto.Preco = novoPreco;
                    dbContext.SaveChanges();
                    Console.WriteLine($"Preço atualizado com sucesso para o produto com ID {idProduto}");
                }
                else
                {
                    Console.WriteLine($"Produto com ID {idProduto} não encontrado");
                }
            }
        }
    }
}
