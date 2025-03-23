using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<GameObject> items = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void AddItem(GameObject item)
    {
        items.Add(item);
    }
}
