using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCollection : Deck
{
    private Deck myCards;
    private CardHand myHand;

    public new void Start()
    {
        // Do nothing;
    }

    public void Collect(Card c)
    {
        myCards.Add(c);
    }
}