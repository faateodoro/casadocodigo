using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public async Task<Categoria> NovaCategoria(string novaCategoria)
        {
            var categoriaDB = new Categoria();

            if (dbSet.Where(c => c.Nome == novaCategoria).Any())
            {
                categoriaDB = dbSet.Where(c => c.Nome == novaCategoria).FirstOrDefault();
            }
            else
            {
                categoriaDB = new Categoria(novaCategoria);
                await contexto.SaveChangesAsync();
            }
            return categoriaDB;
        }
    }

    public interface ICategoriaRepository
    {
        Task<Categoria> NovaCategoria(string novaCategoria);
    }
}
