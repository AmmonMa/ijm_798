using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.CrossCutting.ViewModels.Escolas
{
    public class SaveEscolaDTO
    {
        [Required]
        public string Nome { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string Telefone { get; set; }
        [Required]
        [RegularExpression(".+@..+")]
        public string Email { get; set; }
        public string Site { get; set; }
    }
}
