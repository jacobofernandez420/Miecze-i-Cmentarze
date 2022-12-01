using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestSlot : MonoBehaviour
{
    public Image icon;

    public Image highlightImage;

    public ChestUI chestUI;

    Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
    }
    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }
    public void ShowDetails()
    {
        chestUI.RemoveHighlights();
        chestUI.chestInventoryItemDetailsUI.HideDetails();
        chestUI.chestItemDetailsUI.ShowDetails(item);
        highlightImage.enabled = true;
    }
}
