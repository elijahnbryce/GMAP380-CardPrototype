using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckMaster : MonoBehaviour
{
    public PlayingHand playingHand;
    public int health;
    [SerializeField] private int maxHealth;

    public void TakeDamage(int dmg)
    {
        health -= dmg;

    }

    public IEnumerator TakeTurn()
    {
        yield return null;
    }
}
