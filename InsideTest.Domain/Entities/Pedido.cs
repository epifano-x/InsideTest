using System;
using System.Collections.Generic;

namespace InsideTest.Domain.Entities
{
    public class Pedido
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public bool Fechado { get; set; } = false;
        public List<Produto> Produtos { get; set; } = new();
    }
}
