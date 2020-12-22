using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DAL.UnitOfWork.DTO
{
    public class SaveTurmaDTO
    {
        [Required]
        public string Nome { get; set; }
        [Range(1, int.MaxValue)]
        public int QtdAlunos { get; set; }
        [Range(1, int.MaxValue)]
        public int EscolaId { get; set; }
    }
}
