using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector]
    public Transform ParentAfterDrag
    {
        get { return parentAfterDrag; }
        set
        {
            Debug.Log("Draggable Item parent change");
            if (value != transform.root && value != parentAfterDrag)
                OnParentChanged?.Invoke();
            parentAfterDrag = value;
        }
    }
    protected Transform parentAfterDrag;

    public Image image;
    public event Action OnParentChanged;

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }
}
