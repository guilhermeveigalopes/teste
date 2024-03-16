using AlmoxarifadoAPI.Models;
using CrawlerDados.Models;
using CrawlerDados.Utils;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class BenchEmail
{

    public static decimal? CompararValores(ProdutoScraper precoMagazineLuiza, ProdutoScraper precoMercadoLivre, int idProduto, string NomeProduto)
    {
        char[] charRemove = { 'R', '$', ' ' };
        // Converte as strings para decimal
        var precoMagaluvar = precoMagazineLuiza.Preco.Trim(charRemove);
        var precoMercadovar = precoMercadoLivre.Preco.Trim(charRemove);

        decimal precoMagalu = ConvertToBRL.StringToDecimal(precoMagaluvar);
        decimal precoMercado;

        decimal.TryParse(precoMercadovar, out precoMercado);

        Console.WriteLine($"Valor Magazine Luiza: {precoMagalu}");
        Console.WriteLine($"Valor Mercado Livre: {precoMercado}\n");

        if (precoMagalu < precoMercado)
        {
            SendEmail.EnviarEmail(precoMagazineLuiza.Titulo, precoMercadoLivre.Titulo, precoMercado, precoMagalu, "Magazine Luiza", precoMagazineLuiza.Url, idProduto, NomeProduto);

        }
        else if (precoMercado < precoMagalu)
        {
            SendEmail.EnviarEmail(precoMagazineLuiza.Titulo, precoMercadoLivre.Titulo, precoMercado, precoMagalu, "Mercado Livre", precoMercadoLivre.Url, idProduto, NomeProduto);

        }
        else
        {
            return null;
        }
        return 0;
    }

}
