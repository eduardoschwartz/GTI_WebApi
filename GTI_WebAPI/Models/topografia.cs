﻿using System.ComponentModel.DataAnnotations;

namespace GTI_WebApi.Models {
    public class Topografia {
        [Key]
        public short Codtopografia { get; set; }
        public string Desctopografia { get; set; }
    }
}
