using InsideTest.Domain.Entities;

namespace InsideTest.Domain.Interfaces
{
    public interface IPedidoRepository
    {
        Task<Pedido> CriarPedidoAsync();
        Task<Pedido> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Pedido>> ListarAsync();
        Task AtualizarAsync(Pedido pedido);
    }
}
