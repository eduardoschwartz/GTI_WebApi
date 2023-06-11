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

        public DbSet<Anexo> Anexo { get; set; }
        public DbSet<Anexo_log> Anexo_log { get; set; }
        public DbSet<areas> Areas { get; set; }
        public DbSet<Assunto> Assunto { get; set; }
        public DbSet<Atividade> Atividade { get; set; }
        public DbSet<Atividadeiss> AtividadeIss { get; set; }
        public DbSet<Bairro> Bairro { get; set; }
        public DbSet<Benfeitoria> Benfeitoria { get; set; }
        public DbSet<Cadimob> Cadimob { get; set; }
        public DbSet<Categconstr> Categconstr { get; set; }
        public DbSet<Categprop> Categprop { get; set; }
        public DbSet<Centrocusto> Centrocusto { get; set; }
        public DbSet<Cep> Cep { get; set; }
        public DbSet<Cidadao> Cidadao { get; set; }
        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Cnae> Cnae { get; set; }
        public DbSet<Cnae_Aliquota> Cnae_Aliquota { get; set; }
        public DbSet<Cnaesubclasse> Cnaesubclass { get; set; }
        public DbSet<Condominio> Condominio { get; set; }
        public DbSet<Documento> Documento { get; set; }
        public DbSet<Endentrega> Endentrega { get; set; }
        public DbSet<Facequadra> Facequadra { get; set; }
        public DbSet<Horario_funcionamento> horario_Funcionamento { get; set; }
        public DbSet<Logradouro> Logradouro { get; set; }
        public DbSet<Mobiliario> Mobiliario { get; set; }
        public DbSet<Mobiliarioatividadeiss> Mobiliarioatividadeiss { get; set; }
        public DbSet<Mobiliariocnae> Mobiliariocnae { get; set; }
        public DbSet<Mobiliariovs> Mobiliariovs { get; set; }
        public DbSet<Mobiliarioevento> Mobiliarioevento { get; set; }
        public DbSet<mobiliarioplaca> Mobiliarioplaca { get; set; }
        public DbSet<Mobiliarioproprietario> mobiliarioproprietario { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Pedologia> Pedologia { get; set; }
        public DbSet<Periodomei> Periodomei { get; set; }
        public DbSet<Processodoc> Processodoc { get; set; }
        public DbSet<Processoend> Processoend { get; set; }
        public DbSet<Processogti> Processogti { get; set; }
        public DbSet<Proprietario> Proprietario { get; set; }
        public DbSet<Situacao> Situacao { get; set; }
        public DbSet<Tabelaiss> Tabelaiss { get; set; }
        public DbSet<Testada> Testada { get; set; }
        public DbSet<Tipoconstr> Tipoconstr { get; set; }
        public DbSet<Topografia> Topografia { get; set; }
        public DbSet<Usoconstr> Usoconstr { get; set; }
        public DbSet<Usoterreno> Usoterreno { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

    }
}