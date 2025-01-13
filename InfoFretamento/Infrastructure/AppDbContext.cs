using InfoFretamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfoFretamento.Infrastructure
{
    public class AppDbContext(DbContextOptions<AppDbContext> opts) : DbContext(opts)
    {


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

                entity.Property(p => p.DataNascimento).HasColumnType("DATE");
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

            modelBuilder.Entity<Motorista>(entity =>
            {
                entity.OwnsOne(e => e.Habilitacao, habilitacao =>
                {
                    habilitacao.Property(h => h.Vencimento).HasColumnType("DATE");
                });

                entity.Property(e => e.DataAdmissao).HasColumnType("DATE");
                entity.HasMany(e => e.Despesas).WithOne().HasForeignKey(d => d.ResponsavelId);
                entity.HasMany(e => e.Receitas).WithOne().HasForeignKey(d => d.ResponsavelId);
                entity.HasMany(e => e.Ferias).WithOne().HasForeignKey(f => f.ResponsavelId);
            });

            modelBuilder.Entity<Colaborador>(e =>
            {
                e.HasMany(e => e.Ferias).WithOne().HasForeignKey(f => f.ResponsavelId);
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Nome).HasMaxLength(100);
                entity.HasMany(e => e.Despesas).WithOne().HasForeignKey(d => d.ResponsavelId);
                entity.HasMany(e => e.Receitas).WithOne().HasForeignKey(d => d.ResponsavelId);
            });

            modelBuilder.Entity<Fornecedor>(entity =>
            {
                entity.Property(e => e.Nome).HasMaxLength(100);
                entity.HasMany(e => e.Despesas).WithOne().HasForeignKey(d => d.ResponsavelId);
                entity.HasMany(e => e.Receitas).WithOne().HasForeignKey(d => d.ResponsavelId);
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

            modelBuilder.Entity<Viagem>(entity =>
            {
                entity.Property(v => v.TipoServico)
                      .HasMaxLength(50);

                entity.Property(v => v.Status)
                      .HasMaxLength(20);

                entity.Property(v => v.ValorPago).HasColumnType("DECIMAL(18,2)");
                entity.Property(v => v.ValorContratado).HasColumnType("DECIMAL(18,2)");

                entity.OwnsOne(v => v.Rota, rota =>
                {
                    rota.OwnsOne(r => r.Saida, saida =>
                    {
                        saida.Property(s => s.UfSaida).HasMaxLength(2);
                        saida.Property(s => s.CidadeSaida).HasMaxLength(50);
                        saida.Property(s => s.LocalDeSaida).HasMaxLength(50);
                    });

                    rota.OwnsOne(r => r.Retorno, retorno =>
                    {
                        retorno.Property(r => r.UfSaida).HasMaxLength(2);
                        retorno.Property(r => r.CidadeSaida).HasMaxLength(50);
                        retorno.Property(s => s.LocalDeSaida).HasMaxLength(50);
                    });
                });

                entity.OwnsOne(v => v.DataHorarioChegada, horario =>
                {
                    horario.Property(h => h.Hora).HasMaxLength(5);
                    horario.Property(h => h.Data).HasColumnType("DATE");
                });

                entity.OwnsOne(v => v.DataHorarioRetorno, horario =>
                {
                    horario.Property(h => h.Hora).HasMaxLength(5);
                    horario.Property(h => h.Data).HasColumnType("DATE");
                });

                entity.OwnsOne(v => v.DataHorarioSaida, horario =>
                {
                    horario.Property(h => h.Hora).HasMaxLength(5);
                    horario.Property(h => h.Data).HasColumnType("DATE");
                });

                entity.OwnsOne(v => v.DataHorarioSaidaGaragem, horario =>
                {
                    horario.Property(h => h.Hora).HasMaxLength(5);
                    horario.Property(h => h.Data).HasColumnType("DATE");
                });

               

                entity.HasMany(v => v.Despesas)
                   .WithOne(p => p.Viagem)
                   .HasForeignKey(p => p.ViagemId)
                   .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(v => v.Receita)
                    .WithOne(p => p.Viagem)
                    .HasForeignKey<Receita>(v => v.ViagemId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Despesa>(entity =>
            {
                entity.Property(d => d.OrigemPagamento)
                     .HasMaxLength(100);

                entity.Property(d => d.ValorTotal)
                      .HasColumnType("DECIMAL(18,2)");

                entity.Property(d => d.DataCompra)
                      .HasColumnType("DATE");

                entity.Property(d => d.DataPagamento)
                      .HasColumnType("DATE");

                entity.Property(d => d.Vencimento)
                      .HasColumnType("DATE");

                entity.Property(e => e.FormaPagamento).HasMaxLength(10);

                entity.HasOne(e => e.Responsavel)
                      .WithMany()
                      .HasForeignKey(p => p.ResponsavelId);

                entity.Property(d => d.Descricao)
                   .HasMaxLength(150);

                
            });

            modelBuilder.Entity<Receita>(entity =>
            {
                entity.Property(d => d.OrigemPagamento)
                     .HasMaxLength(100);

                entity.Property(d => d.NumeroDocumento)
                      .HasMaxLength(50);

                entity.Property(d => d.ValorTotal)
                      .HasColumnType("DECIMAL(18,2)");

                entity.Property(d => d.DataCompra)
                      .HasColumnType("DATE");
                entity.Property(d => d.DataPagamento)
                      .HasColumnType("DATE");

                entity.Property(d => d.Vencimento)
                      .HasColumnType("DATE");

                entity.Property(d => d.ValorTotal)
                     .HasColumnType("DECIMAL(18,2)");

                entity.Property(e => e.FormaPagamento).HasMaxLength(10);

                entity.HasOne(e => e.Responsavel)
                      .WithMany()
                      .HasForeignKey(p => p.ResponsavelId);
            });

            modelBuilder.Entity<Manutencao>(entity =>
            {
                entity.Property(e => e.Tipo).HasMaxLength(20);
                entity.HasOne(e => e.Servico).WithMany().HasForeignKey(e => e.ServicoId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Veiculo).WithMany(v => v.Manutencoes).HasForeignKey(e => e.VeiculoId).OnDelete(DeleteBehavior.Restrict);
                entity.Property(e => e.DataVencimento).HasColumnType("DATE");
                entity.Property(e => e.DataLancamento).HasColumnType("DATE");
                entity.Property(e => e.DataRealizada).HasColumnType("DATE");
                entity.Property(e => e.Custo).HasColumnType("DECIMAL(18,2)");
            });

            modelBuilder.Entity<Servico>(entity =>
            {
                entity.Property(e => e.NomeServico).HasMaxLength(50);
            });

            modelBuilder.Entity<ViagemProgramada>(entity =>
            {
                entity.Property(e => e.Descricao).HasMaxLength(150);
                entity.Property(e => e.FormaPagto).HasMaxLength(14);
                entity.Property(e => e.Guia).HasMaxLength(20);
                entity.Property(e => e.Itinerario).HasMaxLength(100);
                entity.Property(e => e.Observacoes).HasMaxLength(100);
                entity.HasOne(e => e.Veiculo).WithMany(v => v.ViagensProgramadaas).HasForeignKey(e => e.VeiculoId);
            });



            modelBuilder.Entity<Passagem>(entity =>
            {
                entity.Property(e => e.DataEmissao).HasColumnType("DATE");
                entity.Property(e => e.FormaPagamento).HasMaxLength(15);
                entity.Property(e => e.Situacao).HasMaxLength(15);
                entity.HasOne(e => e.Viagem).WithMany(v => v.Passagens).HasForeignKey(e => e.ViagemId);
            });

            modelBuilder.Entity<Peca>(entity =>
            {
                entity.Property(e => e.Preco).HasColumnType("DECIMAL(18,2)");
            });

            modelBuilder.Entity<Documento>(entity =>
            {
                entity.Property(d => d.Vencimento).HasColumnType("DATE");
                entity.Property(d => d.Referencia)
                     .HasMaxLength(150);
                entity.Property(d => d.TipoDocumento)
                   .HasMaxLength(100);

            });

            modelBuilder.Entity<Ferias>(entity =>
            {
                entity.HasOne(f => f.Responsavel)
                 .WithMany()
                 .HasForeignKey(f => f.ResponsavelId);
                entity.Property(e => e.InicioFerias).HasColumnType("DATE");
                entity.Property(e => e.FimFerias).HasColumnType("DATE");
            });

            modelBuilder.Entity<MotoristaViagem>()
        .HasKey(mv => new { mv.MotoristaId, mv.ViagemId }); // Chave composta

            modelBuilder.Entity<MotoristaViagem>()
                .HasOne(mv => mv.Motorista)
                .WithMany(m => m.MotoristaViagens)
                .HasForeignKey(mv => mv.MotoristaId);

            modelBuilder.Entity<MotoristaViagem>()
                .HasOne(mv => mv.Viagem)
                .WithMany(v => v.MotoristaViagens)
                .HasForeignKey(mv => mv.ViagemId);

        }


        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Motorista> Motoristas { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<Viagem> Viagens { get; set; }
        public DbSet<MotoristaViagem> MotoristaViagens { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<Receita> Receitas { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Manutencao> Manutencoes { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<ViagemProgramada> ViagensProgramadas { get; set; }
        public DbSet<Passagem> Passagens { get; set; }
        public DbSet<Abastecimento> Abastecimentos { get; set; }
        public DbSet<Peca> Pecas { get; set; }
        public DbSet<Adiantamento> Adiantamentos { get; set; }
        public DbSet<RetiradaPeca> Retiradas { get; set; }
        public DbSet<AdicionarPeca> Adicionamentos { get; set; }
        public DbSet<Ferias> Ferias { get; set; }



    }
}
