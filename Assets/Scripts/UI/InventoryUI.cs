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
            // GridLayoutGroup�� �ڽ� ������� ����Ʈ�� �ֱ�(FindObjectsOfType���� �� �� ����Ʈ�� ������ ������ ��)
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
            Debug.LogError("GridLayoutGroup ������Ʈ�� �����ϴ�.");
        }
    }

    private void Update()
    {
        // �׽�Ʈ�� ������ �߰� �Լ�
        if (Input.GetKeyDown(KeyCode.V))
        {
            AddItem(ItemManager.Instance.consumableItemList[0]);
        }
    }

    // ������ �߰� �Լ�
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
                Debug.LogWarning("���Կ� �������� ��ϵ��� �ʾҽ��ϴ�.");
            }
        }
        else
        {
            Debug.LogWarning("���õ� ������ �����ϴ�.");
        }
    }

    public void SetDescription(InventorySlot slot = null)
    {
        if (slot == null)
        {
            Debug.LogWarning("���Կ� �������� ��ϵ��� �ʾҽ��ϴ�.");

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
