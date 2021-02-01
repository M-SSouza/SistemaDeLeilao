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
        [RegularExpression("[a-zA-Z ]+$", ErrorMessage = "Um nome deve conter apenas letras e espaços.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Nos diga sua idade, por favor.")]
        [Range(minimum: 18, maximum: 120, ErrorMessage = "Você precisa ter de 18 anos até 120 anos..")]
        [RegularExpression("^[0-9]*[1-9][0-9]*$", ErrorMessage = "Apenas números inteiro.")]
        public int? Idade { get; set; }
    }
}
