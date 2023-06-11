using GTI_WebApi.Data;
using GTI_WebApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace GTI_WebApi.Controllers {

    [RoutePrefix("imovel")]
    public class ImovelController : ApiController    {
        
        /// <summary>
        /// Retorna detalhes de um imóvel por código.
        /// </summary>
        [Route("RetornaImovelId/{id:int}")]
        [HttpGet]
        public HttpResponseMessage RetornaImovelId(int id) {
            string _connection = "GTIconnection";
            ImovelRepository _imovelRepository = new ImovelRepository(_connection);
            Imovel_Full imovel = _imovelRepository.Dados_Imovel_Full(id);

            if (imovel == null || imovel.Codigo == 0) {
                var response2 = Request.CreateResponse(HttpStatusCode.NotFound);
                response2.Content = new StringContent("Imóvel não cadastrado!");
                return response2;
            }

            List<AreaStruct> lista_area = imovel.Lista_Area;
            List<ProprietarioStruct> lista_proprietario = imovel.Lista_Proprietario;
            List<Testada> lista_testada = imovel.Lista_Testada;
            string _cip=(bool)imovel.Cip == true ? "S" : "N";
            var response = Request.CreateResponse(HttpStatusCode.OK);

            string dados = JsonConvert.SerializeObject(new { 
                imovel.Area_Terreno,
                imovel.Benfeitoria,
                imovel.Benfeitoria_Nome,
                imovel.Categoria,
                imovel.Categoria_Nome,
                Cip=_cip,
                imovel.Codigo,
                imovel.Condominio_Codigo,
                imovel.Condominio_Nome,
                imovel.Conjugado,
                imovel.EE_TipoEndereco,
                imovel.Endereco_Entrega,
                imovel.Endereco_Imovel,
                imovel.FracaoIdeal,
                imovel.Imunidade,
                imovel.Inativo,
                imovel.Inscricao,
                imovel.LoteOriginal,
                imovel.NumMatricula,
                imovel.Pedologia,
                imovel.Pedologia_Nome,
                imovel.QuadraOriginal,
                imovel.Situacao,
                imovel.Situacao_Nome,
                imovel.TipoMat,
                imovel.Topografia,
                imovel.Topografia_Nome,
                imovel.Uso_terreno,
                imovel.Uso_terreno_Nome,
                Lista_Area = lista_area,
                Lista_Proprietario = lista_proprietario,
                Lista_Testada = lista_testada

            });

            response.Content = new StringContent(dados, Encoding.UTF8, "application/json");
            return response;
        }

    }
}