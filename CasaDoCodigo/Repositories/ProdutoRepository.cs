using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        private readonly ICategoriaRepository categoriaRepository;

        public ProdutoRepository(ApplicationContext contexto,
            ICategoriaRepository categoriaRepository) : base(contexto)
        {
            this.categoriaRepository = categoriaRepository;
        }

        public async Task<IList<Produto>> GetProdutos()
        {
            return await dbSet.Include(p => p.Categoria).ToListAsync();
        }

        public async Task<IList<Produto>> GetProdutos(string pesquisa)
        {
            if (pesquisa == null || pesquisa.Equals(""))
            {
                return await dbSet.Include(p => p.Categoria).ToListAsync();
            }
            else
            {
                var listaDeProdutos = dbSet.Include(p => p.Categoria)
                    .Where(p => p.Nome.Contains(pesquisa) || 
                        p.Categoria.Nome.Contains(pesquisa))
                    .Distinct()
                    .ToListAsync();
                return await listaDeProdutos;
            }
        }

        public async Task SaveProdutos(List<Livro> livros)
        {
            foreach (var livro in livros)
            {
                if (!dbSet.Where(p => p.Codigo == livro.Codigo).Any())
                {
                    var categoria = await categoriaRepository.NovaCategoria(livro.Categoria);
                    dbSet.Add(new Produto(livro.Codigo, livro.Nome, categoria, livro.Preco));
                }

                await contexto.SaveChangesAsync();
            }
        }
    }

    public class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Subcategoria { get; set; }
        public decimal Preco { get; set; }
    }
}
