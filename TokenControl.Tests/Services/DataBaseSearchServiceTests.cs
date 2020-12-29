using NUnit.Framework;
using TokenControl.Models;
using TokenControl.Services;
using TokenControl.Tests.Shared;

namespace TokenControl.Tests.Services
{
    [TestFixture]
    class DataBaseSearchServiceTests : MockHelpers
    {
        DataBaseSearchService dataBaseSearchService;

        [SetUp]
        public void Setup()
        {
            dataBaseSearchService = new DataBaseSearchService();
            DbContexMockGeneration();
        }

        [Test]
        public void WhenGetCardByIds_ReturnCardControlObject()
        {
            //arrange  
          
            //act
            var token = dataBaseSearchService.GetCardByIds(dbContextMock.Object, tokenRequest);

            //assert
            Assert.That(token.Cardnumber, Is.EqualTo(123456781234));
        }

        [Test]
        public void WhenGetCardDuplication_ReturnCardControlObject()
        {
            //arrange  
            var cardControlEntry = new CardControl { CustomerId = 3, Cardnumber = 123456789014, CVV = 789 };

            //act
            var cardControlDatabase = dataBaseSearchService.GetCardDuplication(dbContextMock.Object, cardControlEntry);

            //assert
            Assert.That(cardControlDatabase.Id, Is.EqualTo(3));
        }
    }
}
