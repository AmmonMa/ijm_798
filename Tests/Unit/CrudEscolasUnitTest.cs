using Application.DAL.UnitOfWork;
using Application.DAL.UnitOfWork.DTO;
using Application.DAL.UnitOfWork.Entities;
using Application.DAL.UnitOfWork.Repositories;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AppContext = Application.DAL.UnitOfWork.AppContext;

namespace Tests.Unit
{
    [TestFixture]
    public class CrudEscolasUnitTest
    {
        [Test]
        public async Task Should_Return_List_Of_Entities()
        {
            var ctx = new Mock<AppContext>();
            var entityList = new List<Escola>
            {
                new Escola
                {
                    Id = 1,
                    Nome = "Escola Teste 1",
                    RazaoSocial = "Teste 1",
                    Cnpj = "26086811000140",
                    Telefone = "21973521973",
                    Email = "marcos.ammon@gmail.com",
                    Site = "https://www.linkedin.com/in/marcos-vin%C3%ADcius-ammon-02287572/"
                },
                new Escola
                {
                    Id = 2,
                    Nome = "Escola Teste 2",
                    RazaoSocial = "Teste 2",
                    Cnpj = "26086811000140",
                    Telefone = "21973521973",
                    Email = "marcos.ammon@gmail.com",
                    Site = "https://www.linkedin.com/in/marcos-vin%C3%ADcius-ammon-02287572/"
                },
                new Escola
                {
                    Id = 3,
                    Nome = "Escola Teste 3",
                    RazaoSocial = "Teste 3",
                    Cnpj = "26086811000140",
                    Telefone = "21973521973",
                    Email = "marcos.ammon@gmail.com",
                    Site = "https://www.linkedin.com/in/marcos-vin%C3%ADcius-ammon-02287572/"
                }
            };
            var repository = new Mock<EscolaRepository>(ctx.Object);
            repository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(entityList);


            var unitOfWork = new Mock<UnitOfWork>(ctx.Object);
            unitOfWork.Setup(uow => uow.Escolas).Returns(repository.Object);


            var list = await unitOfWork.Object.Escolas.ListAllAsync();

            Assert.AreEqual(entityList.Count, list.Count);
        }

        [Test]
        public async Task Should_Add_New_Entity_With_Valid_Data()
        {
            var ctx = new Mock<AppContext>();
            var entity = new Escola();

            var repository = new Mock<EscolaRepository>(ctx.Object);
            repository.Setup(repo => repo
                .CreateAsync(It.IsAny<SaveEscolaDTO>()))
                .ReturnsAsync(entity)
                .Callback<SaveEscolaDTO>(c => 
                {
                    entity = new Escola
                    {
                        Id = 1,
                        Nome = c.Nome,
                        RazaoSocial = c.RazaoSocial,
                        Cnpj = c.Cnpj,
                        Telefone = c.Telefone,
                        Email = c.Email,
                        Site = c.Site
                    };
                });

            var unitOfWork = new Mock<UnitOfWork>(ctx.Object);
            unitOfWork.Setup(uow => uow.Escolas).Returns(repository.Object);

            var entityId = await unitOfWork.Object.Escolas.CreateAsync(new SaveEscolaDTO
            {
                Nome = "Escola Teste 1",
                RazaoSocial = "Teste 1",
                Cnpj = "26086811000140",
                Telefone = "21973521973",
                Email = "marcos.ammon@gmail.com",
                Site = "https://www.linkedin.com/in/marcos-vin%C3%ADcius-ammon-02287572/"
            });

            repository.Verify(r => r.CreateAsync(It.IsAny<SaveEscolaDTO>()), Times.Once);

            Assert.AreEqual( "Escola Teste 1", entity.Nome );
        }

