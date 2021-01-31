using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeLeilao.Models
{
    public class Lances
    {
        public int LancesID { get; set; }

        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Informe quem esta realizando esse lance.")]
        public int PessoasID { get; set; }

        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Informe qual o produto do lance.")]
        public int ProdutosID { get; set; }

        [Required(ErrorMessage = "Não esqueça do valor.")]
        public decimal Valor { get; set; }

        public ICollection<Pessoas> Pessoas { get;}
        public ICollection<Produtos> Produtos { get;}
    }
}
