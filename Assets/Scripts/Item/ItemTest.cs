using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTest : MonoBehaviour
{
    public ConsumableItemData itemData;

    private void Start()
    {
        if (itemData != null)
            itemData.Use();
    }
}
