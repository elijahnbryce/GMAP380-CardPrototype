using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlot : InventorySlot<Card>, IPointerClickHandler, ISerializationCallbackReceiver
{
    protected Card slotted;
    public Card Slotted
    {
        get {return slotted;}
        set => slotted = value != null ? HoldObject(value.gameObject) : null;
    }

    public Deck Deck => (Deck)inventory;

    protected Deck serializedDeck;

    // This happens *before* Unity serializes the object (e.g. when saving)
    public void OnBeforeSerialize() => serializedDeck = (Deck)inventory;

    // This happens *after* Unity deserializes the object (e.g. on load/start)
    public void OnAfterDeserialize()
    {
        inventory = (Deck)serializedDeck;
        serializedDeck = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        return;
        if (transform.childCount == 0)
        {
            HoldObject(eventData.pointerClick);
        }
    }
    
    protected override Card HoldObject(GameObject card)
    {
        if (Deck.Full()) return null; 
        var c = card.GetComponent<Card>();
        SubscribeToCard(c);
        SetParent(c);
        return c;
    }

    protected override void SetParent(DraggableItem draggableItem) => ((Card)draggableItem).ParentAfterDrag = transform;

    public void SetDeck(Deck d) => SetInventory(d);

    public void AddToDeck(Card card)
    {
        Deck.Add(card);

        card.OnDeckChanged -= () => AddToDeck(card);
        card.OnDeckChanged -= () => RemoveFromDeck(card);
        card.OnDeckChanged += () => RemoveFromDeck(card);
    }

    public void RemoveFromDeck(Card card)
    {
        if (card == Slotted) 
            Slotted = null;
        Deck.Remove(card);
        card.OnDeckChanged -= () => RemoveFromDeck(card);
    }

    public void SubscribeToCard(Card c)
    {
        c.OnDeckChanged -= () => AddToDeck(c);
        c.OnDeckChanged += () => AddToDeck(c);
    }
}