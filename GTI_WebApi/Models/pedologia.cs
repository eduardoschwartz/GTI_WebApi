using System.ComponentModel.DataAnnotations;

namespace GTI_WebApi.Models {
    public class Pedologia {
        [Key]
        public short Codpedologia { get; set; }
        public string Descpedologia { get; set; }
    }
}
