using InsideTest.Domain.Entities;

namespace InsideTest.Domain.Interfaces
{
    /// <summary>
    /// Interface de acesso aos dados dos pedidos.
    /// </summary>
    public interface IPedidoRepository
    {
        /// <summary>
        /// Cria um novo pedido e persiste no banco de dados.
        /// </summary>
        /// <returns>O pedido recém-criado.</returns>
        Task<Pedido> CriarPedidoAsync();

        /// <summary>
        /// Obtém um pedido pelo seu ID, incluindo os produtos associados.
        /// </summary>
        /// <param name="id">ID do pedido.</param>
        /// <returns>O pedido correspondente.</returns>
        Task<Pedido> ObterPorIdAsync(Guid id);

        /// <summary>
        /// Lista pedidos com suporte a filtro por status e paginação.
        /// </summary>
        /// <param name="status">Status do pedido (\"aberto\", \"fechado\" ou null para todos).</param>
        /// <param name="page">Número da página.</param>
        /// <param name="pageSize">Tamanho da página.</param>
        /// <returns>Tupla com a lista de pedidos e o total de registros encontrados.</returns>
        Task<(IEnumerable<Pedido> Pedidos, int TotalItems)> ListarAsync(string? status, int page, int pageSize);

        /// <summary>
        /// Atualiza os dados de um pedido existente.
        /// </summary>
        /// <param name="pedido">Pedido com os dados atualizados.</param>
        /// <returns>Task representando a operação assíncrona.</returns>
        Task AtualizarAsync(Pedido pedido);
    }
}
