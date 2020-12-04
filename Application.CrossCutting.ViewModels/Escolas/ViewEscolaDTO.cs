using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CrossCutting.ViewModels.Escolas
{
    public class ViewEscolaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
    }
}
