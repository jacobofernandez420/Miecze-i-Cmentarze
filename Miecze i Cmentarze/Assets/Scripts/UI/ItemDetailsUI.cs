using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDetailsUI : MonoBehaviour
{
    public GameObject itemDetailsUI;
    public InventoryUI inventoryUI;

    public Text itemName;
    public TextMeshProUGUI itemDescription;
    public Image itemIcon;

    Item item;

    public void ShowDetails(Item newItem)
    {
        item = newItem;

        if (item != null)
        {
            itemIcon.sprite = item.icon;
            itemName.text = item.name;
            GenerateDescription();
            itemDetailsUI.SetActive(true);
        }
        else
        {
            HideDetails();
        }
    }

    public void HideDetails()
    {
        itemDetailsUI.SetActive(false);
    }

    public void UseItem()
    {
        if (item.type == "weapon")
        {
            if (inventoryUI.weapon.item != null)
            {
                inventoryUI.weapon.OnUnEquip();
            }

            inventoryUI.weapon.OnEquip(item);
            Inventory.instance.Remove(item);
            inventoryUI.UpdateInventory();
            inventoryUI.RemoveHighlights();
            HideDetails();
        }
        if (item.type == "armor")
        {
            if (inventoryUI.armor.item != null)
            {
                inventoryUI.armor.OnUnEquip();
            }

            inventoryUI.armor.OnEquip(item);
            Inventory.instance.Remove(item);
            inventoryUI.UpdateInventory();
            inventoryUI.RemoveHighlights();
            HideDetails();
        }
        if (item.type == "helmet")
        {
            if (inventoryUI.helmet.item != null)
            {
                inventoryUI.helmet.OnUnEquip();
            }

            inventoryUI.helmet.OnEquip(item);
            Inventory.instance.Remove(item);
            inventoryUI.UpdateInventory();
            inventoryUI.RemoveHighlights();
            HideDetails();
        }
        if (item.type == "ring")
        {
            if (inventoryUI.ring.item != null)
            {
                inventoryUI.ring.OnUnEquip();
            }

            inventoryUI.ring.OnEquip(item);
            Inventory.instance.Remove(item);
            inventoryUI.UpdateInventory();
            inventoryUI.RemoveHighlights();
            HideDetails();
        }
        if (item.type == "gloves")
        {
            if (inventoryUI.gloves.item != null)
            {
                inventoryUI.boots.OnUnEquip();
            }

            inventoryUI.gloves.OnEquip(item);
            Inventory.instance.Remove(item);
            inventoryUI.UpdateInventory();
            inventoryUI.RemoveHighlights();
            HideDetails();
        }
        if (item.type == "boots")
        {
            if (inventoryUI.boots.item != null)
            {
                //Inventory.instance.Add(inventoryUI.boots.item);
                inventoryUI.boots.OnUnEquip();
            }

            inventoryUI.boots.OnEquip(item);
            Inventory.instance.Remove(item);
            inventoryUI.UpdateInventory();
            inventoryUI.RemoveHighlights();
            HideDetails();
        }
    }

    public void TakeOff()
    {
        if (Inventory.instance.items.Count < Inventory.instance.space)
        {
            if (item.type == "helmet")
            {
                inventoryUI.helmet.OnUnEquip();
                inventoryUI.UpdateInventory();
                inventoryUI.RemoveHighlights();
                HideDetails();
            }
            if (item.type == "weapon")
            {
                inventoryUI.weapon.OnUnEquip();
                inventoryUI.UpdateInventory();
                inventoryUI.RemoveHighlights();
                HideDetails();
            }
            if (item.type == "armor")
            {
                inventoryUI.armor.OnUnEquip();
                inventoryUI.UpdateInventory();
                inventoryUI.RemoveHighlights();
                HideDetails();
            }
            if (item.type == "gloves")
            {
                inventoryUI.gloves.OnUnEquip();
                inventoryUI.UpdateInventory();
                inventoryUI.RemoveHighlights();
                HideDetails();
            }
            if (item.type == "ring")
            {
                inventoryUI.ring.OnUnEquip();
                inventoryUI.UpdateInventory();
                inventoryUI.RemoveHighlights();
                HideDetails();
            }
            if (item.type == "boots")
            {
                inventoryUI.boots.OnUnEquip();
                inventoryUI.UpdateInventory();
                inventoryUI.RemoveHighlights();
                HideDetails();
            }
        }
        else Debug.Log("Nie ma miejsca w ekwipunku");
    }

    public void GenerateDescription()
    {
        Debug.Log('\u015B');
        itemDescription.text = "";

        if (item.type == "weapon" || item.type == "armor" || item.type == "gloves" || 
            item.type == "boots" || item.type == "ring" || item.type == "helmet")
        {
            if (item.attack != 0) itemDescription.text += "Atak: +" + item.attack + "\n";
            if (item.speed != 0) itemDescription.text += "Szybko\u015B\u0107: +" + item.speed + "\n";
            if (item.agility != 0) itemDescription.text += "Zwinno\u015B\u0107: +" + item.agility + "\n";
            if (item.vitality != 0) itemDescription.text += "Witalno\u015B\u0107: +" + item.vitality + "\n";
            if (item.condition != 0) itemDescription.text += "Kondycja: +" + item.condition + "\n";
            if (item.defense != 0) itemDescription.text += "Obrona: +" + item.defense + "\n";
        }
        else if(item.type == "consumable")
        {

        }
        else if (item.type == "coins")
        {

        }
    }
}