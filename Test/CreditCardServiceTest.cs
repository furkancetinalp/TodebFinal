using AutoMapper;
using BackgroundJobs.Abstract;
using Business.Concrete;
using Business.Configuration.Mapper;
using DAL.Abstract;
using DTO.CreditCard;
using FluentAssertions;
using Models.Document;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class CreditCardServiceTest
    {
        [Fact]
        public void CreditCardAdd_Success()
        {
            //Arrange
            var creditcardRepositoryMock = new Mock<ICreditCardRepository>();
            creditcardRepositoryMock.Setup(x => x.Add(It.IsAny<CreditCard>()));

            MapperConfiguration mapperConfig = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            });

            IMapper mapper = new Mapper(mapperConfig);

            var creditCardService = new CreditCardService(creditcardRepositoryMock.Object, mapper);

            var creditCardRequest = new CreateCreditCardRequest()
            {
                CustomerName = "Test customer",
                CardNumber = "19900",
                ExpireMonth = 12,
                ExpireYear = 2027

            };

            //Act
            var response = creditCardService.Add(creditCardRequest);
            response.Status.Should().BeTrue();


        }
    }
}
