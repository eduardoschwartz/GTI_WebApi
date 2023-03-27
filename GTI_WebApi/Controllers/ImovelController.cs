using GTI_WebApi.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GTI_WebApi.Controllers {

    public class ImovelController : ApiController    {


        /// <summary>
        /// Retorna dados de um imóvel por código.
        /// </summary>
        [Route("GetImovelId")]
        public HttpResponseMessage GetImovelId(int id) {
            string _connection = "GTIconnection";
            Imovel_Data _imovel = new Imovel_Data(_connection);
            int lote = _imovel.Retorna_Imovel(id).Lote;
            return Request.CreateResponse(HttpStatusCode.OK, "Lote: " + lote.ToString());
        }

    }
}