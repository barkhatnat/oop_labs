using OOP_ICT.Models;
using OOP_ICT.Second.Interfaces;
using OOP_ICT.Second.Models;
using OOP_ICT.Third.Exceptions;

namespace OOP_ICT.Fourth.Classes;

public class DealingPlayer : Dealer, IPlayer
{
    public Account BankAccount { get; private set; } = null;
    public Account CasinoAccount { get; private set; } = null;
    public string Name { get; private set; }
    public string Passport { get; private set; }

    public string Surname { get; private set; }
    public BlackjackHand PlayersHand { get; private set; }

    public Player Player { get; private set; }

    public DealingPlayer(Player player)
    {
        Player = player;
        Name = player.Name;
        BankAccount = player.BankAccount;
        CasinoAccount = player.CasinoAccount;
        Surname = player.Surname;
        Passport = player.Passport;
    }

    public bool CheckPassport(string passport)
    {
        return Player.CheckPassport(Player.Passport);
    }

    public bool HasBankAccount()
    {
        return Player.HasBankAccount();
    }

    public bool HasCasinoAccount()
    {
        return Player.HasCasinoAccount();
    }

    public void AskForCards(Dealer dealer, int numberOfCards = 1)
    {
        if (numberOfCards < 0)
        {
            throw new InvalidCardNumberException("Card number can't be 0 or negative");
        }

        var newDeck = GetSeveralCards(numberOfCards);
        newDeck.AddRange(Player.PlayersHand.Cards);
        Player.PlayersHand.Cards = newDeck.AsReadOnly();
    }

    public void ChangeDealingPlayer(Player player)
    {
        Player = player;
        Name = player.Name;
        BankAccount = player.BankAccount;
        CasinoAccount = player.CasinoAccount;
        Surname = player.Surname;
        Passport = player.Passport;
    }
}