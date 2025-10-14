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

    public void Start()
    {
        slot.GetComponent<DeckSlot>().SetDeck(this);
        capacity = 52;
        GenerateFullDeck();
    }

    public void ShuffleDeck(int loop = 0)
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

    public Card Draw() => Cards.Pop();

    public void Add(Card card) =>
        _ = Cards.Contains(card) | Full() ? 0 :
        ((Func<int>)(() => { Cards.Add(card); return 0; }))();

    public void Stack(Card card) =>
        _ = Cards.Contains(card) ? 0 :
        ((Func<int>)(() => { Cards.Insert(0, card); return 0; }))();

    public void Bury(Card card) => Add(card);

    public void Remove(Card card) => Cards.Remove(card);

    public bool Full() => Size >= Capacity;

    protected void GenerateFullDeck()
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
}