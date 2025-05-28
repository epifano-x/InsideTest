using InsideTest.Application;
using InsideTest.Domain.Requests;
using InsideTest.Domain.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InsideTest.Controllers
{
    [ApiController]
    [Route("api/pedidos")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpPost]
        public async Task<ActionResult<PedidoResponse>> PostPedido()
        {
            var result = await _pedidoService.IniciarPedidoAsync();
            return CreatedAtAction(nameof(GetPedidoById), new { id = result.Id }, result);
        }

        [HttpPost("{id}/produtos")]
        public async Task<ActionResult<PedidoResponse>> PostAdicionarProduto(Guid id, [FromBody] ProdutoRequest request)
        {
            var result = await _pedidoService.AdicionarProdutoAsync(id, request);
            return Ok(result);
        }

        [HttpDelete("{id}/produtos")]
        public async Task<ActionResult<PedidoResponse>> DeleteRemoverProduto(Guid id, [FromBody] ProdutoRequest request)
        {
            var result = await _pedidoService.RemoverProdutoAsync(id, request);
            return Ok(result);
        }

        [HttpPost("{id}/fechar")]
        public async Task<ActionResult<PedidoResponse>> PostFecharPedido(Guid id)
        {
            var result = await _pedidoService.FecharPedidoAsync(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoResponse>>> GetPedidos([FromQuery] string? status = null)
        {
            var result = await _pedidoService.ListarPedidosAsync(status);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoResponse>> GetPedidoById(Guid id)
        {
            var result = await _pedidoService.ObterPedidoAsync(id);
            return Ok(result);
        }
    }
}
