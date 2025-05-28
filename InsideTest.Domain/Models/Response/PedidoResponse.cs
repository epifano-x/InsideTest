using System;
using System.Collections.Generic;

namespace InsideTest.Domain.Responses
{
    public class PedidoResponse
    {
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Fechado { get; set; }
        public List<ProdutoResponse> Produtos { get; set; } = new();
    }

    public class ProdutoResponse
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
    }
}
