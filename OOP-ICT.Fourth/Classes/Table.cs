using System.Collections.ObjectModel;
using OOP_ICT.Models;
using OOP_ICT.Second.Models;
using OOP_ICT.Third.Exceptions;

namespace OOP_ICT.Fourth.Classes;

public class Table
{
    public ReadOnlyCollection<Card> TableCards { get; private set; } = new List<Card>().AsReadOnly();
    public void AddCardsOnTable(Dealer dealer, int numberOfCards = 1)
    {
        if (numberOfCards < 0)
        {
            throw new InvalidCardNumberException("Card number can't be 0 or negative");
        }

        var newDeck = dealer.GetSeveralCards(numberOfCards);
        newDeck.AddRange(TableCards);
        TableCards = newDeck.AsReadOnly();
    }
}