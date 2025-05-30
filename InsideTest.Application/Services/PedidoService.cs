using InsideTest.Domain.Entities;
using InsideTest.Domain.Interfaces;
using InsideTest.Domain.Requests;
using InsideTest.Domain.Responses;

namespace InsideTest.Application
{
    /// <summary>
    /// Serviço responsável pelas operações de negócio relacionadas aos pedidos.
    /// </summary>
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repository;

        /// <summary>
        /// Construtor do serviço de pedidos.
        /// </summary>
        /// <param name="repository">Repositório de pedidos.</param>
        public PedidoService(IPedidoRepository repository)
        {
            _repository = repository;
        }

        /// <inheritdoc/>
        public async Task<PedidoResponse> IniciarPedidoAsync()
        {
            var pedido = await _repository.CriarPedidoAsync();
            return MapearParaResponse(pedido);
        }

        /// <inheritdoc/>
        public async Task<PedidoResponse> AdicionarProdutoAsync(Guid pedidoId, ProdutoRequest request)
        {
            var pedido = await _repository.ObterPorIdAsync(pedidoId);
            if (pedido.Fechado)
                throw new InvalidOperationException("Pedido já está fechado.");

            pedido.Produtos.Add(new Produto { Nome = request.Nome, Preco = request.Preco });
            await _repository.AtualizarAsync(pedido);

            return MapearParaResponse(pedido);
        }

        /// <inheritdoc/>
        public async Task<PedidoResponse> RemoverProdutoAsync(Guid pedidoId, ProdutoRequest request)
        {
            var pedido = await _repository.ObterPorIdAsync(pedidoId);
            if (pedido.Fechado)
                throw new InvalidOperationException("Pedido já está fechado.");

            var produto = pedido.Produtos.FirstOrDefault(p => p.Nome == request.Nome && p.Preco == request.Preco);
            if (produto != null)
                pedido.Produtos.Remove(produto);

            await _repository.AtualizarAsync(pedido);
            return MapearParaResponse(pedido);
        }

        /// <inheritdoc/>
        public async Task<PedidoResponse> FecharPedidoAsync(Guid pedidoId)
        {
            var pedido = await _repository.ObterPorIdAsync(pedidoId);
            if (!pedido.Produtos.Any())
                throw new InvalidOperationException("Não é possível fechar um pedido vazio.");

            pedido.Fechado = true;
            await _repository.AtualizarAsync(pedido);

            return MapearParaResponse(pedido);
        }

        /// <inheritdoc/>
        public async Task<PagedResult<PedidoResponse>> ListarPedidosAsync(string? status, int page, int pageSize)
        {
            var (pedidos, totalItems) = await _repository.ListarAsync(status, page, pageSize);

            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            return new PagedResult<PedidoResponse>
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = pedidos.Select(MapearParaResponse)
            };
        }

        /// <inheritdoc/>
        public async Task<PedidoResponse> ObterPedidoAsync(Guid pedidoId)
        {
            var pedido = await _repository.ObterPorIdAsync(pedidoId);
            return MapearParaResponse(pedido);
        }

        /// <summary>
        /// Mapeia uma entidade Pedido para seu modelo de resposta.
        /// </summary>
        /// <param name="pedido">Entidade de pedido.</param>
        /// <returns>Objeto de resposta com dados do pedido.</returns>
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
