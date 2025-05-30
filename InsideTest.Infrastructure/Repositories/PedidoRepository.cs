using InsideTest.Domain.Entities;
using InsideTest.Domain.Interfaces;
using InsideTest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InsideTest.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório responsável por acessar e manipular dados dos pedidos no banco de dados.
    /// </summary>
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Construtor que injeta o contexto do banco de dados.
        /// </summary>
        /// <param name="context">Contexto da aplicação.</param>
        public PedidoRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<Pedido> CriarPedidoAsync()
        {
            var pedido = new Pedido();
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        /// <inheritdoc/>
        public async Task<Pedido> ObterPorIdAsync(Guid id)
        {
            return await _context.Pedidos.Include(p => p.Produtos).FirstAsync(p => p.Id == id);
        }

        /// <inheritdoc/>
        public async Task<(IEnumerable<Pedido> Pedidos, int TotalItems)> ListarAsync(string? status, int page, int pageSize)
        {
            IQueryable<Pedido> query = _context.Pedidos.Include(p => p.Produtos);

            if (status == "aberto") query = query.Where(p => !p.Fechado);
            if (status == "fechado") query = query.Where(p => p.Fechado);

            var totalItems = await query.CountAsync();

            var pedidos = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (pedidos, totalItems);
        }

        /// <inheritdoc/>
        public async Task AtualizarAsync(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
        }
    }
}
