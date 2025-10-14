using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot<T> : MonoBehaviour, IDropHandler
{
    [SerializeField]
    protected Inventory<T> inventory;
    public virtual void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            HoldObject(eventData.pointerDrag);
        }
    }

    protected virtual T HoldObject(GameObject gameObject)
    {
        SetParent(gameObject.GetComponent<DraggableItem>());
        return gameObject.GetComponent<T>();
    }

    protected virtual void SetParent(DraggableItem draggableItem) => draggableItem.ParentAfterDrag = transform;    

    public void SetInventory(Inventory<T> i) => inventory = i;
}
