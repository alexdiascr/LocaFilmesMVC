using LocaFilme.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocaFilme.Data.Mappings
{
    public class LocacaoMapping : IEntityTypeConfiguration<Locacao>
    {
        public void Configure(EntityTypeBuilder<Locacao> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.DataCadastro)
                .IsRequired()
                .HasColumnType("Datetime")
                .HasDefaultValue(DateTime.Now);

            builder.Property(p => p.DataAtualizacao)
                .IsRequired()
                .HasColumnType("Datetime")
                .HasDefaultValue(DateTime.Now);

            builder.Property(p => p.DataDevolucao)
                .IsRequired()
                .HasColumnType("Datetime");

            builder.Property(p => p.Valor)
                .IsRequired();

            // 1 : 1 => Locacao : Cliente

            builder.HasOne(l => l.Cliente)
                .WithOne(c => c.Locacao);

            // 1 : N => Locacao : Filmes
            builder.HasMany(l => l.Produtos)
                .WithOne(f => f.Locacao)
                .HasForeignKey(f => f.LocacaoId);

            builder.ToTable("Locacoes"); //definindo o nome da tabela
        }
    }
}
