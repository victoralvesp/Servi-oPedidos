using ServicoPedidos.Dominio.Abstracoes;
using ServicoPedidos.Dominio.Definicoes;
using ServicoPedidos.Dominio.Excecoes;
using ServicoPedidos.Dominio.Rentabilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServicoPedidos.Dominio
{
    public class Pedido : IPedido
    {
        public int Id { get; set; }

        public ICliente Cliente { get; }
        
        public IEnumerable<IItemDePedido> Itens { get; }

        public Pedido(ICliente cliente, IEnumerable<IItemDePedido> itens)
        {
            Cliente = cliente;
            Itens = itens;
        }

        public void Validar()
        {
            if(Itens?.Count() > 0)
            {
                foreach (var item in Itens)
                {
                    item.Validar();
                }
            }
            else
            {
                throw new PedidoInvalidoException(Mensagens.EXCECAO_PEDIDO_INVALIDO);
            }

            Rentabilidade menorRentabilidadeDosItens = Itens.Min(item => item.Rentabilidade);

            if(menorRentabilidadeDosItens <= Rentabilidade.Ruim)
            {
                throw new RentabilidadeInvalidaException(Mensagens.EXCECAO_RENTABILIDADE_INVALIDA);
            }
        }
    }
}
