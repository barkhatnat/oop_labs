
using lab1.Enums;
using lab1.Models;

Card card = new Card(AllDenominations.Ace, AllSuits.Clubs, true);
Deck deck = new Deck();
Dealer D = new Dealer();
for (int i = 0; i < 10; i++)
{
    D.Shuffle();
    D.ToDealDeck();
}


