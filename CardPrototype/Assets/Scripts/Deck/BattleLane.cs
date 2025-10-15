using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class BattleLane : Deck
{
    [SerializeField]
    private List<Transform> slots = new();

    private new void Start()
    {
        capacity = 2;
        foreach (Transform slot in slots)
        {
            RegisterSlot(slot.GetComponent<CardSlot>());
        }
    }

    public int GetScore() => Cards.Sum(card => card.GetValue());

    public override void Add(Card card)
    {
        if (Full() | Cards.Contains(card)) return;

        Transform t = card.transform.parent;
        Cards.Insert(slots.IndexOf(t), card);
        card.dragEnabled = false;
    }

    public override void Stack(Card card)
    {
        // Don't need this function
        return;
    }

    public override void Remove(Card card)
    {
        base.Remove(card);
        card.dragEnabled = true;
    }
    
    public void Swap(Transform a, Transform b)
    {
        int x = slots.IndexOf(a);
        (CardSlot y, CardSlot z) = (
            slots[x].GetComponent<CardSlot>(),
            ((BattleLane)b.GetComponent<CardSlot>().Deck).slots[x].GetComponent<CardSlot>()
        );
        Swap(y, z);
    }

    public void Swap(CardSlot a, CardSlot b) => (a.Slotted, b.Slotted) = (b.Slotted, a.Slotted);
}