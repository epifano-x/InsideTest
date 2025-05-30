using System;
using System.Collections.Generic;

namespace InsideTest.Domain.Entities
{
    /// <summary>
    /// Representa um pedido realizado na loja.
    /// </summary>
    public class Pedido
    {
        /// <summary>
        /// Identificador único do pedido.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Data e hora da criação do pedido.
        /// </summary>
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Indica se o pedido foi fechado (true) ou ainda está aberto (false).
        /// </summary>
        public bool Fechado { get; set; } = false;

        /// <summary>
        /// Lista de produtos associados ao pedido.
        /// </summary>
        public List<Produto> Produtos { get; set; } = new();
    }
}
