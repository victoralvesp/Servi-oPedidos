using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Dominio.Definicoes
{
    public static class Mensagens
    {
        public static string EXCECAO_QUANTIDADE_INVALIDA(string multiploDaQuantidade)
        {
            return String.Format("A quantidade do item deve ser um múltiplo de {0}", multiploDaQuantidade);
        }

        public static string EXCECAO_PRECO_INVALIDO(string preco)
        {
            return String.Format("{0} é um preco invalido para um item de pedido", preco);
        }

        public const string EXCECAO_RENTABILIDADE_INVALIDA = "Pedidos não podem conter itens com rentabilidade ruim.";
        

        public const string EXCECAO_PEDIDO_INVALIDO = "Pedidos devem ter ao menos um item";

    }
}
