using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one inventory");
            return;
        }
        instance = this;
    }
    #endregion
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;
    public bool canToggle;

    public List<Item> items = new List<Item>();

    public void Add (Item item)
    {
       items.Add(item);

        if (onItemChangedCallback != null) onItemChangedCallback.Invoke();
    }

    public void Remove (Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null) onItemChangedCallback.Invoke();
    }
}
