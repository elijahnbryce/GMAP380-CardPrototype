using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.UI;

public class CardData
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
        Ace,
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
        Ace2,
        LittleJoker,
        BigJoker
    }

    public Suit suit;
    public Number number;
    public Sprite backSprite; // Load

    public CardData(Number n, Suit s)
    {
        this.number = n;
        this.suit = s;
    }
}

public class Card : DraggableItem
{
    public CardData data;
    public Image img;
    private Sprite sprite;
    public event Action OnDeckChanged;

    public new Transform ParentAfterDrag
    {
        get { return parentAfterDrag; }
        set
        {
            if (value != transform.root && value != parentAfterDrag)
            {
                var v = value.GetComponent<CardSlot>();
                if (v && v.Deck != parentAfterDrag.GetComponent<CardSlot>()?.Deck)
                {
                    //PrintEventListeners(OnDeckChanged);
                    OnDeckChanged?.Invoke();
                }
            }
            parentAfterDrag = value;
        }
    }

    public void Start()
    {
        img = GetComponent<Image>();
    }

    public void ShowBack()
    {
        img.sprite = data.backSprite;
    }

    public void ShowFront()
    {
        img.sprite = sprite;
    }
    
    public int GetValue()
    {
        int val = (int)data.number;
        // more logic later
        return val;
    }

    // action debugging method
    void PrintEventListeners(Action a)
    {
        var invocations = a.GetInvocationList();
        foreach (var m in invocations)
        {
            print(m.Method.Name);
        }
    }
}


// TODO: move to card data
public class CardImageDictionary : ScriptableObject
{
    public Image GetImage(Card card) => card.data switch
    {
        { suit: CardData.Suit.Heart } => null,
        { suit: CardData.Suit.Diamond } => null,
        { suit: CardData.Suit.Club } => null,
        { suit: CardData.Suit.Spade } => null,
        { number: CardData.Number.Jack } => null,
        { number: CardData.Number.Queen } => null,
        { number: CardData.Number.King } => null,
        { number: CardData.Number.Ace } => null,
        { number: CardData.Number.LittleJoker } => null,
        { number: CardData.Number.BigJoker } => null,
        _ => null
    };

    public Image GetCard(Card card) => card.data switch
    {
        { number: < CardData.Number.LittleJoker } => null, // spritesheet[(int)card.number * (int)Card.Number.LittleJoker + (int)card.number]
        _ => null // spritesheet[3 * 14 = (int)card.suit + (int)card.number]
    };


}

