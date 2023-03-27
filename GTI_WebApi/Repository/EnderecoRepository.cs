using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace GTI_WebApi.Repository {
    public class EnderecoRepository {
        private readonly String _connection;

        public EnderecoRepository(string connection){
            _connection = connection;
        }

        public int RetornaCep(Int32 CodigoLogradouro, Int16 Numero) {
            int nCep = 0;
            int Num1, Num2;
            bool bPar, bImpar;

            if (Numero % 2 == 0) {
                bPar = true; bImpar = false;
            } else {
                bPar = false; bImpar = true;
            }

            using (GTI_Context db = new GTI_Context(_connection)) {
                var Sql = (from c in db.Cep where c.Codlogr == CodigoLogradouro select c).ToList();
                if (Sql.Count == 0)
                    nCep = 0;
                else if (Sql.Count == 1)
                    nCep = Sql[0].cep;
                else {
                    foreach (var item in Sql) {
                        Num1 = Convert.ToInt32(item.Valor1);
                        Num2 = Convert.ToInt32(item.Valor2);
                        if (Numero >= Num1 && Numero <= Num2) {
                            if ((bImpar && item.Impar) || (bPar && item.Par)) {
                                nCep = item.cep;
                                break;
                            }
                        } else if (Numero >= Num1 && Num2 == 0) {
                            if ((bImpar && item.Impar) || (bPar && item.Par)) {
                                nCep = item.cep;
                                break;
                            }
                        }
                    }
                }
            }
            return nCep;
        }

    }
}