using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServicoPedidos.Infra.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Infra.ConfiguracoesEntityType
{
    public class ItensDePedidoEntityTypeConfiguration : IEntityTypeConfiguration<ItemDePedidoModeloBD>
    {
        public void Configure(EntityTypeBuilder<ItemDePedidoModeloBD> builder)
        {
            builder.ToTable("Itens_de_Pedido");
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.PrecoUnitario);

            builder.HasOne(x => x.Produto).WithOne().HasForeignKey<ItemDePedidoModeloBD>(x => x.IdProduto);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.IdPedido).HasColumnName("id_pedido");
            builder.Property(x => x.IdProduto).HasColumnName("id_produto");

            builder.Property(x => x.Quantidade).HasColumnName("quantidade");

            builder.Property(x => x.PrecoUnitarioValor).HasColumnName("preco_unitario");
            builder.Property(x => x.PrecoUnitarioMoeda).HasColumnName("preco_unitario_moeda");
        }
    }
}
