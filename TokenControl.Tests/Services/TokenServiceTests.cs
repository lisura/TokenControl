using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TokenControl.Models;
using TokenControl.Services;
using Moq;
using System.Linq;
using EntityFrameworkCoreMock;
using TokenControl.Tests.Shared;

namespace TokenControl.Tests.Services
{
    [TestFixture]
    class TokenServiceTests : MockHelpers
    {
        TokenService tokenService;

        [SetUp]
        public void Setup()
        {
            tokenService = new TokenService();
            DbContexMockGeneration();
        }

        [Test]
        public void WhenGenerateToken_ReturnValidToken()
        {
            //arrange  
            int[] lastCardDigits =  { 1, 2, 3, 4 };
            int cvv = 123;
            //act

            long token = tokenService.GenerateToken(lastCardDigits, cvv);

            //assert
            Assert.That(token, Is.EqualTo(2341));
        }

        [Test]
        public void WhenValidateToken_ReturnTrue2()
        {
            //arrange  

            //act
            var tokenValidation = tokenService.ValidateToken(dbContextMock.Object, tokenRequest);

            //assert
            Assert.That(tokenValidation.Result, Is.True);
        }

        
    }
}
//arrange  

//act

//assert