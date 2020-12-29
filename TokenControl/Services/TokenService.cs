using System;
using System.Threading.Tasks;
using TokenControl.Models;

namespace TokenControl.Services
{
    interface ITokenService
    {
        Task<bool> ValidateToken(TokenControlContext _context, TokenRequest tokenRequest);
        long GenerateToken(int[] lastCardDigits, int cvv);
    }


    public class TokenService : ITokenService
    {
        const int LAST_CARD_DIGITS_QUANTITY = 4;

        readonly IDataBaseSearchService _dataBaseSearchService;
        readonly ICardManipulationService _operationsService;
        public TokenService()
        {
            if (_dataBaseSearchService == null)
            {
                _dataBaseSearchService = new DataBaseSearchService();
            }
            if (_operationsService == null)
            {
                _operationsService = new CardManipulationService();
            }
        }

        public Task<bool> ValidateToken(TokenControlContext _context, TokenRequest tokenRequest)
        {
            var cardSavedInDataBase = _dataBaseSearchService.GetCardByIds(_context, tokenRequest);
            if (cardSavedInDataBase == null || !ValidateThirtyMinutesRule(cardSavedInDataBase))
            {
                return Task.FromResult(false);
            }
            var lastFourDigitsArray = _operationsService.GetLastFourDigitsFromCard(cardSavedInDataBase.Cardnumber);
            bool result = tokenRequest.Token == GenerateToken(lastFourDigitsArray, tokenRequest.CVV);
            return Task.FromResult(result);
        }

        private bool ValidateThirtyMinutesRule(CardControl cardSavedInDataBase)
        {
            var timeNowInUTC = DateTime.UtcNow;
            var diffTimes = timeNowInUTC - cardSavedInDataBase.RegistrationDate;
            return diffTimes.TotalMinutes <= 30;
        }

        public long GenerateToken(int[] lastCardDigits, int cvv)
        {
            int factor = cvv % LAST_CARD_DIGITS_QUANTITY;
            int[] exitArry = new int[LAST_CARD_DIGITS_QUANTITY];
            for (int i = 0; i < LAST_CARD_DIGITS_QUANTITY; i++)
            {
                int destiny = factor + i;
                if (destiny > LAST_CARD_DIGITS_QUANTITY - 1)
                {
                    destiny -= LAST_CARD_DIGITS_QUANTITY;
                }
                exitArry[destiny] = lastCardDigits[i];
            }
            return long.Parse(string.Join("", exitArry));
        }
    }
}
