using System;
using System.Collections.Generic;

namespace InsideTest.Domain.Responses
{
    /// <summary>
    /// Representa os dados de saída de um pedido retornado pela API.
    /// </summary>
    public class PedidoResponse
    {
        /// <summary>
        /// Identificador único do pedido.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Data e hora da criação do pedido.
        /// </summary>
        public DateTime DataCriacao { get; set; }

        /// <summary>
        /// Indica se o pedido está fechado.
        /// </summary>
        public bool Fechado { get; set; }

        /// <summary>
        /// Lista de produtos contidos no pedido.
        /// </summary>
        public List<ProdutoResponse> Produtos { get; set; } = new();
    }

    /// <summary>
    /// Representa os dados de um produto incluído no pedido.
    /// </summary>
    public class ProdutoResponse
    {
        /// <summary>
        /// Nome do produto.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Preço do produto.
        /// </summary>
        public decimal Preco { get; set; }
    }
}
