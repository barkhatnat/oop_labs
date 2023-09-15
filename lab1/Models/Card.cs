using lab1.Enums;

namespace lab1.Models;

public class Card
{
    public AllDenominations Denomination { get; set; }
    public AllSuits Suit { get; set; }
    public bool IsOpen { get; set; }

    public Card(AllDenominations denomination, AllSuits suit, bool isOpen = false)
    {
        Denomination = denomination;
        Suit = suit;
        IsOpen = isOpen;
    }

    public override string ToString()
    {
        return $"CARD {Denomination}-{Suit} open? {IsOpen}";
    }
}