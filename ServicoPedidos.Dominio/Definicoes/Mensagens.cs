using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Dominio.Definicoes
{
    public static class Mensagens
    {
        public static string EXCECAO_QUANTIDADE_INVALIDA(params string[] args)
        {
            return String.Format("A quantidade do item deve ser um múltiplo de {0}", args);
        }

        public const string EXCECAO_RENTABILIDADE_INVALIDA = "Pedidos não podem conter itens com rentabilidade ruim.";
        

        public const string EXCECAO_PEDIDO_INVALIDO = "Pedidos devem ter ao menos um item";

    }
}
