using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHand : Deck
{
    private new void Start()
    {
        foreach (Transform child in slot)
        {
            RegisterSlot(child.GetComponent<CardSlot>());
        }
    }

    public void ShowHand()
    {
        slot.gameObject.SetActive(true);
        foreach (Transform child in slot)
        {
            CardSlot cs = child.GetComponent<CardSlot>();
            if (cs.Slotted != null) cs.Slotted.dragEnabled = true;
        }
    }
    
    public void HideHand()
    {
        foreach (Transform child in slot)
        {
            CardSlot cs = child.GetComponent<CardSlot>();
            if (cs.Slotted != null) cs.Slotted.dragEnabled = false;
        }
        slot.gameObject.SetActive(false);
    }
}