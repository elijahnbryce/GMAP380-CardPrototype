using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class DeckMaster : MonoBehaviour
{
    public FieldZone myField;
    public CardHand currentHand;

    public bool busy = false;
    public int health;
    [SerializeField] private int maxHealth;

    public bool Dead => health <= 0;

    void Start()
    {
        myField.SetAction(() => EndTurn());
    }

    public int EvalLane(int i) => myField.lanes[i].GetScore();

    public void TakeDamage(int dmg)
    {
        health -= dmg;
    }

    public IEnumerator TakeTurn()
    {
        StartTurn();
        yield return new WaitUntil(() => !busy);
    }

    public void StartTurn()
    {
        busy = true;
        myField.SubscribeToLanes();
        currentHand.ShowHand();
        return;
    }
    
    public void EndTurn()
    {
        busy = false;
        myField.UnSubscribeToLanes();
        currentHand.HideHand();
        return;
    }

}
