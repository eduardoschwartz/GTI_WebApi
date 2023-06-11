using GTI_WebApi.Models;
using GTI_WebApi.Repository;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace GTI_WebApi.Controllers {
    [RoutePrefix("processo")]
    public class ProcessoController : ApiController {
        /// <summary>
        /// Retorna detalhes de um processo pelo ano e número.
        /// </summary>
        [Route("RetornaProcessoDados/{ano}/{numero}")]
        [HttpGet]
        public HttpResponseMessage RetornaProcessoDados(int ano, int numero) {
            string _connection = "GTIconnection";
            ProcessoRepository processoRepository = new ProcessoRepository(_connection);

            
            ProcessoStruct _processo = processoRepository.Dados_Processo(ano, numero);
            if (_processo == null) {
                var response2 = Request.CreateResponse(HttpStatusCode.NotFound);
                response2.Content = new StringContent("Processo não cadastrado!");
                return response2;
            }

            string dados = JsonConvert.SerializeObject(new {
                _processo.Anexo,
                _processo.Ano,
                _processo.Assunto,
                _processo.AtendenteId,
                _processo.AtendenteNome,
                _processo.CentroCusto,
                _processo.CentroCustoNome,
                _processo.CodigoAssunto,
                _processo.CodigoCidadao,
                _processo.Complemento,
                _processo.DataArquivado,
                _processo.DataCancelado,
                _processo.DataEntrada,
                _processo.DataReativacao,
                _processo.DataSuspensao,
                _processo.Dv,
                _processo.Fisico,
                _processo.Hora,
                _processo.Inscricao,
                _processo.Interno,
                Lista_Anexos = _processo.ListaAnexo,
                Lista_Anexos_Log= _processo.ListaAnexoLog,
                Lista_Documentos = _processo.ListaProcessoDoc,
                Lista_Enderecos = _processo.ListaProcessoEndereco,
                _processo.LogradouroCodigo,
                _processo.LogradouroNome,
                _processo.LogradouroNumero,
                _processo.Message,
                _processo.NomeCidadao,
                _processo.Numero,
                _processo.ObsAnexo,
                _processo.ObsArquiva,
                _processo.ObsCancela,
                _processo.Observacao,
                _processo.ObsReativa,
                _processo.ObsSuspensao,
                _processo.Origem,
                Numero_Completo = _processo.SNumero,
                Tipo_Endereco=_processo.TipoEnd,
                _processo.Valido
            });

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(dados, Encoding.UTF8, "application/json");
            return response;

        }




    }
}