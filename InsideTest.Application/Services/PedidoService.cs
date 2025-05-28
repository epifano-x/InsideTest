using InsideTest.Domain.Entities;
using InsideTest.Domain.Interfaces;
using InsideTest.Domain.Requests;
using InsideTest.Domain.Responses;

namespace InsideTest.Application
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repository;

        public PedidoService(IPedidoRepository repository)
        {
            _repository = repository;
        }

        public async Task<PedidoResponse> IniciarPedidoAsync()
        {
            var pedido = await _repository.CriarPedidoAsync();
            return MapearParaResponse(pedido);
        }

        public async Task<PedidoResponse> AdicionarProdutoAsync(Guid pedidoId, ProdutoRequest request)
        {
            var pedido = await _repository.ObterPorIdAsync(pedidoId);
            if (pedido.Fechado) throw new InvalidOperationException("Pedido já está fechado.");

            pedido.Produtos.Add(new Produto { Nome = request.Nome, Preco = request.Preco });
            await _repository.AtualizarAsync(pedido);

            return MapearParaResponse(pedido);
        }

        public async Task<PedidoResponse> RemoverProdutoAsync(Guid pedidoId, ProdutoRequest request)
        {
            var pedido = await _repository.ObterPorIdAsync(pedidoId);
            if (pedido.Fechado) throw new InvalidOperationException("Pedido já está fechado.");

            var produto = pedido.Produtos.FirstOrDefault(p => p.Nome == request.Nome && p.Preco == request.Preco);
            if (produto != null)
                pedido.Produtos.Remove(produto);

            await _repository.AtualizarAsync(pedido);
            return MapearParaResponse(pedido);
        }

        public async Task<PedidoResponse> FecharPedidoAsync(Guid pedidoId)
        {
            var pedido = await _repository.ObterPorIdAsync(pedidoId);
            if (!pedido.Produtos.Any())
                throw new InvalidOperationException("Não é possível fechar um pedido vazio.");

            pedido.Fechado = true;
            await _repository.AtualizarAsync(pedido);

            return MapearParaResponse(pedido);
        }

        public async Task<IEnumerable<PedidoResponse>> ListarPedidosAsync(string? status)
        {
            var pedidos = await _repository.ListarAsync();
            if (status == "aberto") pedidos = pedidos.Where(p => !p.Fechado);
            if (status == "fechado") pedidos = pedidos.Where(p => p.Fechado);

            return pedidos.Select(MapearParaResponse);
        }

        public async Task<PedidoResponse> ObterPedidoAsync(Guid pedidoId)
        {
            var pedido = await _repository.ObterPorIdAsync(pedidoId);
            return MapearParaResponse(pedido);
        }

        private PedidoResponse MapearParaResponse(Pedido pedido)
        {
            return new PedidoResponse
            {
                Id = pedido.Id,
                DataCriacao = pedido.DataCriacao,
                Fechado = pedido.Fechado,
                Produtos = pedido.Produtos.Select(p => new ProdutoResponse
                {
                    Nome = p.Nome,
                    Preco = p.Preco
                }).ToList()
            };
        }
    }
}
