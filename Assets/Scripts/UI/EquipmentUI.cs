using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Button equipOrUnEquipButton;
    private List<EquipmentSlot> slots = new List<EquipmentSlot>();
    public EquipmentSlot selectSlot;

    [Header("Current Equipment")]
    [SerializeField] private Image currentWeaponImage;
    [SerializeField] private EquipmentItemData currentWeaponItem;
    [SerializeField] private Image currentArmorImage;
    [SerializeField] private EquipmentItemData currentArmorItem;

    [Header("Description")]
    [SerializeField] private GameObject description;
    [SerializeField] private TextMeshProUGUI descriptionText;

    private void Start()
    {
        closeButton.onClick.AddListener(OnClickCloseButton);
        equipOrUnEquipButton.onClick.AddListener(OnClickEquipOrUnEquipButton);
        slots.AddRange(GetComponentsInChildren<EquipmentSlot>(true));

        if (slots != null)
        {
            foreach (EquipmentSlot slot in slots)
            {
                slot.SetSlot();
            }
        }

        currentWeaponImage.sprite = null;
        currentWeaponImage.enabled = false;

        currentArmorImage.sprite = null;
        currentArmorImage.enabled = false;

        // 테스트
        AddItem(ItemManager.Instance.equipmentItemList[0]);
        AddItem(ItemManager.Instance.equipmentItemList[1]);
        AddItem(ItemManager.Instance.equipmentItemList[2]);
        AddItem(ItemManager.Instance.equipmentItemList[3]);

        SetDescription();
    }

    private void OnEnable()
    {
        SetDescription();
    }

    private void OnClickCloseButton()
    {
        UIManager.Instance.RemoveCurrentOpenUI(gameObject);
    }

    private void OnClickEquipOrUnEquipButton()
    {
        if (selectSlot == null) return;

        if (selectSlot.GetIsEquip())
            UnEquipItem();
        else
            EquipItem();
    }

    private void EquipItem()
    {
        if (selectSlot.SlotItem.type == EquipmentItemData.EquipmentType.Weapon)
        {
            currentWeaponImage.enabled = true;
            currentWeaponImage.sprite = selectSlot.SlotItem.icon;
            currentWeaponItem = selectSlot.SlotItem;
            selectSlot.Equip();
            ChangeButtonText(true);
        }
        else
        {
            currentArmorImage.enabled = true;
            currentArmorImage.sprite = selectSlot.SlotItem.icon;
            currentArmorItem = selectSlot.SlotItem;
            selectSlot.Equip();
            ChangeButtonText(true);
        }
    }

    private void UnEquipItem()
    {
        if (selectSlot.SlotItem.type == EquipmentItemData.EquipmentType.Weapon)
        {
            currentWeaponImage.sprite = null;
            currentWeaponImage.enabled = false;
            currentWeaponItem = null;
            selectSlot.UnEquip();
            ChangeButtonText(selectSlot.GetIsEquip());
        }
        else
        {
            currentArmorImage.sprite = null;
            currentArmorImage.enabled = false;
            currentArmorItem = null;
            selectSlot.UnEquip();
            ChangeButtonText(selectSlot.GetIsEquip());
        }
    }

    public void AddItem(EquipmentItemData item)
    {
        EquipmentSlot slot = slots.Find(x => x.SlotItem == null);

        if (slot == null)
        {
            Debug.LogWarning("비어있는 슬롯이 없습니다.");
            return;
        }

        slot.SetSlot(item);
    }

    public void SetDescription(EquipmentSlot slot = null)
    {
        selectSlot = slot;

        if (slot != null && slot.SlotItem != null)
        {
            description.SetActive(true);
            descriptionText.text = $"{slot.SlotItem.description}";
            descriptionText.text += slot.SlotItem.healthBonus == 0 ? null : $"\n최대 체력 증가:{slot.SlotItem.healthBonus}%";
            descriptionText.text += slot.SlotItem.attackPowerBonus == 0 ? null : $"\n공격력 증가:{slot.SlotItem.attackPowerBonus}%";
            descriptionText.text += slot.SlotItem.goldBonus == 0 ? null : $"\n골드 획득량 증가:{slot.SlotItem.goldBonus}%";
        }
        else
        {
            description.SetActive(false);
            descriptionText.text = string.Empty;
            return;
        }

        foreach (var _slot in slots)
        {
            if (_slot == selectSlot)
                _slot.SetSelected(true);
            else
                _slot.SetSelected(false);
        }

        ChangeButtonText(selectSlot.GetIsEquip());
    }

    private void ChangeButtonText(bool isEquip)
    {
        if (isEquip)
            equipOrUnEquipButton.GetComponentInChildren<TextMeshProUGUI>().text = "해제";
        else
            equipOrUnEquipButton.GetComponentInChildren<TextMeshProUGUI>().text = "장착";
    }
}