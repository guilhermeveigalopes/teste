using System;
using System.Collections.Generic;

namespace AlmoxarifadoAPI.Models
{
    public class Logrobo
    {
        public int IDlOg { get; set; }
        public string CodigoRobo { get; set; } = null!;
        public string UsuarioRobo { get; set; } = null!;
        public DateTime DateLog { get; set; }
        public string Etapa { get; set; } = null!;
        public string InformacaoLog { get; set; } = null!;
        public int IdProdutoAPI { get; set; }
    }
}
