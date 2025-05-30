using System;

namespace InsideTest.Domain.Entities
{
    /// <summary>
    /// Representa um produto disponível em um pedido.
    /// </summary>
    public class Produto
    {
        /// <summary>
        /// Identificador único do produto.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

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
