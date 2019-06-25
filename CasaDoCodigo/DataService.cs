using CasaDoCodigo.Models;
using CasaDoCodigo.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CasaDoCodigo
{
    class DataService : IDataService
    {
        private readonly ApplicationContext contexto;
        private readonly IProdutoRepository produtoRepository;
        private readonly ICategoriaRepository categoriaRepository;

        public DataService(ApplicationContext contexto,
            IProdutoRepository produtoRepository,
            ICategoriaRepository categoriaRepository)
        {
            this.contexto = contexto;
            this.produtoRepository = produtoRepository;
            this.categoriaRepository = categoriaRepository;
        }

        public async Task InicializaDB()
        {
            await contexto.Database.MigrateAsync();

            List<Livro> livros = await GetLivros();

            await produtoRepository.SaveProdutos(livros);

        }

        private static async Task<List<Livro>> GetLivros()
        {
            var json = await File.ReadAllTextAsync("livros.json");
            
            var livros = JsonConvert.DeserializeObject<List<Livro>>(json);

            return livros;
        }
    }


}
