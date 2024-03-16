using System;
using System.Collections.Generic;

namespace AlmoxarifadoAPI.Models
{
    public partial class GestaoProduto
    {
        public int IdProduto { get; set; }
        public string? Descricao { get; set; }
        public decimal? Preco { get; set; }
        public int? EstoqueAtual { get; set; }
        public int? EstoqueMinimo { get; set; }
        public string Estado{ get; set; }
    }
}
