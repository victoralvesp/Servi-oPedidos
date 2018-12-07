using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServicoPedidos.Infra.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Infra.ConfiguracoesEntityType
{
    public class ProdutosEntityTypeConfiguration : IEntityTypeConfiguration<ProdutoModeloBD>
    {
        public void Configure(EntityTypeBuilder<ProdutoModeloBD> builder)
        {
            builder.ToTable("Produtos");
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.PrecoSugerido);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Nome).HasColumnName("nome");
            builder.Property(x => x.Multiplo).HasColumnName("multiplo");
            builder.Property(x => x.PrecoSugeridoValor).HasColumnName("preco_sugerido");
            builder.Property(x => x.PrecoSugeridoMoeda).HasColumnName("preco_sugerido_moeda");
        }
    }
}
