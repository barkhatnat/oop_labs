using OOP_ICT.Enums;

namespace OOP_ICT.Models;

public class Card
{
    public Denomination Denomination { get; private set; }
    public Suit Suit { get; private set; }

    public Card(Denomination denomination, Suit suit)
    {
        Denomination = denomination;
        Suit = suit;
    }

    public override bool Equals(object obj)
    {
        return Denomination == (obj as Card).Denomination && Suit == (obj as Card).Suit;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)Denomination, (int)Suit);
    }
}