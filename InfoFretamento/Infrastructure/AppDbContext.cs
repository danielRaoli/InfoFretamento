using InfoFretamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfoFretamento.Infrastructure
{
    public class AppDbContext(DbContextOptions<AppDbContext> opts) : DbContext(opts)
    {


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Viagem>(entity =>
            {
                entity.Property(v => v.HorarioSaida)
                      .HasMaxLength(5);   // Exemplo: "08:30"

                entity.Property(v => v.HorarioRetorno)
                      .HasMaxLength(5);

                entity.Property(v => v.TipoServico)
                      .HasMaxLength(50);

                entity.Property(v => v.Status)
                      .HasMaxLength(20);

                entity.OwnsOne(v => v.Rota, rota =>
                {
                    // Configurando Saida
                    rota.OwnsOne(r => r.Saida, saida =>
                    {
                        saida.Property(s => s.UfSaida).HasMaxLength(2);
                        saida.Property(s => s.CidadeDestino).HasMaxLength(50);
                        saida.Property(s => s.CidadeSaida).HasMaxLength(50);
                    });

                    // Configurando Retorno
                    rota.OwnsOne(r => r.Retorno, retorno =>
                    {
                        retorno.Property(r => r.UfSaida).HasMaxLength(2);
                        retorno.Property(r => r.CidadeDestino).HasMaxLength(50);
                        retorno.Property(r => r.CidadeSaida).HasMaxLength(50);
                    });

                });
            });
            // Configuração de tipos complexos com Owned Types



            modelBuilder.Entity<Pessoa>(entity =>
            {
                entity.Property(p => p.Nome)
                      .HasMaxLength(100)   // Nome limitado a 100 caracteres
                      .IsRequired();

                entity.Property(p => p.Telefone)
                      .HasMaxLength(15);   // Telefone no formato (99) 99999-9999

                entity.Property(p => p.Cpf)
                      .HasMaxLength(14)    // CPF no formato 999.999.999-99
                      .IsFixedLength();    // Tamanho fixo para garantir consistência

                entity.OwnsOne(p => p.Endereco, endereco =>
                {
                    endereco.Property(e => e.Cidade).HasMaxLength(100);
                    endereco.Property(e => e.Rua).HasMaxLength(150);
                    endereco.Property(e => e.Bairro).HasMaxLength(100);
                    endereco.Property(e => e.Numero).HasMaxLength(10);
                    endereco.Property(e => e.Uf).HasMaxLength(2);
                });

                entity.OwnsOne(p => p.Documento, documento =>
                {
                    documento.Property(d => d.Documento).HasMaxLength(20);
                    documento.Property(d => d.Tipo).HasMaxLength(20);
                });

            });


            modelBuilder.Entity<Veiculo>(entity =>
            {
                entity.Property(v => v.Prefixo)
                      .HasMaxLength(10);

                entity.Property(v => v.Placa)
                      .HasMaxLength(8)   // Exemplo: "ABC-1234"
                      .IsRequired();

                entity.Property(v => v.Marca)
                      .HasMaxLength(50);

                entity.Property(v => v.LocalEmplacado)
                      .HasMaxLength(100);

                entity.Property(v => v.Carroceria)
                      .HasMaxLength(50);

                entity.Property(v => v.Tipo)
                      .HasMaxLength(40);

                entity.Property(v => v.Modelo).HasMaxLength(50);

                entity.Property(v => v.QuantidadePoltronas)
                      .HasDefaultValue(0); 
            });

            modelBuilder.Entity<Despesa>(entity =>
            {
                entity.Property(d => d.DestinoPagamento)
                      .HasMaxLength(100);   
                  
                entity.Property(d => d.NumeroDocumento)
                      .HasMaxLength(20);   

                entity.Property(d => d.ValorTotal)
                      .HasColumnType("decimal(18,2)");  

                entity.Property(d => d.DataLancamento)
                      .HasColumnType("date");           

                entity.Property(d => d.DataCompra)
                      .HasColumnType("date");

                entity.Property(d => d.Vencimento)
                      .HasColumnType("date");

                entity.Property(d => d.Pago)
                      .HasDefaultValue(false);         

                entity.HasOne(d => d.GrupoCusto)       
                      .WithMany(g => g.Despesas)
                      .HasForeignKey(d => d.GrupoCustoId)
                      .OnDelete(DeleteBehavior.Restrict); 

                entity.HasOne(d => d.Veiculo)          
                      .WithMany(v => v.Despesas)                      
                      .HasForeignKey(d => d.VeiculoId)
                      .OnDelete(DeleteBehavior.Restrict); 
            });

            // Configuração para GrupoDeCusto
            modelBuilder.Entity<GrupoDeCusto>(entity =>
            {
                entity.Property(g => g.Nome)
                      .HasMaxLength(100)    // Limitar nome a 100 caracteres
                      .IsRequired();        // Nome obrigatório
            });

            modelBuilder.Entity<Motorista>(entity =>
            {
                entity.Property(e => e.Cpf).HasMaxLength(14);
                entity.Property(e => e.DataNascimento).HasColumnType("date");
                entity.OwnsOne(e => e.Habilitacao, habilitacao =>
                {
                    habilitacao.Property(h => h.Vencimento).HasColumnType("date");
                    habilitacao.Property(h => h.Uf).HasMaxLength(2);
                    habilitacao.Property(h => h.Categoria).HasMaxLength(3);
                    habilitacao.Property(h => h.Protocolo).HasMaxLength(20);
                });

                entity.Property(e => e.Telefone).HasMaxLength(15);
            });

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Motorista> Motoristas { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<Viagem> Viagens { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<GrupoDeCusto> GruposDeCusto { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
