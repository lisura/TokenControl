using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenControl.Models;
using Moq;
using TokenControl.Services;

namespace TokenControl.Tests.Shared
{
    class MockHelpers
    {
        public DbContextMock<TokenControlContext> dbContextMock;

        public TokenRequest tokenRequest;

        
        public void DbContexMockGeneration()
        {
            var data = new[]
                        {
                new CardControl { Id = 1, CustomerId = 1, Cardnumber = 123456789012, CVV = 456, RegistrationDate = DateTime.UtcNow },
                new CardControl { Id = 2, CustomerId = 2, Cardnumber = 123456781234, CVV = 123, RegistrationDate = DateTime.UtcNow },
                new CardControl { Id = 3, CustomerId = 3, Cardnumber = 123456789014, CVV = 789, RegistrationDate = DateTime.UtcNow },
            };

            var dummyOptions = new DbContextOptionsBuilder<TokenControlContext>().Options;

            dbContextMock = new DbContextMock<TokenControlContext>(dummyOptions);
            var usersDbSetMock = dbContextMock.CreateDbSetMock(x => x.CardControl, data);
          
            tokenRequest = new TokenRequest()
            {
                CardId = 2,
                CustomerId = 2,
                CVV = 123,
                Token = 2341
            };
        }

        public Mock<IDataBaseSearchService> MockIDataBaseSearchService()
        {
            var cardControl = new CardControl() {  CustomerId = 3, Cardnumber = 123456789014, CVV = 789 };
            var iDataBaseSearchServiceMock = new Mock<IDataBaseSearchService>();
            iDataBaseSearchServiceMock.Setup(_ => _.DetachedTrackerFromRequest(It.IsAny<TokenControlContext>(), It.IsAny<CardControl>()));
            iDataBaseSearchServiceMock.Setup(_ => _.UpdateCardStateToModified(It.IsAny<TokenControlContext>(), It.IsAny<CardControl>()));
            iDataBaseSearchServiceMock.Setup(_ => _.GetCardDuplication(It.IsAny<TokenControlContext>(), It.IsAny<CardControl>())).Returns(cardControl);
            return iDataBaseSearchServiceMock;
        }
    }
}
