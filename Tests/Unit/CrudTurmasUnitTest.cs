using Application.CrossCutting.ViewModels.Escolas;
using Application.DAL.UnitOfWork;
using Application.DAL.UnitOfWork.Entities;
using Application.DAL.UnitOfWork.Repositories;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppContext = Application.DAL.UnitOfWork.AppContext;

namespace Tests.Unit
{
    [TestFixture]
    public class CrudTurmasUnitTest
    {
        [Test]
        public async Task Should_Return_List_Of_Entities()
        {
            var ctx = new Mock<AppContext>();
            var entityList = new List<Turma>
            {
                new Turma
                {
                    Id = 1,
                    Nome = "Turma Teste 1",
                    QtdAlunos = 10,
                    EscolaId = 1
                },
                new Turma
                {
                    Id = 2,
                    Nome = "Turma Teste 2",
                    QtdAlunos = 20,
                    EscolaId = 1
                },
                new Turma
                {
                    Id = 3,
                    Nome = "Turma Teste 2",
                    QtdAlunos = 20,
                    EscolaId = 1
                }
            };
            var repository = new Mock<TurmaRepository>(ctx.Object);
            repository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(entityList);


            var unitOfWork = new Mock<UnitOfWork>(ctx.Object);
            unitOfWork.Setup(uow => uow.Turmas).Returns(repository.Object);


            var list = await unitOfWork.Object.Turmas.ListAllAsync();

            Assert.AreEqual(entityList.Count, list.Count);
        }

        [Test]
        public void Should_Add_New_Entity_With_Valid_Data()
        {
            var ctx = new Mock<AppContext>();
            var entity = new Turma();

            var repository = new Mock<TurmaRepository>(ctx.Object);
            repository.Setup(repo => repo
                .Add(It.IsAny<SaveTurmaDTO>()))
                .Returns(entity)
                .Callback<SaveTurmaDTO>(c =>
                {
                    entity = new Turma
                    {
                        Id = 1,
                        Nome = "Turma Teste 1",
                        QtdAlunos = 10,
                        EscolaId = 1
                    };
                });

            var unitOfWork = new Mock<UnitOfWork>(ctx.Object);
            unitOfWork.Setup(uow => uow.Turmas).Returns(repository.Object);

            var entityId = unitOfWork.Object.Turmas.Add(new SaveTurmaDTO
            {
                Nome = "Turma Teste 1",
                QtdAlunos = 10,
                EscolaId = 1
            });

            repository.Verify(r => r.Add(It.IsAny<SaveTurmaDTO>()), Times.Once);

            Assert.AreEqual("Turma Teste 1", entity.Nome);
            Assert.AreEqual(entityId, entity.Id);
        }

        [Test]
        public async Task Should_Modify_Entity_With_Valid_Data()
        {
            var ctx = new Mock<AppContext>();
            var entity = new Turma
            {
                Id = 1,
                Nome = "Turma Teste 1",
                QtdAlunos = 10,
                EscolaId = 1
            };

            var repository = new Mock<TurmaRepository>(ctx.Object);
            repository.Setup(repo => repo
                .UpdateAsync(1, It.IsAny<SaveTurmaDTO>()))
                .Callback<int, SaveTurmaDTO>((b, c) =>
                {
                    entity = new Turma
                    {
                        Id = 1,
                        Nome = c.Nome,
                        QtdAlunos = c.QtdAlunos,
                        EscolaId = c.EscolaId
                    };
                });

            var unitOfWork = new Mock<UnitOfWork>(ctx.Object);
            unitOfWork.Setup(uow => uow.Turmas).Returns(repository.Object);


            await unitOfWork.Object.Turmas.UpdateAsync(1, new SaveTurmaDTO
            {
                Nome = "Turma Teste Alterado",
                QtdAlunos = 10,
                EscolaId = 1
            });

            Assert.AreEqual("Turma Teste Alterado", entity.Nome);
        }


        [Test]
        public async Task Should_Retrieve_Entity_With_Id()
        {
            var ctx = new Mock<AppContext>();
            var entity = new Turma
            {
                Nome = "Turma Teste 1",
                QtdAlunos = 10,
                EscolaId = 1
            };

            var repository = new Mock<TurmaRepository>(ctx.Object);
            repository.Setup(r => r.FindByIdAsync(entity.Id)).ReturnsAsync(entity);

            var unitOfWork = new Mock<UnitOfWork>(ctx.Object);
            unitOfWork.Setup(d => d.Turmas).Returns(repository.Object);

            var dtoRetrieved = await unitOfWork.Object.Turmas.FindByIdAsync(entity.Id);


            Assert.AreEqual(dtoRetrieved.Id, entity.Id);
        }

        [Test]
        public async Task Should_Remove_Entity_With_Id()
        {
            var ctx = new Mock<AppContext>();
            var entity = new Turma
            {
                Nome = "Turma Teste 1",
                QtdAlunos = 10,
                EscolaId = 1
            };

            var repository = new Mock<TurmaRepository>(ctx.Object);
            repository.Setup(r => r.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(entity);
            // repository.Setup(r => r.DeleteAsync(entity.Id)).ReturnsAsync(true);

            var unitOfWork = new Mock<UnitOfWork>(ctx.Object);
            unitOfWork.Setup(uow => uow.Turmas).Returns(repository.Object);

            // var result = await unitOfWork.Object.Turmas.DeleteAsync(entity.Id);

           //  Assert.AreEqual(true, result);

        }

        [Test]
        public void Should_Return_Error_On_Validation()
        {
            var validationModel = new SaveTurmaDTO();
            var validationResult = new List<ValidationResult>();
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(validationModel, null, null);
            Validator.TryValidateObject(validationModel, validationContext, validationResult, true);

            Assert.IsTrue(validationResult.Any(v => v.MemberNames.Contains("Nome") && v.ErrorMessage.Contains("required")));
            Assert.IsTrue(validationResult.Any(v => v.MemberNames.Contains("QtdAlunos") && v.ErrorMessage.Contains("between")));
            Assert.IsTrue(validationResult.Any(v => v.MemberNames.Contains("EscolaId") && v.ErrorMessage.Contains("between")));
        }
    }
}
