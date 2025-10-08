using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggable = dropped.GetComponent<DraggableItem>();
            draggable.parentAfterDrag = transform;
        }
    }
}

public class CardSlot : InventorySlot, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject card = eventData.pointerClick;
            DraggableItem draggableItem = card.GetComponent<DraggableItem>();
            draggableItem.parentAfterDrag = transform;
        }
    }
}
