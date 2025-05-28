using System;

namespace InsideTest.Domain.Entities
{
    public class Produto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; }
        public decimal Preco { get; set; }
    }
}
