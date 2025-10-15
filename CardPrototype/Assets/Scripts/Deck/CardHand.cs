using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHand : Deck
{
    private new void Start()
    {
        foreach (Transform child in slot.transform)
        {
            RegisterSlot(child.GetComponent<CardSlot>());
        }
    }
}