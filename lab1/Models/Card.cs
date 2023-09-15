using lab1.Enums;

namespace lab1.Models;

public class Card
{
    private AllDenominations Denomination { get; set; }
    private AllDenominations Suit { get; set; }
    private bool IsOpen { get; set; }
    
    public Card(AllDenominations denomination, AllDenominations suit, bool isOpen) 
    {
         

    }

}