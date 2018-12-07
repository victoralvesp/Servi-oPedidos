using ServicoPedidos.Dominio.Abstracoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Infra.Modelos
{
    public class ItemDePedidoModeloBD : IItemDePedidoDTO
    {
        public int Id { get; set; }
        public int IdPedido { get; internal set; }
        public int IdProduto { get; set; }
        public ProdutoModeloBD Produto { get; set; }
        public int Quantidade { get; set; }
        public ValorMonetario PrecoUnitario { get; set; }
        
        public string PrecoUnitarioMoeda
        {
            get
            {
                if(PrecoUnitario.Moeda != null)
                {
                    return PrecoUnitario.Moeda.Id;
                }
                else
                {
                    return Moeda.Real.Id;
                }
            }

            set
            {
                if (value != null)
                {
                    PrecoUnitario = new ValorMonetario(PrecoUnitario.Valor, Moeda.Obtem(value));
                }
                else
                {
                    PrecoUnitario = new ValorMonetario(PrecoUnitario.Valor);
                }
            }
        }

        public decimal PrecoUnitarioValor
        {
            get => PrecoUnitario.Valor;
            set => PrecoUnitario = new ValorMonetario(value, Moeda.Obtem(PrecoUnitarioMoeda));
        }

        internal ItemDePedidoModeloBD()
        {
        }

        public ItemDePedidoModeloBD(IItemDePedidoDTO itemDTO)
        {
            Id = itemDTO.Id;
            IdProduto = itemDTO.IdProduto;
            Quantidade = itemDTO.Quantidade;
            PrecoUnitario = itemDTO.PrecoUnitario;
        }

    }
}
