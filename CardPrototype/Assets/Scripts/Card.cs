using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Card : DraggableItem
{
    public enum Suit
    {
        Heart,
        Diamond,
        Club,
        Spade
    }

    public enum Number
    {
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace,
        LittleJoker,
        BigJoker
    }

    public Suit suit;
    public Number number;
}

public class CardImageDictionary : ScriptableObject
{
    public Image GetImage(Card card) => card switch
    {
        { suit: Card.Suit.Heart } => null,
        { suit: Card.Suit.Diamond } => null,
        { suit: Card.Suit.Club } => null,
        { suit: Card.Suit.Spade } => null,
        { number: Card.Number.Jack } => null,
        { number: Card.Number.Queen } => null,
        { number: Card.Number.King } => null,
        { number: Card.Number.Ace } => null,
        { number: Card.Number.LittleJoker } => null,
        { number: Card.Number.BigJoker } => null,
        _ => null
    };

    public Image GetCard(Card card) => card switch
    {
        { number: < Card.Number.LittleJoker } => null, // spritesheet[(int)card.number * (int)Card.Number.LittleJoker + (int)card.number]
        _ => null // spritesheet[3 * 14 = (int)card.suit + (int)card.number]
    };
}
