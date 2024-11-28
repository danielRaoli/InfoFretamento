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
                    rota.Property(r => r.Saida.UfSaida).HasMaxLength(2);
                    rota.Property(r => r.Saida.CidadeDestino).HasMaxLength(50);
                    rota.Property(r => r.Saida.CidadeSaida).HasMaxLength(50);
                    rota.Property(r => r.Retorno.UfSaida).HasMaxLength(2);
                    rota.Property(r => r.Retorno.CidadeDestino).HasMaxLength(50);
                    rota.Property(r => r.Retorno.CidadeSaida).HasMaxLength(50);
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
                });
            });

            modelBuilder.Entity<Habilitacao>(entity =>
            {
                entity.Property(h => h.Protocolo)
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(h => h.Categoria)
                      .HasMaxLength(3);  // Exemplo: "A", "B", "AB", etc.

                entity.Property(h => h.Cidade)
                      .HasMaxLength(100);

                entity.Property(h => h.Uf)
                      .HasMaxLength(2)   // Exemplo: "SP", "RJ"
                      .IsFixedLength();
            });

            modelBuilder.Entity<Veiculo>(entity =>
            {
                entity.Property(v => v.Prefixo)
                      .HasMaxLength(10);

                entity.Property(v => v.Placa)
                      .HasMaxLength(7)   // Exemplo: "ABC1234"
                      .IsRequired();

                entity.Property(v => v.Marca)
                      .HasMaxLength(50);

                entity.Property(v => v.LocalEmplacado)
                      .HasMaxLength(100);

                entity.Property(v => v.Carroceria)
                      .HasMaxLength(50);

                entity.Property(v => v.Tipo)
                      .HasMaxLength(30);

                entity.Property(v => v.QuantidadePoltronas)
                      .HasDefaultValue(0);  // Definir um valor padrão
            });

            modelBuilder.Entity<Despesa>(entity =>
            {
                entity.Property(d => d.DestinoPagamento)
                      .HasMaxLength(100)    // Limitar a 100 caracteres
                      .IsRequired();        // Campo obrigatório

                entity.Property(d => d.NumeroDocumento)
                      .HasMaxLength(20)     // Limitar a 20 caracteres (ex: "12345-67890")
                      .IsRequired();        // Documento obrigatório

                entity.Property(d => d.ValorTotal)
                      .HasColumnType("decimal(18,2)");  // Definir precisão e escala para valores monetários

                entity.Property(d => d.DataLancamento)
                      .HasColumnType("date");           // Armazenar apenas a data

                entity.Property(d => d.DataCompra)
                      .HasColumnType("date");

                entity.Property(d => d.Vencimento)
                      .HasColumnType("date");

                entity.Property(d => d.Pago)
                      .HasDefaultValue(false);          // Valor padrão como não pago

                entity.HasOne(d => d.GrupoCusto)        // Relacionamento com GrupoDeCusto
                      .WithMany(g => g.Despesas)
                      .HasForeignKey(d => d.GrupoCustoId)
                      .OnDelete(DeleteBehavior.Restrict); // Evita exclusão em cascata

                entity.HasOne(d => d.Veiculo)           // Relacionamento com Veiculo
                      .WithMany(v => v.Despesas)                       // Defina se Veiculo terá uma coleção de despesas
                      .HasForeignKey(d => d.VeiculoId)
                      .OnDelete(DeleteBehavior.Restrict); // Evita exclusão em cascata
            });

            // Configuração para GrupoDeCusto
            modelBuilder.Entity<GrupoDeCusto>(entity =>
            {
                entity.Property(g => g.Nome)
                      .HasMaxLength(100)    // Limitar nome a 100 caracteres
                      .IsRequired();        // Nome obrigatório
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
        public DbSet<Habilitacao> Habilitacoes { get; set; }



    }
}
