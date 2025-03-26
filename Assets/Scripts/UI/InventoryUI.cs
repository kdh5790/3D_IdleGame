using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        GridLayoutGroup gridLayoutGroup = GetComponentInChildren<GridLayoutGroup>();

        if (gridLayoutGroup != null)
        {
            // GridLayoutGroup의 자식 순서대로 리스트에 넣기(FindObjectsOfType으로 할 시 리스트에 순서가 섞여서 들어감)
            for (int i = 0; i < gridLayoutGroup.transform.childCount; i++)
            {
                Transform child = gridLayoutGroup.transform.GetChild(i);
                InventorySlot slot = child.GetComponent<InventorySlot>();

                if (slot != null)
                {
                    slots.Add(slot);
                    slot.SetSlot();
                }
            }
        }
        else
        {
            Debug.LogError("GridLayoutGroup 컴포넌트가 없습니다.");
        }
    }

    private void Update()
    {
        // 테스트용 아이템 추가 함수
        if (Input.GetKeyDown(KeyCode.V))
        {
            AddItem(ItemManager.Instance.consumableItemList[0]);
        }
    }

    // 아이템 추가 함수
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
