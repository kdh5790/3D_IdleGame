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
    [SerializeField] private GameObject isEquip;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (slotItem != null && UIManager.Instance.equipmentUI.selectSlot != this)
        {
            UIManager.Instance.equipmentUI.SetDescription(this);
        }
        else if (UIManager.Instance.equipmentUI.selectSlot == this)
        {
            UIManager.Instance.equipmentUI.SetDescription();
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

        if (isEquip != null)
            isEquip.SetActive(false);
    }

    public void SetSelected(bool isSelected)
    {
        frameImage.color = isSelected ? Color.green : Color.white;
    }

    public bool GetIsEquip() => isEquip.activeSelf;
    public void Equip() => isEquip.SetActive(true);
    public void UnEquip() => isEquip.SetActive(false);
}