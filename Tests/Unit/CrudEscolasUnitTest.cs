using Application.CrossCutting.ViewModels.Escolas;
using Application.DAL.UnitOfWork;
using Application.DAL.UnitOfWork.Repositories;
using AutoMapper;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests.Unit
{
    [TestFixture]
    public class CrudEscolasUnitTest
    {
        [Test]
        public async Task Return_List_Of_Entities()
        {
            var ctx = new Mock<AppContext>();
            var entityList = new List<ViewEscolaDTO>
            {
                new ViewEscolaDTO
                {
                    Id = 1,
                    Nome = "Escola Teste 1",
                    RazaoSocial = "Teste 1",
                    Cnpj = "26086811000140",
                    Telefone = "21973521973",
                    Email = "marcos.ammon@gmail.com",
                    Site = "https://www.linkedin.com/in/marcos-vin%C3%ADcius-ammon-02287572/"
                },
                new ViewEscolaDTO
                {
                    Id = 2,
                    Nome = "Escola Teste 2",
                    RazaoSocial = "Teste 2",
                    Cnpj = "26086811000140",
                    Telefone = "21973521973",
                    Email = "marcos.ammon@gmail.com",
                    Site = "https://www.linkedin.com/in/marcos-vin%C3%ADcius-ammon-02287572/"
                },
                new ViewEscolaDTO
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
    }
}
