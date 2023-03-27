using GTI_WebApi.Models;
using GTI_WebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using static GTI_WebApi.Models.modelCore;

namespace GTI_WebApi.Data {
    public class ImovelRepository {
        private readonly string _connection;

        public ImovelRepository(string sConnection) {
            _connection = sConnection;
        }

        public Cadimob Retorna_Imovel(int codigo) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                return (from p in db.Cadimob where p.Codreduzido == codigo select p).FirstOrDefault();
            }
        }

        public Imovel_Full Dados_Imovel_Full(int nCodigo) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                var reg = (from i in db.Cadimob
                           join c in db.Condominio on i.Codcondominio equals c.Cd_codigo into ic from c in ic.DefaultIfEmpty()
                           join b in db.Benfeitoria on i.Dt_codbenf equals b.Codbenfeitoria into ib from b in ib.DefaultIfEmpty()
                           join p in db.Pedologia on i.Dt_codpedol equals p.Codpedologia into ip from p in ip.DefaultIfEmpty()
                           join t in db.Topografia on i.Dt_codtopog equals t.Codtopografia into it from t in it.DefaultIfEmpty()
                           join s in db.Situacao on i.Dt_codsituacao equals s.Codsituacao into ist from s in ist.DefaultIfEmpty()
                           join cp in db.Categprop on i.Dt_codcategprop equals cp.Codcategprop into icp from cp in icp.DefaultIfEmpty()
                           join u in db.Usoterreno on i.Dt_codusoterreno equals u.Codusoterreno into iu from u in iu.DefaultIfEmpty()
                           where i.Codreduzido == nCodigo
                           select new ImovelStruct {
                               Codigo = i.Codreduzido, Distrito = i.Distrito, Setor = i.Setor, Quadra = i.Quadra, Lote = i.Lote, Seq = i.Seq,
                               Unidade = i.Unidade, SubUnidade = i.Subunidade, NomeCondominio = c.Cd_nomecond, Imunidade = i.Imune, TipoMat = i.Tipomat, NumMatricula = i.Nummat,
                               Numero = i.Li_num, Complemento = i.Li_compl, QuadraOriginal = i.Li_quadras, LoteOriginal = i.Li_lotes, ResideImovel = i.Resideimovel, Inativo = i.Inativo,
                               FracaoIdeal = i.Dt_fracaoideal, Area_Terreno = i.Dt_areaterreno, Benfeitoria = i.Dt_codbenf, Categoria = i.Dt_codcategprop, Pedologia = i.Dt_codpedol, Topografia = i.Dt_codtopog,
                               Uso_terreno = i.Dt_codusoterreno, Situacao = i.Dt_codsituacao, EE_TipoEndereco = i.Ee_tipoend, Benfeitoria_Nome = b.Descbenfeitoria, Pedologia_Nome = p.Descpedologia,
                               Topografia_Nome = t.Desctopografia, Situacao_Nome = s.Descsituacao, Categoria_Nome = cp.Desccategprop, Uso_terreno_Nome = u.Descusoterreno, CodigoCondominio = c.Cd_codigo
                           }).FirstOrDefault();

                Imovel_Full row = new Imovel_Full();
                if (reg == null)
                    return row;
                row.Codigo = nCodigo;
                row.Inscricao = reg.Distrito.ToString() + "." + reg.Setor.ToString("00") + "." + reg.Quadra.ToString("0000") + "." + reg.Lote.ToString("00000") + "." + reg.Seq.ToString("00") + "." + reg.Unidade.ToString("00") + "." + reg.SubUnidade.ToString("000");
                row.Condominio_Codigo = reg.CodigoCondominio == null ? 999 : (int)reg.CodigoCondominio;
                row.Condominio_Nome = reg.CodigoCondominio == null || reg.CodigoCondominio == 999 ? "N/A" : row.Condominio_Nome;
                row.Imunidade = reg.Imunidade == null ? false : Convert.ToBoolean(reg.Imunidade);
                row.Cip = reg.Cip == null ? false : Convert.ToBoolean(reg.Cip);
                row.ResideImovel = reg.ResideImovel == null ? false : Convert.ToBoolean(reg.ResideImovel);
                row.Inativo = reg.Inativo == null ? false : Convert.ToBoolean(reg.Inativo);
                if (reg.TipoMat == null || reg.TipoMat == "M")
                    row.TipoMat = "M";
                else
                    row.TipoMat = "T";
                row.NumMatricula = reg.NumMatricula == null ? 0 : (long)reg.NumMatricula;
                row.QuadraOriginal = reg.QuadraOriginal == null ? "N/D" : reg.QuadraOriginal.ToString();
                row.LoteOriginal = reg.LoteOriginal == null ? "N/D" : reg.LoteOriginal.ToString();
                row.FracaoIdeal = reg.FracaoIdeal;
                row.Area_Terreno = reg.Area_Terreno;
                row.Benfeitoria = (short)reg.Benfeitoria;
                row.Benfeitoria_Nome = reg.Benfeitoria_Nome;
                row.Categoria = (short)reg.Categoria;
                row.Categoria_Nome = reg.Categoria_Nome;
                row.Pedologia = (short)reg.Pedologia;
                row.Pedologia_Nome = reg.Pedologia_Nome;
                row.Situacao = (short)reg.Situacao;
                row.Situacao_Nome = reg.Situacao_Nome;
                row.Topografia = (short)reg.Topografia;
                row.Topografia_Nome = reg.Topografia_Nome;
                row.Uso_terreno = (short)reg.Uso_terreno;
                row.Uso_terreno_Nome = reg.Uso_terreno_Nome;
                row.EE_TipoEndereco = (short)reg.EE_TipoEndereco;

                row.Endereco_Imovel = Dados_Endereco(nCodigo, TipoEndereco.Local);
                if (reg.EE_TipoEndereco == 0)
                    row.Endereco_Entrega = row.Endereco_Imovel;
                else {
                    if (reg.EE_TipoEndereco == 1) {
                        row.Endereco_Entrega = Dados_Endereco(nCodigo, TipoEndereco.Proprietario);
                    } else {
                        row.Endereco_Entrega = Dados_Endereco(nCodigo, TipoEndereco.Entrega);
                    }
                }

                row.Lista_Proprietario = Lista_Proprietario(nCodigo);
                row.Lista_Testada = Lista_Testada(nCodigo);
                row.Lista_Area = Lista_Area(nCodigo);


                return row;
            }
        }

        public EnderecoStruct Dados_Endereco(int Codigo, TipoEndereco Tipo) {
            EnderecoStruct regEnd = new EnderecoStruct();
            using (GTI_Context db = new GTI_Context(_connection)) {
                if (Tipo == TipoEndereco.Local) {
                    var reg = (from i in db.Cadimob
                               join b in db.Bairro on i.Li_codbairro equals b.Codbairro into ib from b in ib.DefaultIfEmpty()
                               join fq in db.Facequadra on new { p1 = i.Distrito, p2 = i.Setor, p3 = i.Quadra, p4 = i.Seq } equals new { p1 = fq.Coddistrito, p2 = fq.Codsetor, p3 = fq.Codquadra, p4 = fq.Codface } into ifq from fq in ifq.DefaultIfEmpty()
                               join l in db.Logradouro on fq.Codlogr equals l.Codlogradouro into lfq from l in lfq.DefaultIfEmpty()
                               where i.Codreduzido == Codigo && b.Siglauf == "SP" && b.Codcidade == 413
                               select new {
                                   i.Li_num, i.Li_codbairro, b.Descbairro, fq.Codlogr, l.Endereco, i.Li_compl, l.Endereco_resumido
                               }).FirstOrDefault();
                    if (reg == null)
                        return regEnd;
                    else {
                        regEnd.CodigoBairro = reg.Li_codbairro;
                        regEnd.NomeBairro = reg.Descbairro.ToString();
                        regEnd.CodigoCidade = 413;
                        regEnd.NomeCidade = "JABOTICABAL";
                        regEnd.UF = "SP";
                        regEnd.CodLogradouro = reg.Codlogr;
                        regEnd.Endereco = reg.Endereco ?? "";
                        regEnd.Endereco_Abreviado = reg.Endereco_resumido ?? "";
                        regEnd.Numero = reg.Li_num;
                        regEnd.Complemento = reg.Li_compl ?? "";
                        regEnd.CodigoBairro = reg.Li_codbairro;
                        regEnd.NomeBairro = reg.Descbairro;
                        EnderecoRepository clsCep = new EnderecoRepository(_connection);
                        regEnd.Cep = clsCep.RetornaCep(Convert.ToInt32(reg.Codlogr), Convert.ToInt16(reg.Li_num)) == 0 ? "14870000" : clsCep.RetornaCep(Convert.ToInt32(reg.Codlogr), Convert.ToInt16(reg.Li_num)).ToString("0000");
                    }
                } else if (Tipo == TipoEndereco.Entrega) {
                    var reg = (from ee in db.Endentrega
                               join b in db.Bairro on new { p1 = ee.Ee_uf, p2 = ee.Ee_cidade, p3 = ee.Ee_bairro } equals new { p1 = b.Siglauf, p2 = (short?)b.Codcidade, p3 = (short?)b.Codbairro } into eeb from b in eeb.DefaultIfEmpty()
                               join c in db.Cidade on new { p1 = ee.Ee_uf, p2 = ee.Ee_cidade } equals new { p1 = c.Siglauf, p2 = (short?)c.Codcidade } into eec from c in eec.DefaultIfEmpty()
                               join l in db.Logradouro on ee.Ee_codlog equals l.Codlogradouro into lee from l in lee.DefaultIfEmpty()
                               where ee.Codreduzido == Codigo
                               select new {
                                   ee.Ee_numimovel, ee.Ee_bairro, b.Descbairro, c.Desccidade, ee.Ee_uf, ee.Ee_cidade, ee.Ee_codlog, ee.Ee_nomelog, l.Endereco, ee.Ee_complemento, l.Endereco_resumido, ee.Ee_cep
                               }).FirstOrDefault();
                    if (reg == null)
                        return regEnd;
                    else {
                        regEnd.CodigoBairro = reg.Ee_bairro;
                        regEnd.NomeBairro = reg.Descbairro == null ? "" : reg.Descbairro.ToString();
                        regEnd.CodigoCidade = reg.Ee_cidade == null ? 0 : reg.Ee_cidade;
                        regEnd.NomeCidade = reg.Descbairro == null ? "" : reg.Desccidade;
                        regEnd.UF = "SP";
                        regEnd.CodLogradouro = reg.Ee_codlog;
                        regEnd.Endereco = reg.Ee_nomelog ?? "";
                        regEnd.Endereco_Abreviado = reg.Endereco_resumido ?? "";
                        if (!String.IsNullOrEmpty(reg.Endereco))
                            regEnd.Endereco = reg.Endereco.ToString();
                        regEnd.Numero = reg.Ee_numimovel;
                        regEnd.Complemento = reg.Ee_complemento ?? "";
                        regEnd.CodigoBairro = reg.Ee_bairro;
                        regEnd.NomeBairro = reg.Descbairro;
                        EnderecoRepository clsCep = new EnderecoRepository(_connection);
                        if (regEnd.CodLogradouro == 0)
                            regEnd.Cep = Functions.RetornaNumero(reg.Ee_cep) == "" ? "00000000" : Convert.ToInt32(Functions.RetornaNumero(reg.Ee_cep)).ToString("00000000");
                        else
                            regEnd.Cep = clsCep.RetornaCep(Convert.ToInt32(regEnd.CodLogradouro), Convert.ToInt16(reg.Ee_numimovel)) == 0 ? "00000000" : clsCep.RetornaCep(Convert.ToInt32(regEnd.CodLogradouro), Convert.ToInt16(reg.Ee_numimovel)).ToString("0000");
                    }
                } else if (Tipo == TipoEndereco.Proprietario) {
                    List<ProprietarioStruct> _lista_proprietario = Lista_Proprietario(Codigo, true);
                    int _codigo_proprietario = _lista_proprietario[0].Codigo;
                    CidadaoRepository clsCidadao = new CidadaoRepository(_connection);
                    CidadaoStruct _cidadao = clsCidadao.LoadReg(_codigo_proprietario);
                    if (_cidadao.EtiquetaC == "S") {
                        regEnd.Endereco = _cidadao.EnderecoC;
                        regEnd.Endereco_Abreviado = _cidadao.EnderecoC;
                        regEnd.Numero = _cidadao.NumeroC;
                        regEnd.Complemento = _cidadao.ComplementoC;
                        regEnd.CodigoBairro = _cidadao.CodigoBairroC;
                        regEnd.NomeBairro = _cidadao.NomeBairroC;
                        regEnd.CodigoCidade = _cidadao.CodigoCidadeC;
                        regEnd.NomeCidade = _cidadao.NomeCidadeC;
                        regEnd.UF = _cidadao.UfC;
                        regEnd.Cep = _cidadao.CepC.ToString();
                    } else {
                        regEnd.Endereco = _cidadao.EnderecoR;
                        regEnd.Endereco_Abreviado = _cidadao.EnderecoR;
                        regEnd.Numero = _cidadao.NumeroR;
                        regEnd.Complemento = _cidadao.ComplementoR;
                        regEnd.CodigoBairro = _cidadao.CodigoBairroR;
                        regEnd.NomeBairro = _cidadao.NomeBairroR;
                        regEnd.CodigoCidade = _cidadao.CodigoCidadeR;
                        regEnd.NomeCidade = _cidadao.NomeCidadeR;
                        regEnd.UF = _cidadao.UfR;
                        regEnd.Cep = _cidadao.CepR.ToString();
                    }
                }
            }

            return regEnd;
        }

        public List<ProprietarioStruct> Lista_Proprietario(int CodigoImovel, bool Principal = false) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                var reg = (from p in db.Proprietario
                           join c in db.Cidadao on p.Codcidadao equals c.Codcidadao
                           where p.Codreduzido == CodigoImovel
                           orderby p.Principal descending
                           select new { p.Codcidadao, c.Nomecidadao, p.Tipoprop, p.Principal, c.Cpf, c.Cnpj, c.Rg });

                if (Principal)
                    reg = reg.Where(u => u.Tipoprop == "P" && u.Principal == true);

                List<ProprietarioStruct> Lista = new List<ProprietarioStruct>();
                foreach (var query in reg) {
                    string sDoc;
                    if (!string.IsNullOrEmpty(query.Cpf) && query.Cpf.ToString().Length > 5)
                        sDoc = Convert.ToInt64(Functions.RetornaNumero(query.Cpf)).ToString("00000000000");
                    else {
                        if (!string.IsNullOrEmpty(query.Cnpj) && query.Cnpj.ToString().Length > 10)
                            sDoc = Functions.RetornaNumero(query.Cnpj);
                        else
                            sDoc = "";
                    }

                    ProprietarioStruct Linha = new ProprietarioStruct {
                        Codigo = query.Codcidadao,
                        Nome = query.Nomecidadao,
                        Tipo = query.Tipoprop,
                        Principal = Convert.ToBoolean(query.Principal),
                        CPF = sDoc,
                        RG = query.Rg
                    };
                    Lista.Add(Linha);
                }
                return Lista;
            }
        }

        public List<Testada> Lista_Testada(int Codigo) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                var reg = (from t in db.Testada where t.Codreduzido == Codigo orderby t.Numface select t).ToList();
                List<Testada> Lista = new List<Testada>();
                foreach (var item in reg) {
                    Testada Linha = new Testada {
                        Codreduzido = item.Codreduzido,
                        Numface = item.Numface,
                        Areatestada = item.Areatestada
                    };
                    Lista.Add(Linha);
                }
                return Lista;
            }
        }

        public List<AreaStruct> Lista_Area(int Codigo) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                var reg = (from a in db.Areas
                           join c in db.Categconstr on a.Catconstr equals c.Codcategconstr into ac from c in ac.DefaultIfEmpty()
                           join t in db.Tipoconstr on a.Tipoconstr equals t.Codtipoconstr into at from t in at.DefaultIfEmpty()
                           join u in db.Usoconstr on a.Usoconstr equals u.Codusoconstr into au from u in au.DefaultIfEmpty()
                           where a.Codreduzido == Codigo orderby a.Seqarea select new AreaStruct {
                               Data_Aprovacao = a.Dataaprova, Area = (decimal)a.Areaconstr, Categoria_Codigo = a.Catconstr,
                               Tipo_Nome = t.Desctipoconstr, Categoria_Nome = c.Desccategconstr, Data_Processo = a.Dataprocesso, Numero_Processo = a.Numprocesso, Pavimentos = a.Qtdepav, Seq = a.Seqarea,  Tipo_Codigo = a.Tipoconstr,
                               Uso_Codigo = a.Usoconstr, Uso_Nome = u.Descusoconstr
                           }).ToList();
                List<AreaStruct> Lista = new List<AreaStruct>();
                foreach (var item in reg) {
                    AreaStruct Linha = new AreaStruct {
                        Seq = item.Seq,
                        Area = item.Area,
                        Categoria_Codigo = item.Categoria_Codigo,
                        Categoria_Nome = item.Categoria_Nome,
                        Uso_Codigo = item.Uso_Codigo,
                        Uso_Nome = item.Uso_Nome,
                        Tipo_Codigo = item.Tipo_Codigo,
                        Tipo_Nome = item.Tipo_Nome,
                        Pavimentos = item.Pavimentos,
                        Numero_Processo = item.Numero_Processo,
                        Data_Processo = item.Data_Processo,
                        Data_Aprovacao = item.Data_Aprovacao
                      
                    };
                    Lista.Add(Linha);
                }
                return Lista;
            }
        }

    }
}