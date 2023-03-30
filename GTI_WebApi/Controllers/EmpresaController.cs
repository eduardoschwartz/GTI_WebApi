using GTI_WebApi.Models;
using GTI_WebApi.Repository;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace GTI_WebApi.Controllers {
    [RoutePrefix("empresa")]
    public class EmpresaController : ApiController    {

        /// <summary>
        /// Retorna detalhes de uma empresa por inscrição municipal.
        /// </summary>
        [Route("GetEmpresaId/{id:int}")]
        public HttpResponseMessage GetEmpresaId(int id) {
            string _connection = "GTIconnection";
            EmpresaRepository _empresa = new EmpresaRepository(_connection);
            EmpresaStruct empresa = _empresa.Dados_Empresa_Full(id);
            var response = Request.CreateResponse(HttpStatusCode.OK);

            string dados = JsonConvert.SerializeObject(new {
                empresa.Alvara,
                empresa.Area,
                empresa.Atividade_codigo,
                empresa.Atividade_extenso,
                empresa.Atividade_nome,
                empresa.Bairro_codigo,
                empresa.Bairro_nome,
                empresa.Cadastro_vre,
                empresa.Capital_social,
                empresa.Cargo_contato,
                empresa.Cep,
                empresa.Cidade_codigo,
                empresa.Cidade_nome,
                Documento=Functions.FormatarCpfCnpj(empresa.Cpf_cnpj),
                empresa.Codigo,
                empresa.Codigo_aliquota,
                empresa.Complemento,
                Data_Processo_Enceramento=empresa.Dataprocencerramento,
                Data_Processo_Abertura=empresa.Dataprocesso,
                Data_Abertura=empresa.Data_abertura,
                empresa.Data_Encerramento,
                empresa.Distrito,
                empresa.Email_contato,
                empresa.Endereco_codigo,
                empresa.Endereco_nome,
                empresa.Endereco_nome_abreviado,
                empresa.Fax_contato,
                empresa.Fone_contato,
                empresa.Homepage,
                empresa.Horario,
                empresa.Horario_extenso,
                empresa.Horario_Nome,
                empresa.Imovel,
                empresa.Imune_issqn,
                empresa.Inscricao_estadual,
                empresa.Isento_iss,
                empresa.Isento_taxa,
                empresa.Juridica,
                empresa.Lote,
                empresa.Mei,
                empresa.Nome_contato,
                empresa.Nome_fantasia,
                empresa.Nome_logradouro,
                empresa.Nome_orgao,
                empresa.Numero,
                empresa.Numero_registro_resp,
                Numero_Processo_Abertura=empresa.Numprocesso,
                Numero_Processo_Encerramento=empresa.Numprocessoencerramento,
                empresa.Orgao,
                empresa.Orgao_emissor_resp,
                empresa.Ponto_agencia,
                empresa.prof_responsavel_codigo,
                empresa.Prof_responsavel_conselho,
                empresa.prof_responsavel_nome,
                empresa.Prof_responsavel_registro,
                empresa.Qtde_empregado,
                empresa.Qtde_profissionais,
                empresa.Qtde_socio,
                empresa.Quadra,
                empresa.Razao_social,
                empresa.Responsavel_contabil_codigo,
                empresa.Rg,
                empresa.Rg_responsavel,
                empresa.Seq,
                empresa.Setor,
                empresa.Sil,
                empresa.Simples,
                empresa.Situacao,
                empresa.Subunidade,
                empresa.UF,
                empresa.Lista_Socio,
                empresa.Lista_Placa

            }); 

            response.Content = new StringContent(dados, Encoding.UTF8, "application/json");
            return response;
        }

    }
}