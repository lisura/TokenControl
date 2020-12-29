using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenControl.Models;
using TokenControl.Services;
using TokenControl.Tests.Shared;

namespace TokenControl.Tests.Services
{
    [TestFixture]
    class CardControlsServiceTests : MockHelpers
    {
        CardControlsService cardControlsService;
        Mock<IDataBaseSearchService> dataBaseSearchService;

        [SetUp]
        public void Setup()
        {
            dataBaseSearchService = MockIDataBaseSearchService();
            cardControlsService = new CardControlsService();
            DbContexMockGeneration();
        }

        [Test]
        public void WhenProcessCustomerCard_New_ReturnCardControlDTO()
        {
            //arrange  
            CardControl cardControlNew = new CardControl { CustomerId = 3, Cardnumber = 103456789015, CVV = 789 };

            //act
            var cardDTO = cardControlsService.ProcessCustomerCard(dbContextMock.Object, cardControlNew);

            //assert
            Assert.That(cardDTO.Result.Token, Is.EqualTo(5901));
        }

        [Test]
        public void WhenProcessCustomerCard_Edit_ReturnCardControlDTO()
        {
            //arrange  
            CardControl cardControlNew = new CardControl { CustomerId = 1, Cardnumber = 123456789012, CVV = 456 };
            cardControlsService._dataBaseSearchService = dataBaseSearchService.Object;

            //act
            var cardDTO = cardControlsService.ProcessCustomerCard(dbContextMock.Object, cardControlNew);

            //assert
            Assert.That(cardDTO.Result.Token, Is.EqualTo(9012));
        }
    }
}
