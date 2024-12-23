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


                entity.Property(v => v.TipoServico)
                      .HasMaxLength(50);

                entity.Property(v => v.Status)
                      .HasMaxLength(20);

                entity.Property(v => v.ValorPago).HasColumnType("decimal(18,2)");
                entity.Property(v => v.ValorContratado).HasColumnType("decimal(18,2)");

                entity.OwnsOne(v => v.Rota, rota =>
                {
                    // Configurando Saida
                    rota.OwnsOne(r => r.Saida, saida =>
                    {
                        saida.Property(s => s.UfSaida).HasMaxLength(2);
                        saida.Property(s => s.CidadeSaida).HasMaxLength(50);
                        saida.Property(s => s.LocalDeSaida).HasMaxLength(50);
                    });

                    // Configurando Retorno
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
                    horario.Property(h => h.Data).HasColumnType("date");
                });

                entity.OwnsOne(v => v.DataHorarioRetorno, horario =>
                {
                    horario.Property(h => h.Hora).HasMaxLength(5);
                    horario.Property(h => h.Data).HasColumnType("date");
                });
                entity.OwnsOne(v => v.DataHorarioSaida, horario =>
                {
                    horario.Property(h => h.Hora).HasMaxLength(5);
                    horario.Property(h => h.Data).HasColumnType("date");
                });
                entity.OwnsOne(v => v.DataHorarioSaidaGaragem, horario =>
                {
                    horario.Property(h => h.Hora).HasMaxLength(5);
                    horario.Property(h => h.Data).HasColumnType("date");
                });

                entity.HasOne(e => e.Motorista).WithMany(m => m.Viagens).HasForeignKey(e => e.MotoristaId);

                entity.HasMany<Despesa>(v => v.Despesas)
                   .WithOne(p => p.Viagem)
                   .HasForeignKey(p => p.ViagemId)
                   .OnDelete(DeleteBehavior.Cascade);

                // Relação com Receitas
                entity.HasMany<Receita>(v => v.Receitas)
                    .WithOne(p => p.Viagem)
                    .HasForeignKey(p => p.ViagemId)
                    .OnDelete(DeleteBehavior.Cascade);
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


                entity.Property(p => p.DataNascimento).HasColumnType("date");
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
                    habilitacao.Property(h => h.Vencimento).HasColumnType("date");
                });
                entity.HasMany(e => e.Despesas).WithOne().HasForeignKey(d => d.ResponsavelId);

                entity.HasMany(e => e.Receitas).WithOne().HasForeignKey(d => d.ResponsavelId);
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasMany(e => e.Despesas).WithOne().HasForeignKey(d => d.ResponsavelId);

                entity.HasMany(e => e.Receitas).WithOne().HasForeignKey(d => d.ResponsavelId);
            });

            modelBuilder.Entity<Fornecedor>(entity =>
            {
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

            modelBuilder.Entity<Despesa>(entity =>
            {
                entity.Property(d => d.OrigemPagamento)
                     .HasMaxLength(100);

                entity.Property(d => d.NumeroDocumento)
                      .HasMaxLength(50);

                entity.Property(d => d.ValorTotal)
                      .HasColumnType("decimal(18,2)");

                entity.Property(d => d.DataCompra)
                      .HasColumnType("date");
                entity.Property(d => d.DataEmissao)
                        .HasColumnType("date");

                entity.Property(d => d.Vencimento)
                      .HasColumnType("date");

                entity.Property(d => d.Pago)
                      .HasDefaultValue(false);



                entity.Property(d => d.ValorTotal)
                     .HasColumnType("decimal(18,2)");

                entity.Property(e => e.FormaPagamento).HasMaxLength(10);

                // Relação com Responsável
                entity.HasOne(e => e.Responsavel)
                      .WithMany()
                      .HasForeignKey(p => p.ResponsavelId);
            });


            modelBuilder.Entity<Receita>(entity =>
            {
                entity.Property(d => d.OrigemPagamento)
                     .HasMaxLength(100);

                entity.Property(d => d.NumeroDocumento)
                      .HasMaxLength(50);

                entity.Property(d => d.ValorTotal)
                      .HasColumnType("decimal(18,2)");

                entity.Property(d => d.DataCompra)
                      .HasColumnType("date");
                entity.Property(d => d.DataEmissao)
                        .HasColumnType("date");

                entity.Property(d => d.Vencimento)
                      .HasColumnType("date");

                entity.Property(d => d.Pago)
                      .HasDefaultValue(false);



                entity.Property(d => d.ValorTotal)
                     .HasColumnType("decimal(18,2)");

                entity.Property(e => e.FormaPagamento).HasMaxLength(10);


                // Relação com Responsável
                entity.HasOne(e => e.Responsavel)
                      .WithMany()
                      .HasForeignKey(p => p.ResponsavelId);
            });


            modelBuilder.Entity<Manutencao>(entity =>
            {
                entity.Property(e => e.Tipo).HasMaxLength(20);
                entity.HasOne(e => e.Servico).WithMany().HasForeignKey(e => e.ServicoId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Veiculo).WithMany(v => v.Manutencoes).HasForeignKey(e => e.VeiculoId).OnDelete(DeleteBehavior.Restrict);
                entity.Property(e => e.DataVencimento).HasColumnType("date");
                entity.Property(e => e.DataLancamento).HasColumnType("date");
                entity.Property(e => e.DataRealizada).HasColumnType("date");
                entity.Property(e => e.Custo).HasColumnType("decimamal(18,2)");
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

            modelBuilder.Entity<Passageiro>(entity =>
            {
                entity.Property(e => e.Matricula).HasMaxLength(50);
                entity.Property(e => e.Cartao).HasMaxLength(50);

            });

            modelBuilder.Entity<Passagem>(entity =>
            {
                entity.Property(e => e.DataEmissao).HasColumnType("date");
                entity.Property(e => e.FormaPagamento).HasMaxLength(15);
                entity.Property(e => e.Situacao).HasMaxLength(15);
                entity.HasOne(e => e.Viagem).WithMany(v => v.Passagens).HasForeignKey(e => e.ViagemId);
                entity.HasOne(e => e.Passageiro).WithMany(v => v.Passagens).HasForeignKey(e => e.PassageiroId);
            });


        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Motorista> Motoristas { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<Viagem> Viagens { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<Receita> Receitas { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Manutencao> Manutencoes { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<ViagemProgramada> ViagensProgramadas { get; set; }
        public DbSet<Passageiro> Passageiros { get; set; }
        public DbSet<Passagem> Passagens { get; set; }


    }
}
