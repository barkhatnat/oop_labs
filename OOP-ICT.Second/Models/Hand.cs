using System.Collections.ObjectModel;
using OOP_ICT.Models;

namespace OOP_ICT.Second.Models;

public class Hand
{
        public ReadOnlyCollection<Card> Cards { get; set; } = new List<Card>().AsReadOnly();

        public void AddSeveralCards(List<Card> cards)
        {
                var newHand = new List<Card>();
                newHand.AddRange(cards);
                Cards = newHand.AsReadOnly();
        }

        public void AddCard(Card card)
        {
                var newHand = new List<Card>();
                newHand.Add(card);
                Cards = newHand.AsReadOnly();
        }
        
        
}