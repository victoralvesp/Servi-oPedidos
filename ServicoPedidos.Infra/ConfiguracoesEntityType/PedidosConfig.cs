using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServicoPedidos.Infra.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Infra.ConfiguracoesEntityType
{
    public class PedidosEntityTypeConfiguration : IEntityTypeConfiguration<PedidoModeloBD>
    {
        public void Configure(EntityTypeBuilder<PedidoModeloBD> builder)
        {
            builder.ToTable("Pedidos");
            builder.HasKey(x => x.Id);

            builder.Ignore(x => x.Itens);

            builder.HasMany(x => x.ItensBD).WithOne().HasForeignKey(x => x.IdPedido);


            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.IdCliente).HasColumnName("id_cliente");
        }
    }
}
