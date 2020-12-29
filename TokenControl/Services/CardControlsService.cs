using System;
using System.Threading.Tasks;
using TokenControl.Models;

namespace TokenControl.Services
{

    public interface ICardControlsService
    {
        Task<CardControlDTO> ProcessCustomerCard(TokenControlContext _contextCardControl, CardControl cardControl);
    }
    public class CardControlsService : ICardControlsService
    {
        readonly ITokenService _tokenService;
        readonly ICardManipulationService _operationsService;
        public IDataBaseSearchService _dataBaseSearchService;

        public CardControlsService()
        {
            if (_tokenService == null)
            {
                _tokenService = new TokenService();
            }
            if (_operationsService == null)
            {
                _operationsService = new CardManipulationService();
            }
            if (_dataBaseSearchService == null)
            {
                _dataBaseSearchService = new DataBaseSearchService();
            }
        }

        public async Task<CardControlDTO> ProcessCustomerCard(TokenControlContext _context, CardControl cardControl)
        {
            var cardSaved = _dataBaseSearchService.GetCardDuplication(_context, cardControl);
            cardControl.RegistrationDate = DateTime.UtcNow;
            if (cardSaved == null)
            {
                _context.CardControl.Add(cardControl);
            }
            else
            {
                _dataBaseSearchService.DetachedTrackerFromRequest(_context, cardSaved);
                cardControl.Id = cardSaved.Id;
                _dataBaseSearchService.UpdateCardStateToModified(_context, cardControl);
            }
            await _context.SaveChangesAsync();
            return ReturnCardInformation(cardControl);
        }

        private CardControlDTO ReturnCardInformation(CardControl cardControl)
        {
            var lastFourDigitsArray = _operationsService.GetLastFourDigitsFromCard(cardControl.Cardnumber);
            var returnCardinformation = new CardControlDTO
            {
                Id = cardControl.Id,
                RegistrationDate = cardControl.RegistrationDate,
                Token = _tokenService.GenerateToken(lastFourDigitsArray, cardControl.CVV)
            };
            return returnCardinformation;
        }
    }
}
