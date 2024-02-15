using OOP_ICT.Enums;
using OOP_ICT.Fourth.Enums;
using OOP_ICT.Models;

namespace OOP_ICT.Fourth.Classes;

public class CombinationCounter
{
    private readonly List<Card> _cards;
    private bool _flush = false;
    private bool _flushRoyal = false;
    private bool _straight = false;
    private readonly Dictionary<Denomination, int> _counter = new Dictionary<Denomination, int>();
    private readonly List<int> _duplicates = new List<int>() { 0, 0, 0, 0, 0 };


    public CombinationCounter(List<Card> cards)
    {
        _cards = cards;
    }

    private void FindDetails()
    {
        List<Suit> suits = new List<Suit>();
        List<Denomination> denominations = new List<Denomination>();
        foreach (var card in _cards)
        {
            suits.Add(card.Suit);
            denominations.Add(card.Denomination);
        }

        suits.Sort();
        denominations.Sort();
        _flush = suits[0] == suits[suits.Count - 1];
        bool isStraight = true;
        for (var i = 0; i < denominations.Count - 1; i++)
        {
            if ((int)denominations[i + 1] - (int)denominations[i] == 1) continue;
            isStraight = false;
            break;
        }

        _straight = isStraight;
        if (_flush && _straight)
        {
            _flushRoyal = denominations.All(x =>
                x is Denomination.Ace or Denomination.King or Denomination.Queen or Denomination.Jack
                    or Denomination.Ten);
        }

        foreach (var denomination in denominations)
        {
            if (_counter.ContainsKey(denomination))
            {
                _counter[denomination] += 1;
            }
            else
            {
                _counter.Add(denomination, 1);
            }
        }

        List<int> duplications = new List<int>() { 0, 0, 0, 0, 0 };
        foreach (var denomination in _counter.Keys)
        {
            duplications[_counter[denomination]] += 1;
        }
    }

    public Combination FindRank()
    {
        FindDetails();
        Combination rank = 0;
        if (_flushRoyal)
        {
            rank = Combination.RoyalFlush;
        }
        else if (_flush && _straight)
        {
            rank = Combination.StraightFlush;
        }
        else if (_duplicates[4] == 1)
        {
            rank = Combination.FourOfAKind;
        }
        else if (_duplicates[3] == 1 && _duplicates[2] == 1)
        {
            rank = Combination.FullHouse;
        }
        else if (_flush)
        {
            rank = Combination.FLush;
        }
        else if (_straight)
        {
            rank = Combination.Straight;
        }
        else if (_duplicates[3] == 1)
        {
            rank = Combination.ThreeOfAKind;
        }
        else if (_duplicates[2] == 2)
        {
            rank = Combination.TwoPairs;
        }
        else if (_duplicates[2] == 1)
        {
            rank = Combination.OnePair;
        }
        else
        {
            rank = Combination.HighHand;
        }

        return rank;
    }
}