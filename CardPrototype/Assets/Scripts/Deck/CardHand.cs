using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHand : Deck
{
    //private Transform slots;
    //public new int size, capacity;

    private new void Start()
    {
        foreach (Transform child in slot.transform)
        {
            var s = child.GetComponent<CardSlot>();
            s.SetDeck(this);
        }
    }
}