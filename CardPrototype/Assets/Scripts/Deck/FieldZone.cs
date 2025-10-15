using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldZone : MonoBehaviour
{
    public BattleLane[] lanes = new BattleLane[3];

    public Action onFieldChanged;

    public void SetAction(Action action) => onFieldChanged = action;

    public void SubscribeToLanes()
    {
        foreach (var lane in lanes)
            lane.onCardAdded += onFieldChanged;
    }

    public void UnSubscribeToLanes()
    {
        foreach (var lane in lanes)
            lane.onCardAdded -= onFieldChanged;
    }

}