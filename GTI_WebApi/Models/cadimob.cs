using System;
using System.ComponentModel.DataAnnotations;

namespace GTI_WebApi.Models {
    public class Cadimob {
        [Key]
        public int Codreduzido { get; set; }
        public short Dv { get; set; }
        public int Codcondominio { get; set; }
        public short Distrito { get; set; }
        public short Setor { get; set; }
        public short Quadra { get; set; }
        public int Lote { get; set; }
        public short Seq { get; set; }
        public short Unidade { get; set; }
        public short Subunidade { get; set; }
        public short? Li_num { get; set; }
        public string Li_compl { get; set; }
        public string Li_cep { get; set; }
        public string Li_uf { get; set; }
        public short? Li_codcidade { get; set; }
        public short? Li_codbairro { get; set; }
        public string Li_quadras { get; set; }
        public string Li_lotes { get; set; }
        public decimal Dt_areaterreno { get; set; }
        public short? Dt_codusoterreno { get; set; }
        public short? Dt_codbenf { get; set; }
        public short? Dt_codtopog { get; set; }
        public short? Dt_codcategprop { get; set; }
        public short? Dt_codsituacao { get; set; }
        public short? Dt_codpedol { get; set; }
        public string Dt_numagua { get; set; }
        public decimal Dt_fracaoideal { get; set; }
        public short? Dc_qtdeedif { get; set; }
        public short? Dc_qtdepav { get; set; }
        public short Ee_tipoend { get; set; }
        public bool? Inativo { get; set; }
        public string Tipomat { get; set; }
        public Int64? Nummat { get; set; }
        public DateTime? Datainclusao { get; set; }
        public bool? Imune { get; set; }
        public bool? Conjugado { get; set; }
        public bool? Resideimovel { get; set; }
        public bool? Cip { get; set; }
    }
}