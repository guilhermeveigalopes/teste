using AlmoxarifadoAPI.Models;

namespace CrawlerDados.Utils
{
    public class LogManager
    {
        public static void RegistrarLog(string codRob, string usuRob, DateTime dateLog, string processo, string infLog, int idProd)
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
}
