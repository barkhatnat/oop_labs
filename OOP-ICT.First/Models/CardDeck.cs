using System.Collections.ObjectModel;
using OOP_ICT.Enums;

namespace OOP_ICT.Models;

public class CardDeck
{
    public ReadOnlyCollection<Card> Cards { get; set; }

    public CardDeck()
    {
        Cards = CreateNewDeck();
    }

    private ReadOnlyCollection<Card> CreateNewDeck()
    {
        var cards = ((Denomination[])Enum.GetValues(typeof(Denomination)))
            .OrderByDescending(x => x)
            .SelectMany(denomination => ((Suit[])Enum.GetValues(typeof(Suit)))
                .Select(suit => new Card(denomination, suit)))
            .ToList();
        return cards.AsReadOnly();
    }
}