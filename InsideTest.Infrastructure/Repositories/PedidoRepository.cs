using InsideTest.Domain.Entities;
using InsideTest.Domain.Interfaces;
using InsideTest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InsideTest.Infrastructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;

        public PedidoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Pedido> CriarPedidoAsync()
        {
            var pedido = new Pedido();
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        public async Task<Pedido> ObterPorIdAsync(Guid id)
        {
            return await _context.Pedidos.Include(p => p.Produtos).FirstAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Pedido>> ListarAsync()
        {
            return await _context.Pedidos.Include(p => p.Produtos).ToListAsync();
        }

        public async Task AtualizarAsync(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
        }
    }
}
