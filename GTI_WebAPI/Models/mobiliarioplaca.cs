﻿using System.ComponentModel.DataAnnotations;

namespace GTI_WebApi.Models {
    public class mobiliarioplaca {
        [Key]
        public int Codigo { get; set; }
        public string placa { get; set; }
    }
}
