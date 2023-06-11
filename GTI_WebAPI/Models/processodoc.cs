using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

public class Processodoc {
    [Key]
    [Column(Order = 1)]
    public short Ano { get; set; }
    [Key]
    [Column(Order = 2)]
    public int Numero { get; set; }
    [Key]
    [Column(Order = 3)]
    public short Coddoc { get; set; }
    public DateTime? Data { get; set; }
}