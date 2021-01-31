using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace SistemaDeLeilao.Models
{
    public class Produtos
    {
        public int ProdutosID { get; set; }

        [Required(ErrorMessage = "Ops, você esqueceu de um nome.")]
        [StringLength(maximumLength: 50, ErrorMessage = "Comprimento necessário.. min: 5/max: 50.", MinimumLength = 5)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Não esqueça do valor.")]
        [Range(minimum:2.00, maximum: 50000.00, ErrorMessage = "Defina um valor de R$:2,00 até R$: 50.000,00. ")]
        public decimal Valor { get; set; }
    }
}
