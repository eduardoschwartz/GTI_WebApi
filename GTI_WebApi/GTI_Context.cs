using GTI_WebApi.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace GTI_WebApi {
    public class GTI_Context :DbContext{
        public GTI_Context(string Connection_Name) : base(Connection_Name ?? "gtiConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            Database.SetInitializer<GTI_Context>(null);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Conventions.Remove<DecimalPropertyConvention>();
            modelBuilder.Conventions.Add(new DecimalPropertyConvention(14, 4));

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<areas> Areas { get; set; }
        public DbSet<Bairro> Bairro { get; set; }
        public DbSet<Benfeitoria> Benfeitoria { get; set; }
        public DbSet<Cadimob> Cadimob { get; set; }
        public DbSet<Categconstr> Categconstr { get; set; }
        public DbSet<Categprop> Categprop { get; set; }
        public DbSet<Cep> Cep { get; set; }
        public DbSet<Cidadao> Cidadao { get; set; }
        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Condominio> Condominio { get; set; }
        public DbSet<Endentrega> Endentrega { get; set; }
        public DbSet<Facequadra> Facequadra { get; set; }
        public DbSet<Logradouro> Logradouro { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Pedologia> Pedologia { get; set; }
        public DbSet<Proprietario> Proprietario { get; set; }
        public DbSet<Situacao> Situacao { get; set; }
        public DbSet<Testada> Testada { get; set; }
        public DbSet<Tipoconstr> Tipoconstr { get; set; }
        public DbSet<Topografia> Topografia { get; set; }
        public DbSet<Usoconstr> Usoconstr { get; set; }
        public DbSet<Usoterreno> Usoterreno { get; set; }
        


    }
}