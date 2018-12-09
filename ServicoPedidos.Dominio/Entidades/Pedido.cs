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
            var exceptions = new List<Exception>();

            if (Itens?.Count() > 0)
            {
                Rentabilidade menorRentabilidadeDosItens = Itens.Min(item => item.Rentabilidade);
                if (menorRentabilidadeDosItens <= Rentabilidade.Ruim)
                {
                    exceptions.Add(new RentabilidadeInvalidaException(Mensagens.EXCECAO_RENTABILIDADE_INVALIDA));
                }
                foreach (var item in Itens)
                {
                    try
                    {
                        item.Validar();
                    }
                    catch (Exception ex)
                    {
                        exceptions.Add(ex);
                    }
                }
            }
            else
            {
                throw new PedidoInvalidoException(Mensagens.EXCECAO_PEDIDO_INVALIDO_SEM_ITEM);
            }

            if (exceptions.Count() > 0)
            {
                Exception aggregate = new AggregateException(exceptions);

                throw new PedidoInvalidoException(Mensagens.EXCECAO_PEDIDO_INVALIDO, aggregate);
            }

        }
    }
}
