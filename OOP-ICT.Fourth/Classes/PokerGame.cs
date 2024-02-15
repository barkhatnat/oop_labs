using System.Collections.ObjectModel;
using OOP_ICT.Fourth.Enums;
using OOP_ICT.Fourth.Exceptions;
using OOP_ICT.Second.Exceptions;
using OOP_ICT.Second.Interfaces;
using OOP_ICT.Second.Models;
using OOP_ICT.Third.Exceptions;

namespace OOP_ICT.Fourth.Classes;

public class PokerGame
{
    public DealingPlayer? CurrentDealer { get; private set; }
    private List<IPlayer> AllPlayers { get; set; }
    public IPlayer Winner { get; private set; }
    private Dictionary<IPlayer, decimal> betCatalog { get; set; }
    private Dictionary<IPlayer, decimal> scoreCatalog { get; set; }
    public Table Table { get; private set; }
    public IReadOnlyList<IPlayer> Players => new ReadOnlyCollection<IPlayer>(AllPlayers);
    public IReadOnlyDictionary<IPlayer, decimal> BetCatalog => new ReadOnlyDictionary<IPlayer, decimal>(betCatalog);
    public IReadOnlyDictionary<IPlayer, decimal> ScoreCatalog => new ReadOnlyDictionary<IPlayer, decimal>(scoreCatalog);
    public int PlayersTurn = 1;
    public decimal Bank;
    private const int StartPlayerCount = 2;
    private const int FlopCardNumber = 3;
    private const int MinNumberOfPlayers = 2;
    private const int MaxNumberOfPlayers = 10;
    public PokerStatus GameStatus { get; private set; }
    private int NumberOfPlayers { get; set; }
    private bool _checkIsAvailable = true;
    private const decimal MinBet = 100;
    private readonly PokerCasino _pokerCasino;


    public PokerGame(PokerCasino pokerCasino)
    {
        CurrentDealer = null;
        _pokerCasino = pokerCasino;
        AllPlayers = new List<IPlayer>();
        betCatalog = new Dictionary<IPlayer, decimal>();
        scoreCatalog = new Dictionary<IPlayer, decimal>();
        GameStatus = PokerStatus.JoiningPlayers;
        Table = new Table();
    }

    public void AddPlayer(Player player, bool isDealer)
    {
        if (GameStatus != PokerStatus.JoiningPlayers)
        {
            throw new GameStatusException("Cannot add a player at this game state");
        }


        if (!player.HasCasinoAccount())
        {
            throw new NoMoneyException("Player has no casino account");
            
        }

        if (isDealer)
        {
            if (CurrentDealer != null)
            {
                throw new DealingPlayerException(CurrentDealer.Name + " is dealer of this game");
            }

            CurrentDealer = new DealingPlayer(player);
            AllPlayers.Add(player);
        }
        else
        {
            AllPlayers.Add(player);
        }

        betCatalog.Add(player, 0);
    }

    public void StartGame()
    {
        if (CurrentDealer == null)
        {
            throw new DealingPlayerException("Game can't be start without dealer");
        }

        if (AllPlayers.Count is >= MinNumberOfPlayers and <= MaxNumberOfPlayers)
        {
            GameStatus = PokerStatus.BlindsBetting;
            NumberOfPlayers = AllPlayers.Count;
        }
        else
        {
            throw new PlayerNumberException("Number of player's can't be less than 2 and more than 10");
        }
    }

    public void MakeFirstBlindBet(Player player, decimal bet)
    {
        if (GameStatus != PokerStatus.BlindsBetting)
        {
            throw new GameStatusException("Cannot make a blind bet at this game state");
        }

        if (AllPlayers.IndexOf(player) - AllPlayers.IndexOf(CurrentDealer.Player) == 1)
        {
            if (bet != MinBet / 2)
            {
                throw new BetException("Small blind's first bet must equals the half of the minimal bet amount");
            }

            _pokerCasino.Loss(player, bet);
            betCatalog[player] += bet;
            ChangePlayer();
        }
        else if (AllPlayers.IndexOf(player) - AllPlayers.IndexOf(CurrentDealer.Player) == 2)
        {
            if (bet != MinBet)
            {
                throw new BetException("Big blind's first bet must equals the minimal bet amount");
            }

            _pokerCasino.Loss(player, bet);
            betCatalog[player] += bet;
            ChangePlayer();
        }
        else
        {
            throw new BetException("This player is not small or big blind");
        }

        if (CheckBlindsBets())
        {
            GameStatus = PokerStatus.Dealing;
        }
    }

    private bool CheckBlindsBets()
    {
        return betCatalog[AllPlayers[1]] != 0 && betCatalog[AllPlayers[2]] != 0;
    }

    public void Flop()
    {
        if (GameStatus != PokerStatus.Dealing)
        {
            throw new GameStatusException("Cannot deal cards at this game state");
        }

        CurrentDealer.Shuffle();
        foreach (var player in AllPlayers)
        {
            player.AskForCards(CurrentDealer, StartPlayerCount);
        }

        GameStatus = PokerStatus.BettingRound;
        _checkIsAvailable = true;
        Table.AddCardsOnTable(CurrentDealer, FlopCardNumber);
    }

