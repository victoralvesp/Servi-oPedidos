using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using ServicoPedidos.Infra.ConfiguracoesEntityType;
using ServicoPedidos.Infra.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Infra.Contextos
{
    public class ContextoPedidos : DbContext
    {
        public ContextoPedidos(DbContextOptions options) : base(options)
        {
        }

        public DbSet<PedidoModeloBD> Pedidos { get; set; }
        public DbSet<ProdutoModeloBD> Produtos { get; set; }
        public DbSet<ClienteModeloBD> Clientes { get; set; }
        public DbSet<ItemDePedidoModeloBD> ItensDePedido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PedidosEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ItensDePedidoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ClientesEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProdutosEntityTypeConfiguration());
        }
    }
}
