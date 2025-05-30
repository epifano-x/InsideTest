using InsideTest.Application;
using InsideTest.Domain.Requests;
using InsideTest.Domain.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InsideTest.Controllers
{
    /// <summary>
    /// Controlador responsável por gerenciar os pedidos da loja.
    /// </summary>
    [ApiController]
    [Route("api/pedidos")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        /// <summary>
        /// Inicia um novo pedido.
        /// </summary>
        /// <returns>O pedido recém-criado.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status201Created)]
        public async Task<ActionResult<PedidoResponse>> PostPedido()
        {
            var result = await _pedidoService.IniciarPedidoAsync();
            return CreatedAtAction(nameof(GetPedidoById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Adiciona um produto a um pedido existente.
        /// </summary>
        /// <param name="id">ID do pedido.</param>
        /// <param name="request">Dados do produto a ser adicionado.</param>
        /// <returns>O pedido atualizado.</returns>
        [HttpPost("{id}/produtos")]
        [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<PedidoResponse>> PostAdicionarProduto(Guid id, [FromBody] ProdutoRequest request)
        {
            var result = await _pedidoService.AdicionarProdutoAsync(id, request);
            return Ok(result);
        }

        /// <summary>
        /// Remove um produto de um pedido.
        /// </summary>
        /// <param name="id">ID do pedido.</param>
        /// <param name="request">Produto a ser removido.</param>
        /// <returns>O pedido atualizado.</returns>
        [HttpDelete("{id}/produtos")]
        [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<PedidoResponse>> DeleteRemoverProduto(Guid id, [FromBody] ProdutoRequest request)
        {
            var result = await _pedidoService.RemoverProdutoAsync(id, request);
            return Ok(result);
        }

        /// <summary>
        /// Fecha um pedido.
        /// </summary>
        /// <param name="id">ID do pedido.</param>
        /// <returns>O pedido fechado.</returns>
        [HttpPost("{id}/fechar")]
        [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<PedidoResponse>> PostFecharPedido(Guid id)
        {
            var result = await _pedidoService.FecharPedidoAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Lista os pedidos com suporte a paginação e filtro por status.
        /// </summary>
        /// <param name="status">Status do pedido (aberto ou fechado).</param>
        /// <param name="page">Número da página.</param>
        /// <param name="pageSize">Tamanho da página.</param>
        /// <returns>Lista paginada de pedidos.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<PedidoResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResult<PedidoResponse>>> GetPedidos(
            [FromQuery] string? status = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _pedidoService.ListarPedidosAsync(status, page, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Obtém um pedido específico pelo ID.
        /// </summary>
        /// <param name="id">ID do pedido.</param>
        /// <returns>O pedido com os produtos.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<PedidoResponse>> GetPedidoById(Guid id)
        {
            var result = await _pedidoService.ObterPedidoAsync(id);
            return Ok(result);
        }
    }
}
