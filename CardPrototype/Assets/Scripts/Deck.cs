using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Deck : MonoBehaviour
{
    public Card[] cards = new Card[52];
    public List<Card> c = new();
    public int size, capacity;

    public void ShuffleDeck(int loop = 0)
    {
        for (int l = 0; l < loop; l++)
        {
            for (int i = 0; i < size; i++)
            {
                int j = Random.Range(0, size);
                (cards[j], cards[i]) = (cards[i], cards[j]);
            }
        }
    }

    public Card Pull()
    {
        return c.Pop();
    }

    public void Add(Card card)
    {
        cards.Append(card);
    }
}

public class CardCollection : Deck
{
    public new int size, capacity;

    private Deck myHand;

    public void Collect(Card c)
    {
        myHand.Add(c);
    }
}

public class CardHand
{
    public List<InventorySlot> slots = new();
    public int size, capacity;

    public void DisplayCard(Card c, InventorySlot i)
    {
        var image = c.GetComponent<Image>().image;
        i.GetComponent<Image>().image = image;
    }

    public void AddToHand(Card c)
    {
        if (size >= capacity) return;
        size++;
        DisplayCard(c, slots[size]);
    }

    public Card RemoveFromHand(Card c)
    {
        size--;
        return c;
    }
}