        [Test]
        public async Task Should_Modify_Entity_With_Valid_Data()
        {
            var ctx = new Mock<AppContext>();
            var entity = new Escola
            {
                Id = 1,
                Nome = "Escola Teste 1",
                RazaoSocial = "Teste 1",
                Cnpj = "26086811000140",
                Telefone = "21973521973",
                Email = "marcos.ammon@gmail.com",
                Site = "https://www.linkedin.com/in/marcos-vin%C3%ADcius-ammon-02287572/"
            };

            var repository = new Mock<EscolaRepository>(ctx.Object);
            repository.Setup(repo => repo
                .UpdateAsync(1, It.IsAny<SaveEscolaDTO>()))
                .Callback<int, SaveEscolaDTO>( (b, c)  =>
                {
                    entity = new Escola
                    {
                        Id = b,
                        Nome = c.Nome,
                        RazaoSocial = c.RazaoSocial,
                        Cnpj = c.Cnpj,
                        Telefone = c.Telefone,
                        Email = c.Email,
                        Site = c.Site
                    };
                });

            var unitOfWork = new Mock<UnitOfWork>(ctx.Object);
            unitOfWork.Setup(uow => uow.Escolas).Returns(repository.Object);


            await unitOfWork.Object.Escolas.UpdateAsync(1, new SaveEscolaDTO
            {
                Nome = "Escola Teste Alterado",
                RazaoSocial = "Teste Alterado",
                Cnpj = "26086811000140",
                Telefone = "21973521973",
                Email = "marcos.ammon@gmail.com",
                Site = "https://www.linkedin.com/in/marcos-vin%C3%ADcius-ammon-02287572/"
            });

            Assert.AreEqual("Escola Teste Alterado", entity.Nome);
        }


        [Test]
        public async Task Should_Retrieve_Entity_With_Id()
        {
            var ctx = new Mock<AppContext>();
            var entity = new Escola
            {
                Id = 1,
                Nome = "Escola Teste 1",
                RazaoSocial = "Teste 1",
                Cnpj = "26086811000140",
                Telefone = "21973521973",
                Email = "marcos.ammon@gmail.com",
                Site = "https://www.linkedin.com/in/marcos-vin%C3%ADcius-ammon-02287572/"
            };

            var repository = new Mock<EscolaRepository>(ctx.Object);
            repository.Setup(r => r.FindByIdAsync(entity.Id)).ReturnsAsync(entity);

            var unitOfWork = new Mock<UnitOfWork>(ctx.Object);
            unitOfWork.Setup(d => d.Escolas).Returns(repository.Object);

            var dtoRetrieved = await unitOfWork.Object.Escolas.FindByIdAsync(entity.Id);


            Assert.AreEqual(dtoRetrieved.Id, entity.Id);
        }

        [Test]
        public async Task Should_Remove_Entity_With_Id()
        {
            var ctx = new Mock<AppContext>();
            var entity = new Escola
            {
                Id = 1,
                Nome = "Escola Teste 1",
                RazaoSocial = "Teste 1",
                Cnpj = "26086811000140",
                Telefone = "21973521973",
                Email = "marcos.ammon@gmail.com",
                Site = "https://www.linkedin.com/in/marcos-vin%C3%ADcius-ammon-02287572/"
            };

            var repository = new Mock<EscolaRepository>(ctx.Object);
            repository.Setup(r => r.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(entity);

            var unitOfWork = new Mock<UnitOfWork>(ctx.Object);
            unitOfWork.Setup(uow => uow.Escolas).Returns(repository.Object);

            await unitOfWork.Object.Escolas.DeleteAsync(entity.Id);


            repository.Verify(r => r.DeleteAsync(1), Times.Once);
        }


        [Test]
        public void Should_Return_Error_On_Validation()
        {
            var validationModel = new SaveEscolaDTO();
            var validationResult = new List<ValidationResult>();
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(validationModel, null, null);
            Validator.TryValidateObject(validationModel, validationContext, validationResult, true);

            Assert.IsTrue(validationResult.Any(v => v.MemberNames.Contains("Email") && v.ErrorMessage.Contains("required")));
            Assert.IsTrue(validationResult.Any(v => v.MemberNames.Contains("Nome") && v.ErrorMessage.Contains("required")));
        }
    }
}
