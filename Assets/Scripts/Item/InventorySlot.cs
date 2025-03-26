using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ConsumableItemData slotItem; // 슬롯에 등록된 아이템 정보
    public ConsumableItemData SlotItem { get { return slotItem; } }

    [SerializeField] private Image frameImage;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI stackText;

    private int stack;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (slotItem != null && UIManager.Instance.inventoryUI.selectSlot != this)
        {
            UIManager.Instance.inventoryUI.SetDescription(this);
            frameImage.color = Color.green;
        }
        else if(UIManager.Instance.inventoryUI.selectSlot == this)
        {
            UIManager.Instance.inventoryUI.SetDescription();
            frameImage.color = Color.white;
        }
    }

    // 슬롯 세팅 함수
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

        if (slotItem == null)
            slotItem = item;

        itemImage.enabled = true;
        itemImage.sprite = slotItem.icon;

        stackText.enabled = true;
        stack++;
        stackText.text = stack.ToString();
    }

    // 현재 슬롯의 아이템 수량 관리 함수
    public void SetStack(int stack)
    {
        this.stack += stack;

        if(this.stack <= 0)
        {
            this.stack = 0;
            SetSlot();

            UIManager.Instance.inventoryUI.SetDescription();
            frameImage.color = Color.white;

            return;
        }

        stackText.enabled = true;
        stackText.text = this.stack.ToString();
    }
}
