using System.Collections.Generic;

namespace InsideTest.Domain.Responses
{
    /// <summary>
    /// Representa o resultado de uma consulta paginada.
    /// </summary>
    /// <typeparam name="T">Tipo dos itens retornados na página.</typeparam>
    public class PagedResult<T>
    {
        /// <summary>
        /// Número da página atual (começando em 1).
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Número de itens por página.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Total de itens disponíveis na base de dados.
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Total de páginas baseado no tamanho da página e número total de itens.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Lista de itens da página atual.
        /// </summary>
        public IEnumerable<T> Items { get; set; }
    }
}
