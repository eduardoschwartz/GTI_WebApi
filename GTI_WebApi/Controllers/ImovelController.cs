using GTI_WebApi.Data;
using GTI_WebApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Web.Http;

namespace GTI_WebApi.Controllers {

    [RoutePrefix("imovel")]
    public class ImovelController : ApiController    {
        
        /// <summary>
        /// Retorna detalhes de um imóvel por código.
        /// </summary>
        [Route("GetImovelId/{id:int}")]
        public HttpResponseMessage GetImovelId(int id) {
            string _connection = "GTIconnection";
            ImovelRepository _imovel = new ImovelRepository(_connection);
            Imovel_Full imovel = _imovel.Dados_Imovel_Full(id);
            List<AreaStruct> lista_area = imovel.Lista_Area;
            List<ProprietarioStruct> lista_proprietario = imovel.Lista_Proprietario;
            List<Testada> lista_testada = imovel.Lista_Testada;

            var response = Request.CreateResponse(HttpStatusCode.OK);

            string dados = JsonConvert.SerializeObject(new { 
                imovel.Codigo, 
                imovel.Inscricao,
                imovel.Condominio_Nome,
                Areas_Imovel=lista_area,
                Proprietario=lista_proprietario,
                Testada=lista_testada

            });


            response.Content = new StringContent(dados, Encoding.UTF8, "application/json");
            return response;
        }

    }
}