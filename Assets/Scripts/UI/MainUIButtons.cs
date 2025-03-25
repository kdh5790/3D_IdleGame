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
        Debug.Log("인벤토리 오픈");
    }

    private void OnClickEquipmentButton()
    {
        Debug.Log("장비창 오픈");

    }

    private void OnClickStatusButton()
    {
        Debug.Log("상태창 오픈");

    }
}
