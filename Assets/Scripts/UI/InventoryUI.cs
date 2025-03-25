using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Button useButton;
    private List<InventorySlot> slots = new List<InventorySlot>();
    public InventorySlot selectSlot;

    [Header("Description")]
    [SerializeField] private GameObject description;
    [SerializeField] private TextMeshProUGUI descriptionText;


    void Start()
    {
        closeButton.onClick.AddListener(OnClickCloseButton);
        useButton.onClick.AddListener(OnClickUseButton);
        slots.AddRange(FindObjectsOfType<InventorySlot>(true));

        if (slots != null)
        {
            foreach (InventorySlot slot in slots)
            {
                slot.SetSlot();
            }

            slots.Reverse();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            AddItem(ItemManager.Instance.items[0]);
        }
    }

    public void AddItem(ConsumableItemData item)
    {
        InventorySlot slot = slots.Find(x => x.SlotItem == item);

        if (slot == null)
        {
            foreach (var _slot in slots)
            {
                if (_slot.SlotItem == null)
                {
                    _slot.SetSlot(item);
                    return;
                }
            }
        }
        else
        {
            slot.SetSlot(item);
        }
    }

    private void OnClickCloseButton()
    {
        UIManager.Instance.RemoveCurrentOpenUI(gameObject);
    }

    private void OnClickUseButton()
    {
        if (selectSlot != null)
        {
            if (selectSlot.SlotItem != null)
            {
                selectSlot.SlotItem.Use();
                selectSlot.SetStack(-1);
            }
            else
            {
                Debug.LogWarning("슬롯에 아이템이 등록되지 않았습니다.");
            }
        }
        else
        {
            Debug.LogWarning("선택된 슬롯이 없습니다.");
        }
    }

    public void SetDescription(InventorySlot slot = null)
    {
        if (slot == null)
        {
            Debug.LogWarning("슬롯에 아이템이 등록되지 않았습니다.");

            selectSlot = null;
            descriptionText.text = string.Empty;
            description.SetActive(false);

            return;
        }

        selectSlot = slot;
        description.SetActive(true);
        descriptionText.text = slot.SlotItem.description;
    }
}
