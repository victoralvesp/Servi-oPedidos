using ServicoPedidos.Dominio.Abstracoes;
using ServicoPedidos.Dominio.Definicoes;
using ServicoPedidos.Dominio.Excecoes;
using ServicoPedidos.Dominio.Rentabilidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Dominio
{
    public class ItemDePedido : IItemDePedido
    {
        public int Id { get; set; }
        public IProduto Produto { get; }
        private IAnalisadorDeRentabilidade _analisadorDeRentabilidade = new AnalisadorDeRentabilidade();

        private Lazy<Rentabilidade> _carregamentoDeRentabilidade;

        public ItemDePedido(IProduto produto, ValorMonetario precoUnitario, int quantidade, Rentabilidade rentabilidade = null)
        {
            Produto = produto;
            PrecoUnitario = precoUnitario;
            Quantidade = quantidade;

            if (rentabilidade == null)
            {
                _carregamentoDeRentabilidade = new Lazy<Rentabilidade>(() => _analisadorDeRentabilidade.CalcularRentabilidade(Produto.PrecoSugerido, PrecoUnitario));
            }
            else
            {
                _carregamentoDeRentabilidade = new Lazy<Rentabilidade>(() => rentabilidade);
            }
        }

        public Rentabilidade Rentabilidade => _carregamentoDeRentabilidade.Value;

        public ValorMonetario PrecoUnitario { get; }

        public int Quantidade { get; }

        public void Validar()
        {
            if(Quantidade == 0 || Quantidade % Produto.Multiplo != 0)
            {
                throw new QuantidadeInvalidaException(Mensagens.EXCECAO_QUANTIDADE_INVALIDA(Produto.Multiplo.ToString()));
            }
            
            if(PrecoUnitario.Valor <= 0)
            {
                throw new PrecoInvalidoException(Mensagens.EXCECAO_PRECO_INVALIDO(PrecoUnitario.ToString()));
            }
        }
    }
}
