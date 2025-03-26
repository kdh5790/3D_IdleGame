using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private EquipmentItemData slotItem; // 현재 슬롯에 등록된 아이템 정보
    public EquipmentItemData SlotItem { get { return slotItem; } }

    [SerializeField] private Image frameImage;
    [SerializeField] private Image itemImage;
    [SerializeField] private GameObject isEquip;

    // 현재 슬롯 클릭 시 설명 띄워주기
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

    // 슬롯 세팅 함수
    public void SetSlot(EquipmentItemData item = null)
    {
        slotItem = item;
        // 매개변수가 있다면
        if (slotItem != null)
        {
            // 슬롯의 아이템 이미지 활성화 및 아이콘 적용
            itemImage.enabled = true;
            itemImage.sprite = slotItem.icon;
        }
        else
        {
            ClearSlot();
        }
    }

    // 슬롯 초기화 함수
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