using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.DAL.UnitOfWork.Entities
{
    [Table("Turmas")]
    public class Turma : BaseEntity
    {
        public string Nome { get; set; }
        public int QtdAlunos { get; set; }
        public int EscolaId { get; set; }
        public virtual Escola Escola { get; set; }
    }
}
