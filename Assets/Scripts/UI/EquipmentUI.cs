using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Button equipOrUnEquipButton;
    private List<EquipmentSlot> slots = new List<EquipmentSlot>();
    public EquipmentSlot selectSlot; // 현재 선택한 슬롯

    [Header("Current Equipment")]
    [SerializeField] private Image currentEquipWeaponImage;
    [SerializeField] private EquipmentItemData currentEquipWeaponItem;
    [SerializeField] private Image currentEquipArmorImage;
    [SerializeField] private EquipmentItemData currentEquipArmorItem;

    [Header("Description")]
    [SerializeField] private GameObject description;
    [SerializeField] private TextMeshProUGUI descriptionText;

    private void Start()
    {
        closeButton.onClick.AddListener(OnClickCloseButton);
        equipOrUnEquipButton.onClick.AddListener(OnClickEquipOrUnEquipButton);
        slots.AddRange(GetComponentsInChildren<EquipmentSlot>(true));

        // 슬롯 전체 초기화 진행
        if (slots != null)
        {
            foreach (EquipmentSlot slot in slots)
            {
                slot.SetSlot();
            }
        }

        currentEquipWeaponImage.sprite = null;
        currentEquipWeaponImage.enabled = false;

        currentEquipArmorImage.sprite = null;
        currentEquipArmorImage.enabled = false;

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

    // 장비 장착 함수
    private void EquipItem()
    {
        PlayerStatus playerStatus = Player.Instance.Status;

        // 장착할 아이템이 무기라면
        if (selectSlot.SlotItem.type == EquipmentItemData.EquipmentType.Weapon)
        {
            // 이미 장작한 무기가 있다면 보너스 수치 감소
            if (currentEquipWeaponItem != null)
            {
                playerStatus.EquipAttackBonusPercentage -= currentEquipWeaponItem.attackPowerBonus;
                playerStatus.EquipGoldBonusPercentage -= currentEquipWeaponItem.goldBonus;
            }

            // 아이콘 및 아이템 설정
            currentEquipWeaponImage.enabled = true;
            currentEquipWeaponImage.sprite = selectSlot.SlotItem.icon;
            currentEquipWeaponItem = selectSlot.SlotItem;

            // 보너스 수치 적용
            playerStatus.EquipAttackBonusPercentage += currentEquipWeaponItem.attackPowerBonus;
            playerStatus.EquipGoldBonusPercentage += currentEquipWeaponItem.goldBonus;

            // 현재 장착한 무기를 제외한 나머지 무기들 장착 해제 처리
            foreach (var slot in slots)
            {
                if (slot.SlotItem != null && slot.SlotItem.type == EquipmentItemData.EquipmentType.Weapon && slot != selectSlot)
                    slot.UnEquip();
            }
        }
        // 장착할 아이템이 방어구라면
        else
        {
            if (currentEquipArmorItem != null)
            {
                playerStatus.EquipHealthBonusPercentage -= currentEquipArmorItem.healthBonus;
                playerStatus.EquipGoldBonusPercentage -= currentEquipArmorItem.goldBonus;
            }

            currentEquipArmorImage.enabled = true;
            currentEquipArmorImage.sprite = selectSlot.SlotItem.icon;
            currentEquipArmorItem = selectSlot.SlotItem;

            playerStatus.EquipHealthBonusPercentage += currentEquipArmorItem.healthBonus;
            playerStatus.EquipGoldBonusPercentage += currentEquipArmorItem.goldBonus;

            foreach (var slot in slots)
            {
                if (slot.SlotItem != null && slot.SlotItem.type == EquipmentItemData.EquipmentType.Armor && slot != selectSlot)
                    slot.UnEquip();
            }
        }

        selectSlot.Equip(); // 선택된 슬롯 장착 처리
        ChangeButtonText(true); // 버튼 텍스트 변경(장착 or 해제);
        Player.Instance.Status.OnStatusChanged?.Invoke(); // 변경된 수치 UI에 반영
    }

    // 장비 장착 해제 함수
    private void UnEquipItem()
    {
        PlayerStatus playerStatus = Player.Instance.Status;

        if (selectSlot.SlotItem.type == EquipmentItemData.EquipmentType.Weapon)
        {
            playerStatus.EquipHealthBonusPercentage -= currentEquipWeaponItem.healthBonus;
            playerStatus.EquipGoldBonusPercentage -= currentEquipWeaponItem.goldBonus;

            currentEquipWeaponImage.sprite = null;
            currentEquipWeaponImage.enabled = false;
            currentEquipWeaponItem = null;

            selectSlot.UnEquip();
            ChangeButtonText(selectSlot.GetIsEquip());
        }
        else
        {
            playerStatus.EquipHealthBonusPercentage -= currentEquipArmorItem.healthBonus;
            playerStatus.EquipGoldBonusPercentage -= currentEquipArmorItem.goldBonus;

            currentEquipArmorImage.sprite = null;
            currentEquipArmorImage.enabled = false;
            currentEquipArmorItem = null;

            selectSlot.UnEquip();
            ChangeButtonText(selectSlot.GetIsEquip());
        }

        Player.Instance.Status.OnStatusChanged?.Invoke();
    }

    // 슬롯에 아이템 추가 함수
    public void AddItem(EquipmentItemData item)
    {
        // 현재 비어있는 슬롯 찾아오기
        EquipmentSlot slot = slots.Find(x => x.SlotItem == null);

        // 비어있는 슬롯이 없다면 함수 종료
        if (slot == null)
        {
            Debug.LogWarning("비어있는 슬롯이 없습니다.");
            return;
        }

        // 비어있는 슬롯이 있다면 아이템 넣어주기
        slot.SetSlot(item);
    }

    // 설명UI 설정
    public void SetDescription(EquipmentSlot slot = null)
    {
        selectSlot = slot;

        // 매개변수가 있다면 매개변수 아이템에 맞게 UI 텍스트 설정
        if (slot != null && slot.SlotItem != null)
        {
            description.SetActive(true);
            descriptionText.text = $"{slot.SlotItem.description}";
            descriptionText.text += slot.SlotItem.healthBonus == 0 ? null : $"\n최대 체력 증가:{slot.SlotItem.healthBonus}%";
            descriptionText.text += slot.SlotItem.attackPowerBonus == 0 ? null : $"\n공격력 증가:{slot.SlotItem.attackPowerBonus}%";
            descriptionText.text += slot.SlotItem.goldBonus == 0 ? null : $"\n골드 획득량 증가:{slot.SlotItem.goldBonus}%";
        }
        // 없다면 UI 비활성화
        else
        {
            descriptionText.text = string.Empty;
            description.SetActive(false);
            return;
        }

        // 현재 슬롯 제외한 모든 슬롯들 선택되지 않은 상태로 변경
        foreach (var _slot in slots)
        {
            if (_slot == selectSlot)
                _slot.SetSelected(true);
            else
                _slot.SetSelected(false);
        }

        ChangeButtonText(selectSlot.GetIsEquip());
    }

    // 장착 및 해제 버튼 텍스트 변경
    private void ChangeButtonText(bool isEquip)
    {
        if (isEquip)
            equipOrUnEquipButton.GetComponentInChildren<TextMeshProUGUI>().text = "해제";
        else
            equipOrUnEquipButton.GetComponentInChildren<TextMeshProUGUI>().text = "장착";
    }
}