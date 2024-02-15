using System.Collections.ObjectModel;
using OOP_ICT.Models;
using OOP_ICT.Second.Models;

namespace OOP_ICT.Third.Models;

public class BlackjackDealer : Dealer
{
    public BlackjackHand DealersHand { get; private set; } = new BlackjackHand();
    private const int BlackjackCardNumber = 1;

    public void CreateBlackjackDeck()
    {
        var newDeck = GetSeveralCards(BlackjackCardNumber);
        DealersHand.Cards = newDeck.AsReadOnly();
    }

    public void DrawCard()
    {
        var newDeck = GetSeveralCards(BlackjackCardNumber);
        newDeck.AddRange(DealersHand.Cards);
        DealersHand.Cards = newDeck.AsReadOnly();
    }
}