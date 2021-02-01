using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SistemaDeLeilao.Models
{
    public class Lances
    {
        public int LancesID { get; set; }

        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Informe quem esta realizando esse lance.")]
        [DisplayName("Pessoas")]
        public int PessoasID { get; set; }

        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Informe qual o produto do lance.")]
        [DisplayName("Produtos")]
        public int ProdutosID { get; set; }

        [Required(ErrorMessage = "Não esqueça do valor.")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? Valor { get; set; }

        public Pessoas Pessoas { get; set; }
        public Produtos Produtos { get; set; }
    }
}
