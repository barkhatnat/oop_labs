using OOP_ICT.Models;
using OOP_ICT.Second.Models;

namespace OOP_ICT.Second.Interfaces;

public interface IPlayer
{
    Account BankAccount { get; }
    Account CasinoAccount { get; }
    string Name { get; }
    string Passport { get; }

    string Surname { get; }
    BlackjackHand PlayersHand { get; }
    bool CheckPassport(string passport);

    bool HasBankAccount();

    bool HasCasinoAccount();
    void AskForCards(Dealer dealer, int numberOfCards = 1);
}