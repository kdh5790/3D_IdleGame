using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    private List<InventorySlot> slots = new List<InventorySlot>();
    public ConsumableItemData selectItem;

    [Header("Description")]
    [SerializeField] private GameObject description;
    [SerializeField] private TextMeshProUGUI descriptionText;


    void Start()
    {
        closeButton.onClick.AddListener(OnClickCloseButton);
        slots.AddRange(FindObjectsOfType<InventorySlot>(true));

        if(slots != null)
        {
            foreach(InventorySlot slot in slots)
            {
                slot.SetSlot();
            }
        }
    }

    private void OnClickCloseButton()
    {
        UIManager.Instance.RemoveCurrentOpenUI(gameObject);
    }

    public void SetDescription(ConsumableItemData item)
    {
        if (item == null)
        {
            Debug.LogError("슬롯에 아이템이 등록되지 않았습니다.");
            return;
        }

        selectItem = item;
        description.SetActive(true);
        descriptionText.text = selectItem.description;
    }
}
