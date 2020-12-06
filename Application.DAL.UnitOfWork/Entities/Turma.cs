﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.DAL.UnitOfWork.Entities
{
    [Table("Turmas")]
    public class Turma
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int QtdAlunos { get; set; }
        public int EscolaId { get; set; }
    }
}