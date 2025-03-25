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
    [SerializeField] private Image currentArmorImage;

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

            slots.RemoveRange(0, 2);
        }

        AddItem(ItemManager.Instance.equipmentItemList[0]);

        UpdateEquipButtonText();
        SetDescription(); 
    }

    private void OnEnable()
    {
        UpdateEquipButtonText();
        SetDescription();
        if (selectSlot != null)
        {
            selectSlot.SetSelected(true);
        }
    }

    private void OnClickCloseButton()
    {
        UIManager.Instance.RemoveCurrentOpenUI(gameObject);
    }

    private void OnClickEquipOrUnEquipButton()
    {

    }

    private void UpdateEquipButtonText()
    {

    }

    public void AddItem(EquipmentItemData item)
    {
        EquipmentSlot slot = slots.Find(x => x.SlotItem == null);

        if(slot == null)
        {
            Debug.LogWarning("비어있는 슬롯이 없습니다.");
            return;
        }

        slot.SetSlot(item);
    }

    public void SetDescription(EquipmentSlot slot = null)
    {
        selectSlot?.SetSelected(false);
        selectSlot = slot;

        if (slot != null && slot.SlotItem != null)
        {
            description.SetActive(true);
            descriptionText.text = $"{slot.SlotItem.description}";
            descriptionText.text += slot.SlotItem.healthBonus == 0 ? null : $"\n최대 체력 증가:{slot.SlotItem.healthBonus}%";
            descriptionText.text += slot.SlotItem.attackPowerBonus == 0 ? null : $"\n공격력 증가:{slot.SlotItem.attackPowerBonus}%";
            descriptionText.text += slot.SlotItem.goldBonus == 0 ? null : $"\n골드 획득량 증가:{slot.SlotItem.goldBonus}%";
            selectSlot.SetSelected(true);
        }
        else
        {
            description.SetActive(false);
            descriptionText.text = string.Empty;
        }

        UpdateEquipButtonText();
    }
}