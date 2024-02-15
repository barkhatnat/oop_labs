using OOP_ICT.Enums;

namespace OOP_ICT.Second.Models;

public class BlackjackHand:Hand
{
    private const int Blackjack = 21;
    private const int AceDifference = 10;
    
    public int BlackjackCounter()
    {
        var blackjackScores = new Dictionary<Denomination, int>()
        {
            { Denomination.Ace, 1 },
            { Denomination.Two, 2 },
            { Denomination.Three, 3 },
            { Denomination.Four, 4 },
            { Denomination.Five, 5 },
            { Denomination.Six, 6 },
            { Denomination.Seven, 7 },
            { Denomination.Eight, 8 },
            { Denomination.Nine, 9 },
            { Denomination.Ten, 10 },
            { Denomination.Jack, 10 },
            { Denomination.Queen, 10 },
            { Denomination.King, 10 },
        };
        var sum = Cards.Sum(card => blackjackScores[card.Denomination]);
        if (Cards.Any(card => card.Denomination == Denomination.Ace))
        {
            if (sum < Blackjack)
            {
                sum += AceDifference;
            }
        }

        return sum;
    }
}