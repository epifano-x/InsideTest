namespace InsideTest.Domain.Requests
{
    /// <summary>
    /// Representa os dados necessários para adicionar ou remover um produto de um pedido.
    /// </summary>
    public class ProdutoRequest
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
