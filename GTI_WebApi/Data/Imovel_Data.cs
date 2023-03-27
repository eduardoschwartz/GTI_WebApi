using GTI_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GTI_WebApi.Data {
    public class Imovel_Data {
        private readonly string _connection;

        public Imovel_Data(string sConnection) {
            _connection = sConnection;
        }

        public Cadimob Retorna_Imovel(int codigo) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                return (from p in db.Cadimob where p.Codreduzido == codigo select p).FirstOrDefault();
            }
        }


    }
}