using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServicoPedidos.Infra.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Infra.ConfiguracoesEntityType
{
    public class ClientesEntityTypeConfiguration : IEntityTypeConfiguration<ClienteModeloBD>
    {
        public void Configure(EntityTypeBuilder<ClienteModeloBD> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Nome).HasColumnName("nome");
        }
    }
}
