using OOP_ICT.Models;
using OOP_ICT.Second.Exceptions;
using OOP_ICT.Second.Interfaces;
using OOP_ICT.Third.Exceptions;

namespace OOP_ICT.Second.Models;

public class Player : IPlayer
{
    public Account BankAccount { get; set; } = null;
    public Account CasinoAccount { get; set; } = null;
    public string Name { get; private set; }
    public string Passport { get; }

    public string Surname { get; private set; }
    public BlackjackHand PlayersHand { get; private set; }

    public Player(string passport, string name, string surname)
    {
        if (passport.Length == 10 && (IsDigitsOnly(passport)))
        {
            Passport = passport;
        }
        else
        {
            throw new IncorrectPassportException("Passport number can't be negative or contain letters");
        }

        Name = name;
        Surname = surname;
        PlayersHand = new BlackjackHand();
    }

    public bool CheckPassport(string passport)
    {
        return Passport == passport;
    }

    private bool IsDigitsOnly(string str)
    {
        foreach (var c in str)
        {
            if (!Char.IsDigit(c))
                return false;
        }

        return true;
    }

    public bool HasBankAccount()
    {
        return BankAccount != null;
    }

    public bool HasCasinoAccount()
    {
        return CasinoAccount != null;
    }

    public void AskForCards(Dealer dealer, int numberOfCards = 1)
    {
        if (numberOfCards < 0)
        {
            throw new InvalidCardNumberException("Card number can't be 0 or negative");
        }

        var newDeck = dealer.GetSeveralCards(numberOfCards);
        newDeck.AddRange(PlayersHand.Cards);
        PlayersHand.Cards = newDeck.AsReadOnly();
    }
}