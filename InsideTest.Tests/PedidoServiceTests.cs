using InsideTest.Application;
using InsideTest.Domain.Entities;
using InsideTest.Domain.Interfaces;
using InsideTest.Domain.Requests;
using InsideTest.Domain.Responses;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace InsideTest.Tests
{
    public class PedidoServiceTests
    {
        private readonly Mock<IPedidoRepository> _repositoryMock;
        private readonly PedidoService _service;

        public PedidoServiceTests()
        {
            _repositoryMock = new Mock<IPedidoRepository>();
            _service = new PedidoService(_repositoryMock.Object);
        }

        [Fact]
        public async Task IniciarPedidoAsync_DeveCriarPedidoComSucesso()
        {
            var novoPedido = new Pedido();
            _repositoryMock.Setup(r => r.CriarPedidoAsync()).ReturnsAsync(novoPedido);

            var resultado = await _service.IniciarPedidoAsync();

            Assert.NotNull(resultado);
            Assert.Equal(novoPedido.Id, resultado.Id);
        }

        [Fact]
        public async Task FecharPedidoAsync_DeveFalhar_SePedidoVazio()
        {
            var pedido = new Pedido();
            _repositoryMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(pedido);

            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.FecharPedidoAsync(pedido.Id));
        }

        [Fact]
        public async Task AdicionarProdutoAsync_DeveAdicionarProduto_QuandoPedidoAberto()
        {
            var pedido = new Pedido();
            _repositoryMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(pedido);

            var request = new ProdutoRequest { Nome = "Produto Teste", Preco = 9.99m };

            var resultado = await _service.AdicionarProdutoAsync(Guid.NewGuid(), request);

            Assert.Single(resultado.Produtos);
            Assert.Equal("Produto Teste", resultado.Produtos[0].Nome);
        }

        [Fact]
        public async Task RemoverProdutoAsync_DeveRemoverProduto_QuandoPedidoAberto()
        {
            var pedido = new Pedido
            {
                Produtos = new List<Produto> { new Produto { Nome = "Produto A", Preco = 5.0m } }
            };
            _repositoryMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(pedido);

            var request = new ProdutoRequest { Nome = "Produto A", Preco = 5.0m };

            var resultado = await _service.RemoverProdutoAsync(Guid.NewGuid(), request);

            Assert.Empty(resultado.Produtos);
        }

        [Fact]
        public async Task ObterPedidoAsync_DeveRetornarPedidoCorreto()
        {
            var pedido = new Pedido { Id = Guid.NewGuid(), Fechado = false };
            _repositoryMock.Setup(r => r.ObterPorIdAsync(pedido.Id)).ReturnsAsync(pedido);

            var resultado = await _service.ObterPedidoAsync(pedido.Id);

            Assert.Equal(pedido.Id, resultado.Id);
            Assert.False(resultado.Fechado);
        }

        [Fact]
        public async Task FecharPedidoAsync_DeveFecharPedidoComSucesso_SeContiverProdutos()
        {
            var pedido = new Pedido
            {
                Produtos = new List<Produto> { new Produto { Nome = "Produto X", Preco = 20.0m } }
            };
            _repositoryMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(pedido);

            var resultado = await _service.FecharPedidoAsync(pedido.Id);

            Assert.True(resultado.Fechado);
        }
    }
}