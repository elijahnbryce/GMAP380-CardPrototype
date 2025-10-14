using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckSlot : CardSlot
{
    public Transform oneSlot;
    public override void OnDrop(PointerEventData eventData)
    {
        HoldObject(eventData.pointerDrag);
    }
}