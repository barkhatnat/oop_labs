using System.Collections.ObjectModel;
using OOP_ICT.Exceptions;

namespace OOP_ICT.Models;

public class Dealer
{
    private CardDeck DealerDeck { get;  set; } = new CardDeck();

    public void Shuffle()
    {
        var dealerCards = DealerDeck.Cards;
        var half = dealerCards.Count / 2;
        var newDeck = new List<Card>(new Card[dealerCards.Count]);
        for (var i = 0; i < half; i++)
        {
            newDeck[i * 2] = dealerCards[i + half];
            newDeck[i * 2 + 1] = dealerCards[i];
        }

        DealerDeck.Cards = newDeck.AsReadOnly();
    }

    public Card GetOneCard()
    {
        if (DealerDeck.Cards.Count < 1)
        {
            throw new CardNumberException("Not enough cards in the deck");
        }
        var card = DealerDeck.Cards.First();
        var newDeck = new List<Card>(DealerDeck.Cards);
        newDeck.RemoveAt(0);
        DealerDeck.Cards = newDeck.AsReadOnly();
        return card;
    }

    public List<Card> GetSeveralCards(int numberOfCards)
    {
        if (DealerDeck.Cards.Count < numberOfCards)
        {
            throw new CardNumberException("Not enough cards in the deck");
        }
        var cards = DealerDeck.Cards.ToList().GetRange(0,numberOfCards);
        var newDeck = new List<Card>(DealerDeck.Cards);
        newDeck.RemoveAll(t => cards.Contains(t));
        DealerDeck.Cards = newDeck.AsReadOnly();
        return cards;
    }
    public List<Card> GetAllCards()
    {
        var cards = DealerDeck.Cards.ToList().GetRange(0,DealerDeck.Cards.Count);
        var newDeck = new List<Card>(DealerDeck.Cards);
        newDeck.RemoveAll(t => cards.Contains(t));
        DealerDeck.Cards = newDeck.AsReadOnly();
        return cards;
    }
}