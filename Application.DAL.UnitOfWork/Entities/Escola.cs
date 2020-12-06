using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.DAL.UnitOfWork.Entities
{
    [Table("Escolas")]
    public class Escola : BaseEntity
    {
        public string Nome { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
        public virtual ICollection<Turma> Turmas { get; set; }

    }
}
