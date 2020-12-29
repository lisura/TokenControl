using System;

namespace TokenControl.Services
{
    interface ICardManipulationService
    {
        int[] GetLastFourDigitsFromCard(long? cardNumber);
    }

    public class CardManipulationService : ICardManipulationService
    {
        public int[] GetLastFourDigitsFromCard(long? cardNumber)
        {
            var cardLastFourDigits = (cardNumber % 10000).ToString().ToCharArray();
            int[] lastForDigitsArray = Array.ConvertAll(cardLastFourDigits, _ => (int)Char.GetNumericValue(_));
            return lastForDigitsArray;
        }
    }
}
