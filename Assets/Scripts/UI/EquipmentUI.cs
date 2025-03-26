using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Button equipOrUnEquipButton;
    private List<EquipmentSlot> slots = new List<EquipmentSlot>();
    public EquipmentSlot selectSlot; // ���� ������ ����

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

        // ���� ��ü �ʱ�ȭ ����
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

        // �׽�Ʈ
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

    // ��� ���� �Լ�
    private void EquipItem()
    {
        PlayerStatus playerStatus = Player.Instance.Status;

        // ������ �������� ������
        if (selectSlot.SlotItem.type == EquipmentItemData.EquipmentType.Weapon)
        {
            // �̹� ������ ���Ⱑ �ִٸ� ���ʽ� ��ġ ����
            if (currentEquipWeaponItem != null)
            {
                playerStatus.EquipAttackBonusPercentage -= currentEquipWeaponItem.attackPowerBonus;
                playerStatus.EquipGoldBonusPercentage -= currentEquipWeaponItem.goldBonus;
            }

            // ������ �� ������ ����
            currentEquipWeaponImage.enabled = true;
            currentEquipWeaponImage.sprite = selectSlot.SlotItem.icon;
            currentEquipWeaponItem = selectSlot.SlotItem;

            // ���ʽ� ��ġ ����
            playerStatus.EquipAttackBonusPercentage += currentEquipWeaponItem.attackPowerBonus;
            playerStatus.EquipGoldBonusPercentage += currentEquipWeaponItem.goldBonus;

            // ���� ������ ���⸦ ������ ������ ����� ���� ���� ó��
            foreach (var slot in slots)
            {
                if (slot.SlotItem != null && slot.SlotItem.type == EquipmentItemData.EquipmentType.Weapon && slot != selectSlot)
                    slot.UnEquip();
            }
        }
        // ������ �������� �����
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

        selectSlot.Equip(); // ���õ� ���� ���� ó��
        ChangeButtonText(true); // ��ư �ؽ�Ʈ ����(���� or ����);
        Player.Instance.Status.OnStatusChanged?.Invoke(); // ����� ��ġ UI�� �ݿ�
    }

    // ��� ���� ���� �Լ�
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

    // ���Կ� ������ �߰� �Լ�
    public void AddItem(EquipmentItemData item)
    {
        // ���� ����ִ� ���� ã�ƿ���
        EquipmentSlot slot = slots.Find(x => x.SlotItem == null);

        // ����ִ� ������ ���ٸ� �Լ� ����
        if (slot == null)
        {
            Debug.LogWarning("����ִ� ������ �����ϴ�.");
            return;
        }

        // ����ִ� ������ �ִٸ� ������ �־��ֱ�
        slot.SetSlot(item);
    }

    // ����UI ����
    public void SetDescription(EquipmentSlot slot = null)
    {
        selectSlot = slot;

        // �Ű������� �ִٸ� �Ű����� �����ۿ� �°� UI �ؽ�Ʈ ����
        if (slot != null && slot.SlotItem != null)
        {
            description.SetActive(true);
            descriptionText.text = $"{slot.SlotItem.description}";
            descriptionText.text += slot.SlotItem.healthBonus == 0 ? null : $"\n�ִ� ü�� ����:{slot.SlotItem.healthBonus}%";
            descriptionText.text += slot.SlotItem.attackPowerBonus == 0 ? null : $"\n���ݷ� ����:{slot.SlotItem.attackPowerBonus}%";
            descriptionText.text += slot.SlotItem.goldBonus == 0 ? null : $"\n��� ȹ�淮 ����:{slot.SlotItem.goldBonus}%";
        }
        // ���ٸ� UI ��Ȱ��ȭ
        else
        {
            descriptionText.text = string.Empty;
            description.SetActive(false);
            return;
        }

        // ���� ���� ������ ��� ���Ե� ���õ��� ���� ���·� ����
        foreach (var _slot in slots)
        {
            if (_slot == selectSlot)
                _slot.SetSelected(true);
            else
                _slot.SetSelected(false);
        }

        ChangeButtonText(selectSlot.GetIsEquip());
    }

    // ���� �� ���� ��ư �ؽ�Ʈ ����
    private void ChangeButtonText(bool isEquip)
    {
        if (isEquip)
            equipOrUnEquipButton.GetComponentInChildren<TextMeshProUGUI>().text = "����";
        else
            equipOrUnEquipButton.GetComponentInChildren<TextMeshProUGUI>().text = "����";
    }
}