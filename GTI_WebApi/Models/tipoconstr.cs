using System.ComponentModel.DataAnnotations;

namespace GTI_WebApi.Models {
    public class Tipoconstr {
        [Key]
        public short Codtipoconstr { get; set; }
        public string Desctipoconstr { get; set; }
    }
}
