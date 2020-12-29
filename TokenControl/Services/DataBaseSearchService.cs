using Microsoft.EntityFrameworkCore;
using System.Linq;
using TokenControl.Models;

namespace TokenControl.Services
{
    public interface IDataBaseSearchService
    {
        CardControl GetCardByIds(TokenControlContext _context, TokenRequest tokenRequest);
        CardControl GetCardDuplication(TokenControlContext _context, CardControl cardControl);
        void DetachedTrackerFromRequest(TokenControlContext _context, CardControl cardSaved);
        void UpdateCardStateToModified(TokenControlContext _context, CardControl cardControl);
    }
    public class DataBaseSearchService : IDataBaseSearchService
    {
        public CardControl GetCardByIds(TokenControlContext _context, TokenRequest tokenRequest)
        {
            var cardSaved = _context.CardControl.FirstOrDefault(e =>
                (e.Id == tokenRequest.CardId) &&
                (e.CustomerId == tokenRequest.CustomerId));
            return cardSaved;
        }

        public CardControl GetCardDuplication(TokenControlContext _context, CardControl cardControl)
        {
            var cardSaved = _context.CardControl.FirstOrDefault(e =>
                (e.Cardnumber == cardControl.Cardnumber) &&
                (e.CustomerId == cardControl.CustomerId) &&
                (e.CVV == cardControl.CVV));
            return cardSaved;
        }

        public void DetachedTrackerFromRequest(TokenControlContext _context, CardControl cardSaved)
        {
            var local = _context.Set<CardControl>().Local.FirstOrDefault(entry => entry.Id.Equals(cardSaved.Id));
            _context.Entry(local).State = EntityState.Detached;
        }

        public void UpdateCardStateToModified(TokenControlContext _context, CardControl cardControl)
        {
            _context.Entry(cardControl).State = EntityState.Modified;
        }
    }
}
