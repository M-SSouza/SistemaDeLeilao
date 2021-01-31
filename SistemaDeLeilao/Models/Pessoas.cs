using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeLeilao.Models
{
    public class Pessoas
    {
        public int PessoasID { get; set; }

        [Required(ErrorMessage = "Ops, você esqueceu de um nome.")]
        [StringLength(maximumLength: 50, ErrorMessage = "Comprimento necessário.. min: 5/max: 50.", MinimumLength = 5)]
        public string Nome { get; set; }

        [Range(minimum: 18, maximum: 100, ErrorMessage = "Nosso p")]
        public int Idade { get; set; }
    }
}
