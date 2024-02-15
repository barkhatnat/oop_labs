using System.Collections.ObjectModel;
using OOP_ICT.Second.Exceptions;
using OOP_ICT.Second.Models;
using OOP_ICT.Third.Enums;
using OOP_ICT.Third.Exceptions;

namespace OOP_ICT.Third.Models;

public class BlackjackGame
{
    private BlackjackDealer BlackjackDealer { get; set; }
    public List<Player> Players { get; private set; }
    private Dictionary<Player, decimal> betCatalog { get; set; }

    private Dictionary<Player, Result> resultCatalog { get; set; }
    private Dictionary<Player, decimal> paymentCatalog { get; set; }
    private Dictionary<Player, Result> dealerResult { get; set; }
    public IReadOnlyDictionary<Player, decimal> BetCatalog => new ReadOnlyDictionary<Player, decimal>(betCatalog);

    public IReadOnlyDictionary<Player, Result> ResultCatalog => new ReadOnlyDictionary<Player, Result>(resultCatalog);

    public IReadOnlyDictionary<Player, decimal> PaymentCatalog => new ReadOnlyDictionary<Player, decimal>(paymentCatalog);

    public IReadOnlyDictionary<Player, Result> DealerResult => new ReadOnlyDictionary<Player, Result>(dealerResult);

    public int PlayersTurn = 0;
    private int _dealerScore = 0;
    private const int StartPlayerCount = 2;
    private const int Blackjack = 21;
    private const int StopDealerCount = 17;
    private const int BlackjackCardNumber = 1;
    public BlackjackStatus GameStatus;

    private BlackjackCasino _blackjackCasino;


    public BlackjackGame(BlackjackDealer dealer, BlackjackCasino blackjackCasino)
    {
        _blackjackCasino = blackjackCasino;
        BlackjackDealer = dealer;
        Players = new List<Player>();
        betCatalog = new Dictionary<Player, decimal>();
        resultCatalog = new Dictionary<Player, Result>();
        paymentCatalog = new Dictionary<Player, decimal>();
        dealerResult = new Dictionary<Player, Result>();
        GameStatus = BlackjackStatus.Betting;
    }

    public void AddPlayer(Player player, decimal bet)
    {
        if (GameStatus != BlackjackStatus.Betting)
        {
            throw new GameStatusException("Cannot place a bet at this game state");
        }

        if (player.CasinoAccount.Balance >= bet && bet > 0)
        {
            Players.Add(player);
            betCatalog.Add(player, bet);
            _blackjackCasino.CheckBlackjackAndEndGame(player, false, bet);
        }
        else
        {
            throw new NoMoneyException("Not enough money in the player's casino account to place a bet");
        }
    }

    public void DealCards()
    {
        if (GameStatus != BlackjackStatus.Betting)
        {
            throw new GameStatusException("Cannot deal cards at this game state");
        }

        BlackjackDealer.Shuffle();
        foreach (var player in Players)
        {
            player.AskForCards(BlackjackDealer, StartPlayerCount);
        }

        BlackjackDealer.CreateBlackjackDeck();
        foreach (var player in Players)
        {
            HasBlackJack(player);
        }

        GameStatus = BlackjackStatus.PlayersTurn;
    }


    public void PlayerMove(Player player, Boolean takeCard)
    {
        if (GameStatus != BlackjackStatus.PlayersTurn)
        {
            throw new GameStatusException("Now is not player's turn");
        }

        if (PlayersTurn != Players.IndexOf(player))
        {
            throw new GameStatusException("Now is turn of player " + Players[PlayersTurn].Name +
                                          ", not turn of player " + player.Name);
        }

        if (ResultCatalog.ContainsKey(Players[PlayersTurn]))
        {
            PlayersTurn += 1;
        }

        if (player.PlayersHand.BlackjackCounter() <= Blackjack && takeCard)
        {
            player.AskForCards(BlackjackDealer, BlackjackCardNumber);
            if (player.PlayersHand.BlackjackCounter() > Blackjack)
            {
                resultCatalog[player] = Result.Loss;
                dealerResult[player] = Result.Win;
            }
        }
        else
        {
            resultCatalog[player] = Result.Loss;
        }

        PlayersTurn += 1;
        if (PlayersTurn == Players.Count)
        {
            PlayersTurn = 0;
            GameStatus = BlackjackStatus.DealerTurn;
            DealerMove();
        }
    }


    private void DealerMove()
    {
        if (GameStatus != BlackjackStatus.DealerTurn)
        {
            throw new GameStatusException("Now is not dealer's turn");
        }

        if (_dealerScore <= StopDealerCount)
        {
            BlackjackDealer.DrawCard();
            GameStatus = BlackjackStatus.PlayersTurn;
        }
        else
        {
            var remainingPlayers = Players.Where(player => !ResultCatalog.ContainsKey(player));

            if (_dealerScore == Blackjack)
            {
                foreach (var player in remainingPlayers)
                {
                    if (player.PlayersHand.BlackjackCounter() == Blackjack)
                    {
                        resultCatalog[player] = Result.Draw;
                        dealerResult[player] = Result.Draw;
                    }
                    else
                    {
                        resultCatalog[player] = Result.Loss;
                        dealerResult[player] = Result.Win;
                    }
                }

                GameStatus = BlackjackStatus.WinningPayment;
            }
            else if (_dealerScore > Blackjack)
            {
                foreach (var player in remainingPlayers)
                {
                    resultCatalog[player] = Result.Win;
                    dealerResult[player] = Result.Loss;
                }

                GameStatus = BlackjackStatus.WinningPayment;
            }
            else
            {
                FindWinner();
            }
        }
    }

    private void FindWinner()
    {
        var remainingPlayers = Players.Where(player => !ResultCatalog.ContainsKey(player));
        foreach (var player in remainingPlayers)
        {
            if (player.PlayersHand.BlackjackCounter() == _dealerScore)
            {
                resultCatalog[player] = Result.Draw;
                dealerResult[player] = Result.Draw;
            }
            else if (player.PlayersHand.BlackjackCounter() < _dealerScore)
            {
                resultCatalog[player] = Result.Loss;
                dealerResult[player] = Result.Win;
            }
            else
            {
                resultCatalog[player] = Result.Win;
                dealerResult[player] = Result.Loss;
            }
        }

        if (resultCatalog.Keys.Count == Players.Count)
        {
            GameStatus = BlackjackStatus.WinningPayment;
        }
        else
        {
            GameStatus = BlackjackStatus.PlayersTurn;
        }
    }

    private void HasBlackJack(Player player)
    {
        if (player.PlayersHand.BlackjackCounter() == Blackjack)
        {
            resultCatalog[player] = Result.Win;
        }
    }

    public void BetPayment()
    {
        if (GameStatus != BlackjackStatus.WinningPayment)
        {
            throw new GameStatusException("Cannot give payment at this game state");
        }

        foreach (var player in Players)
        {
            switch (ResultCatalog[player])
            {
                case Result.Draw:
                    paymentCatalog[player] = BetCatalog[player];
                    break;
                case Result.Loss:
                    paymentCatalog[player] = 0;
                    break;
                case Result.Win:
                    paymentCatalog[player] = BetCatalog[player] * 2;
                    break;
            }

            _blackjackCasino.CheckBlackjackAndEndGame(player, true, PaymentCatalog[player]);
        }
    }
}