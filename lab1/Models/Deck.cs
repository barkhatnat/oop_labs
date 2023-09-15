using lab1.Enums;
using System.Text;

namespace lab1.Models;

public class Deck
{
    private List<Card> _cards = new List<Card>();

    public Deck()
    {
        _cards = CreateNewDeck();
    }
    public void UpdateDeck(List<Card> cards)
    {
        _cards = cards;
    }

    private List<Card> CreateNewDeck()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 14; j > 1; j--)
            {
                _cards.Add(new Card((AllDenominations)j, (AllSuits)i));
            }
        }

        return _cards;
    }

    public List<Card> GetDeck()
    {
        return _cards;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        foreach (Card card in _cards)
        {
            sb.Append($"CARD {card.Denomination}-{card.Suit} open? {card.IsOpen}\n");
        }

        return sb.ToString();
    }
}