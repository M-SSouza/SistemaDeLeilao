using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeLeilao.Models
{
    public class Lances
    {
        public int LancesID { get; set; }
        public int PessoasID { get; set; }
        public int ProdutosID { get; set; }
        public decimal Valor { get; set; }

        public ICollection<Pessoas> Pessoas { get;}
        public ICollection<Produtos> Produtos { get;}
    }
}
