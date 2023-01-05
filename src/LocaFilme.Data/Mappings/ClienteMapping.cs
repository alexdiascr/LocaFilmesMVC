using LocaFilme.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocaFilme.Data.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Nome)
              .IsRequired()
              .HasColumnType("varchar(200)");

            builder.Property(e => e.Documento)
                .IsRequired()
                .HasColumnType("varchar(11)");

            builder.Property(e => e.Ativo)
                .IsRequired();


            builder.ToTable("Cliente"); //definindo o nome da tabela
        }
    }
}
