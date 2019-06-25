using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models.ViewModels
{
    public class BuscadorDeProdutoViewModel
    {
        public BuscadorDeProdutoViewModel(string pesquisa, IList<Produto> produtos)
        {
            Pesquisa = pesquisa;
            Produtos = produtos;
        }

        public string Pesquisa { get; set; }
        public IList<Produto> Produtos { get; }
    }
}
