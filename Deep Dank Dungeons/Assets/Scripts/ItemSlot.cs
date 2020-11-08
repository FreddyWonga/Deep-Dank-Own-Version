using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public static bool RunePlaced;

    private void Start()
    {
        RunePlaced = false;
    }

    //If the rune is placed into the runestone slot then set the RunePlaced Bool to true
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            RunePlaced = true;
        }
    }
}