    public void Raise(IPlayer player, decimal bet)
    {
        if (PlayersTurn != AllPlayers.IndexOf(player))
        {
            throw new GameStatusException("Now is turn of player " + AllPlayers[PlayersTurn].Name +
                                          ", not turn of player " + player.Name);
        }

        if (GameStatus != PokerStatus.BettingRound)
        {
            throw new GameStatusException("Cannot make a bet at this game state");
        }

        if (bet < MinBet)
        {
            throw new BetException("Bet can't be less than the minimal bet amount");
        }

        if (AllPlayers.IndexOf(player) - AllPlayers.IndexOf(CurrentDealer.Player) != 1 && bet < betCatalog[player] +
            betCatalog[
                AllPlayers[((AllPlayers.IndexOf(player) - 1) % NumberOfPlayers + NumberOfPlayers) % NumberOfPlayers]])
        {
            throw new BetException("Total bet can't be less than the bet of previous player");
        }

        _pokerCasino.Loss(player, bet);
        betCatalog[player] += bet;
        ChangePlayer();
        CheckBets();
    }

    public void Call(IPlayer player)
    {
        if (GameStatus != PokerStatus.BettingRound)
        {
            throw new GameStatusException("Cannot make a bet at this game state");
        }

        if (PlayersTurn != AllPlayers.IndexOf(player))
        {
            throw new GameStatusException("Now is turn of player " + AllPlayers[PlayersTurn].Name +
                                          ", not turn of player " + player.Name);
        }

        if (PlayersTurn == AllPlayers.IndexOf(CurrentDealer.Player) + 1)
        {
            throw new BetException("Small blind can't call, he should raise");
        }

        decimal bet =
            betCatalog[
                AllPlayers[((AllPlayers.IndexOf(player) - 1) % NumberOfPlayers + NumberOfPlayers) % NumberOfPlayers]] -
            betCatalog[player];
        betCatalog[player] += bet;
        _pokerCasino.Loss(player, bet);
        ChangePlayer();
        CheckBets();
    }

    private void ChangePlayer()
    {
        PlayersTurn = (PlayersTurn + 1) % NumberOfPlayers;
    }

    public void Check(IPlayer player)
    {
        if (GameStatus != PokerStatus.BettingRound)
        {
            throw new GameStatusException("Cannot make a bet at this game state");
        }

        if (PlayersTurn != AllPlayers.IndexOf(player))
        {
            throw new GameStatusException("Now is turn of player " + AllPlayers[PlayersTurn].Name +
                                          ", not turn of player " + player.Name);
        }

        if (!_checkIsAvailable)
        {
            throw new BetException("Check is not available");
        }

        ChangePlayer();
        _checkIsAvailable = false;
        CheckBets();
    }

    public void Fold(Player player)
    {
        NumberOfPlayers--;
        if (CurrentDealer.Player == player)
        {
            CurrentDealer.ChangeDealingPlayer((Player)AllPlayers[(AllPlayers.IndexOf(player) + 1) % NumberOfPlayers]);
        }

        AllPlayers.Remove(player);
        Bank += betCatalog[player];
        betCatalog.Remove(player);
        CheckBets();
        if (NumberOfPlayers < MinNumberOfPlayers)
        {
            Winner = AllPlayers[0];
            GameStatus = PokerStatus.WinningPayment;
        }
    }

    private void CheckBets()
    {
        bool isAllEqual = betCatalog.Values.Distinct().Count() == 1;
        if (isAllEqual && PlayersTurn == 1)
        {
            GameStatus = Table.TableCards.Count switch
            {
                3 => PokerStatus.Turn,
                4 => PokerStatus.River,
                5 => PokerStatus.Showdown,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    public void Turn()
    {
        if (GameStatus != PokerStatus.Turn)
        {
            throw new GameStatusException("Cannot deal turn at this game state");
        }

        GameStatus = PokerStatus.BettingRound;
        _checkIsAvailable = true;
        Table.AddCardsOnTable(CurrentDealer);
    }

    public void River()
    {
        if (GameStatus != PokerStatus.River)
        {
            throw new GameStatusException("Cannot deal river at this game state");
        }

        GameStatus = PokerStatus.BettingRound;
        _checkIsAvailable = true;
        Table.AddCardsOnTable(CurrentDealer);
    }

    public void Showdown()
    {
        if (GameStatus != PokerStatus.Showdown)
        {
            throw new GameStatusException("Cannot showdown at this game state");
        }

        foreach (var player in AllPlayers)
        {
            var combination = Table.TableCards.ToList();
            combination.AddRange(player.PlayersHand.Cards);
            CombinationCounter combinationCounter = new CombinationCounter(combination);
            scoreCatalog.Add(player, (int)combinationCounter.FindRank());
        }

        FindWinner();
        GameStatus = PokerStatus.WinningPayment;
    }

    private void FindWinner()
    {
        Winner = scoreCatalog.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;
    }

    public void Payment()
    {
        if (GameStatus != PokerStatus.WinningPayment)
        {
            throw new GameStatusException("Cannot showdown at this game state");
        }

        Bank += betCatalog.Values.Sum();
        _pokerCasino.Win(Winner, Bank);
    }
}