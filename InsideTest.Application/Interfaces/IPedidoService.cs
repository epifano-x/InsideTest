using InsideTest.Domain.Requests;
using InsideTest.Domain.Responses;

namespace InsideTest.Application
{
    public interface IPedidoService
    {
        Task<PedidoResponse> IniciarPedidoAsync();
        Task<PedidoResponse> AdicionarProdutoAsync(Guid pedidoId, ProdutoRequest request);
        Task<PedidoResponse> RemoverProdutoAsync(Guid pedidoId, ProdutoRequest request);
        Task<PedidoResponse> FecharPedidoAsync(Guid pedidoId);
        Task<IEnumerable<PedidoResponse>> ListarPedidosAsync(string? status);
        Task<PedidoResponse> ObterPedidoAsync(Guid pedidoId);
    }
}
