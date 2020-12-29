using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenControl.Services;

namespace TokenControl.Tests.Services
{
    [TestFixture]
    class CardManipulationServiceTests
    {
        CardManipulationService cardManipulationService;

        [SetUp]
        public void Setup()
        {
            cardManipulationService = new CardManipulationService();
        }

        [Test]
        public void WhenGetLastFourDigitsFromCard_ReturnAnArrayOfIntegers()
        {
            //arrange  
            long cardNumber = 123456781234;
            int[] lastFourDigistAssert = { 1, 2, 3, 4 };
            //act

            var lastFourDigist = cardManipulationService.GetLastFourDigitsFromCard(cardNumber);

            //assert
            Assert.That(lastFourDigist, Is.EqualTo(lastFourDigistAssert));
        }
    }
}
