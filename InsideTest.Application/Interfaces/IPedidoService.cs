using InsideTest.Domain.Requests;
using InsideTest.Domain.Responses;

namespace InsideTest.Application
{
    /// <summary>
    /// Interface para os serviços relacionados à manipulação de pedidos.
    /// </summary>
    public interface IPedidoService
    {
        /// <summary>
        /// Inicia um novo pedido.
        /// </summary>
        /// <returns>O pedido recém-criado.</returns>
        Task<PedidoResponse> IniciarPedidoAsync();

        /// <summary>
        /// Adiciona um produto a um pedido existente.
        /// </summary>
        /// <param name="pedidoId">ID do pedido.</param>
        /// <param name="request">Dados do produto a ser adicionado.</param>
        /// <returns>O pedido atualizado.</returns>
        Task<PedidoResponse> AdicionarProdutoAsync(Guid pedidoId, ProdutoRequest request);

        /// <summary>
        /// Remove um produto de um pedido existente.
        /// </summary>
        /// <param name="pedidoId">ID do pedido.</param>
        /// <param name="request">Dados do produto a ser removido.</param>
        /// <returns>O pedido atualizado.</returns>
        Task<PedidoResponse> RemoverProdutoAsync(Guid pedidoId, ProdutoRequest request);

        /// <summary>
        /// Fecha um pedido existente, impedindo alterações futuras.
        /// </summary>
        /// <param name="pedidoId">ID do pedido.</param>
        /// <returns>O pedido fechado.</returns>
        Task<PedidoResponse> FecharPedidoAsync(Guid pedidoId);

        /// <summary>
        /// Lista os pedidos com suporte a paginação e filtro por status.
        /// </summary>
        /// <param name="status">Status dos pedidos: "aberto", "fechado" ou null para todos.</param>
        /// <param name="page">Número da página.</param>
        /// <param name="pageSize">Quantidade de itens por página.</param>
        /// <returns>Resultado paginado com os pedidos.</returns>
        Task<PagedResult<PedidoResponse>> ListarPedidosAsync(string? status, int page, int pageSize);

        /// <summary>
        /// Obtém um pedido pelo seu identificador.
        /// </summary>
        /// <param name="pedidoId">ID do pedido.</param>
        /// <returns>O pedido encontrado.</returns>
        Task<PedidoResponse> ObterPedidoAsync(Guid pedidoId);
    }
}
