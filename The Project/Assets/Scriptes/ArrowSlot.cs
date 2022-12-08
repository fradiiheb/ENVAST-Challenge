using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ArrowSlot : MonoBehaviour, IDropHandler
{
    public int answer;
    public bool haveItem;
    public Image AnimalImage;
    public void OnDrop(PointerEventData eventData)
    {
        //check if the slot is already occupied by another item
        if (haveItem == true)
        {
            //make danedraging variable of the wanted to drop item to false and return 
            //that will make it call its OnEndDrag fuction that will reset it
            eventData.pointerDrag.GetComponent<ArrowDragDrop>().doneDraging = false;
            return;
        }
        //make the dragged item position take slot position and doneDraging to true 
        //that will make it call its OnEndDrag fuction which will fix the second end of line to the slot
        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        eventData.pointerDrag.GetComponent<ArrowDragDrop>().doneDraging = true;
        //make the variable of the dragged object take this arrowSlot and set haveItem to true
        eventData.pointerDrag.GetComponent<ArrowDragDrop>().arrowSlot = this;
        haveItem = true;
    }

    
}
