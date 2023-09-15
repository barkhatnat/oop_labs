namespace lab1.Models;

public class Dealer
{
    private readonly Deck _dealerDeck;

    public Dealer()
    {
        _dealerDeck = new Deck();
    }
    
    public void Shuffle()
    {
        var dealerCards = _dealerDeck.GetDeck();
        var half = dealerCards.Count / 2;
        var newdeck = new List<Card>(new Card[dealerCards.Count]);
        for (int i = 0; i < half; i++)
        {
            newdeck[i * 2] = dealerCards[i + half];
            newdeck[i * 2 + 1] = dealerCards[i];
        }

        _dealerDeck.UpdateDeck(newdeck);
    }

    public void ToDealDeck()
    {
        Console.WriteLine(_dealerDeck);
    }
}