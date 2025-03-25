using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private EquipmentItemData slotItem;
    public EquipmentItemData SlotItem { get { return slotItem; } }

    [SerializeField] private Image frameImage;
    [SerializeField] private Image itemImage;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (slotItem != null && UIManager.Instance.equipmentUI.selectSlot != this)
        {
            UIManager.Instance.equipmentUI.SetDescription(this);
            frameImage.color = Color.green;
        }
        else if (UIManager.Instance.equipmentUI.selectSlot == this)
        {
            UIManager.Instance.equipmentUI.SetDescription();
            frameImage.color = Color.white;
            UIManager.Instance.equipmentUI.selectSlot = null;
        }
        else if (slotItem != null && UIManager.Instance.equipmentUI.selectSlot == null)
        {
            UIManager.Instance.equipmentUI.SetDescription(this);
            frameImage.color = Color.green;
            UIManager.Instance.equipmentUI.selectSlot = this;
        }
    }

    public void SetSlot(EquipmentItemData item = null)
    {
        slotItem = item;
        if (slotItem != null)
        {
            itemImage.enabled = true;
            itemImage.sprite = slotItem.icon;
        }
        else
        {
            ClearSlot();
        }
    }

    public void ClearSlot()
    {
        slotItem = null;
        itemImage.enabled = false;
        frameImage.color = Color.white;
    }

    public void SetSelected(bool isSelected)
    {
        frameImage.color = isSelected ? Color.green : Color.white;
    }
}