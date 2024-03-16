using AlmoxarifadoAPI.Models;
using CrawlerDados.Models;
using CrawlerDados.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class Benchmarking
{
    public static decimal? PrecoEscolhido { get; private set; }

    public static decimal? CompararValores(ProdutoScraper precoMagazineLuiza, ProdutoScraper precoMercadoLivre, int idProduto, string NomeProduto)
    {
        char[] charRemove = { 'R', '$', ' ' };
        var precoMagaluvar = precoMagazineLuiza.Preco.Trim(charRemove);
        var precoMercadovar = precoMercadoLivre.Preco.Trim(charRemove);

        decimal precoMagalu = ConvertToBRL.StringToDecimal(precoMagaluvar);
        decimal precoMercado;

        decimal.TryParse(precoMercadovar, out precoMercado);

        Console.WriteLine($"Valor Magazine Luiza: {precoMagalu}");
        Console.WriteLine($"Valor Mercado Livre: {precoMercado}\n");

        

        if (precoMagalu < precoMercado)
        {
            PrecoEscolhido = precoMagalu;
            LogManager.RegistrarLog("AO24", "AislanOliveira", DateTime.Now, "Menor Valor - Magazine Luiza", "Sucesso", idProduto);
            decimal economia = precoMercado - precoMagalu;
            LogRelatorio.RelatorioAsync(NomeProduto, precoMagalu, precoMercado, economia);
        }
        else if (precoMercado < precoMagalu)
        {
            PrecoEscolhido = precoMercado;
            LogManager.RegistrarLog("AO24", "AislanOliveira", DateTime.Now, "Menor Valor - Mercado Livre", "Sucesso", idProduto);
            decimal economia =  precoMagalu - precoMercado;
            LogRelatorio.RelatorioAsync(NomeProduto, precoMagalu, precoMercado, economia);
        }
        else
        {
            return null;
        }
        return PrecoEscolhido;
    }

}
