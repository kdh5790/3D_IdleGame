using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ConsumableItemData slotItem;
    [SerializeField] private Image frameImage;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI stackText;

    private int stack;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (slotItem != null)
        {
            UIManager.Instance.inventoryUI.SetDescription(slotItem);
            frameImage.color = Color.green;
        }
    }

    public void SetSlot(ConsumableItemData item = null)
    {
        if (item == null)
        {
            slotItem = null;
            stack = 0;
            itemImage.enabled = false;
            stackText.enabled = false;
            return;
        }

        slotItem = item;

        itemImage.enabled = true;
        itemImage.sprite = slotItem.icon;

        stackText.enabled = true;
        stack++;
        stackText.text = stack.ToString();
    }
}
