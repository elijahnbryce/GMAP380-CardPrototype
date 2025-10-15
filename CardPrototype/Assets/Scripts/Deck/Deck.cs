using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Search;
using UnityEngine;

public abstract class Inventory<T> : MonoBehaviour
{
    [SerializeField]
    protected List<T> items = new();

    protected int Size => items.Count();

    [SerializeField]
    protected int capacity;
    public int Capacity => capacity;
}

[Serializable]
public class Deck : Inventory<Card>, ISerializationCallbackReceiver
{
    [SerializeField]
    private GameObject cardPrefab;

    public List<Card> Cards => items;
    [SerializeField] protected Transform slot;

    protected List<Card> serializedItems = new();

    // This happens *before* Unity serializes the object (e.g. when saving)
    public void OnBeforeSerialize() => serializedItems = new List<Card>(items);

    // This happens *after* Unity deserializes the object (e.g. on load/start)
    public void OnAfterDeserialize()
    {
        items = new List<Card>(serializedItems);
        serializedItems = null;
    }

    public virtual void Start()
    {
        RegisterSlot(slot.GetComponent<DeckSlot>());
        capacity = 52;
        GenerateFullDeck();
    }

    public virtual void ShuffleDeck(int loop = 0)
    {
        for (int l = 0; l < loop; l++)
        {
            for (int i = 0; i < Size; i++)
            {
                int j = UnityEngine.Random.Range(0, Size);
                (Cards[j], Cards[i]) = (Cards[i], Cards[j]);
            }
        }
    }

    public virtual Card Draw() => Cards.Pop();

    public virtual void Add(Card card)
    {
        if (Cards.Contains(card) | Full()) return;
        
        Cards.Add(card);
        card.transform.SetAsLastSibling();
    }
    
    public virtual void Stack(Card card) 
    {
        if (Cards.Contains(card)) return;

        Cards.Insert(0, card);
        card.transform.SetAsFirstSibling();
    }

    public virtual void Bury(Card card) => Add(card);

    public virtual void Remove(Card card) => Cards.Remove(card);

    public virtual bool Full() => Size >= Capacity;

    protected virtual void GenerateFullDeck()
    {
        foreach (CardData.Suit suit in System.Enum.GetValues(typeof(CardData.Suit)))
        {
            for (int i = 0; i < (int)CardData.Number.Ace2; i++)
            {

                Card card = Instantiate(cardPrefab, slot).GetComponent<Card>();
                card.data = new CardData((CardData.Number)i, suit);
                slot.GetComponent<DeckSlot>().AddToDeck(card);
                //card.ShowBack();
                //Add(card);
            }
        }
    }

    protected virtual void RegisterSlot(CardSlot slot)
    {
        slot.SetDeck(this);
    }
}