using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIButtons : MonoBehaviour
{
    [SerializeField] private Button inventoryButton;
    [SerializeField] private Button equipmentButton;
    [SerializeField] private Button statusButton;

    private void Start()
    {
        inventoryButton.onClick.AddListener(OnClickInventoryButton);
        equipmentButton.onClick.AddListener(OnClickEquipmentButton);
        statusButton.onClick.AddListener(OnClickStatusButton);
    }

    private void OnClickInventoryButton()
    {
        UIManager.Instance.AddCurrentOpenUI(UIManager.Instance.inventoryUI.gameObject);
    }

    private void OnClickEquipmentButton()
    {
        UIManager.Instance.AddCurrentOpenUI(UIManager.Instance.equipmentUI.gameObject);
    }

    private void OnClickStatusButton()
    {
        UIManager.Instance.AddCurrentOpenUI(UIManager.Instance.statusUI.gameObject);
    }
}
